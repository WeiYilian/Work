using UnityEngine;
using UnityEngine.AI;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;


public enum EnemyStates {GUARD, PATROL, CHASE ,DEAD }
public enum EnemySort{MAGE,WARRIOR}
[RequireComponent(typeof(NavMeshAgent))]//要求脚本挂载的物体上必须要有某个组件
[RequireComponent(typeof(CharacterStats))]
public class EnemyConcroller : MonoBehaviour,IEnemy,IPoolable
{
    private EnemyStates enemyStates;
    private EnemySort enemySort;
    private NavMeshAgent agent;
    protected Animator animator;
    private new Collider collider;
    
    protected CharacterStats characterStats;
    
    [Header("Basic Settings")]
    public float sightRadius;//攻击范围
    public bool isGuard;
    public bool isSpawn;
    private float speed;
    protected GameObject AttackTarget;
        
    public float lookAtTime;//巡逻停留时间
    private float remainLookAtTime;//巡逻计时器
    private float lastAttackTime;//攻击计时器

    [Header("Patrol State")] 
    public float patrolRange;//巡逻范围
    private Vector3 wayPoint;//随机巡逻点
    public Vector3 guardPos;//初始坐标
    private Quaternion guardRotation;//初始角度(初始面向方向)
    
    //bool配合动画
    private bool isWalk;
    private bool isChase;
    private bool isFollow;
    private bool isDead;
    private bool playerDead;

    protected void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        characterStats = GetComponent<CharacterStats>();
        collider = GetComponent<Collider>();
        
        speed = agent.speed;
        guardRotation = transform.rotation;
        remainLookAtTime = lookAtTime;
    }

    private void Start()
    {
        EvenCenter.AddListener(EventNum.GAMEOVER,EndNotify);
        
        //判断怪物类型
        if (transform.CompareTag("Mage"))
            enemySort = EnemySort.MAGE;
        else if (transform.CompareTag("Warrior"))
            enemySort = EnemySort.WARRIOR;
    }

    protected void OnEnable()
    {
        guardPos = transform.position;
        //判断是否为站桩怪
        if (isGuard)
        {
            enemyStates = EnemyStates.GUARD;
        }
        else
        {
            enemyStates = EnemyStates.PATROL;
            wayPoint = transform.position;
        }
    }

    private void OnDisable()
    {
        EvenCenter.RemoveListener(EventNum.GAMEOVER,EndNotify);
    }

    private void Update()
    {
        if (playerDead || isSpawn || GameLoop.Instance.isTimeOut) return;
        
        if (characterStats.CurrentHealth == 0)
            isDead = true;
        
        if (animator.GetCurrentAnimatorStateInfo(2).IsName("Damage") ||
            animator.GetCurrentAnimatorStateInfo(2).IsName("Dizzy"))
            return;

        SwitchStates();
        SwitchAnimation();
        lastAttackTime -= Time.deltaTime;
        
    }

    void SwitchAnimation()
    {
        animator.SetBool("Walk",isWalk);
        animator.SetBool("Chase",isChase);
        animator.SetBool("Follow",isFollow);
        animator.SetBool("Death", isDead);
    }
    
    
    // ReSharper disable Unity.PerformanceAnalysis
    /// <summary>
    /// 状态切换
    /// </summary>
    void SwitchStates()
    {
        //如果生命值等于0，切换到DEAD
        if (isDead)
            enemyStates = EnemyStates.DEAD;
        //如果发现Player，切换到CHASE
        else if (FoundPlayer())
        {
            enemyStates = EnemyStates.CHASE;
        }
            

        switch (enemyStates)
        {
            case EnemyStates.GUARD://站桩模式的敌人
                isChase = false;
                agent.isStopped = true;

                if (transform.position != guardPos)
                {
                    isWalk = true;
                    agent.isStopped = false;
                    agent.destination = guardPos;//回到原点

                    //SqrMagnitude计算两个三维坐标之间的差值，与disdance作用类似，但开销比distance小
                    if (Vector3.SqrMagnitude(guardPos - transform.position) <= agent.stoppingDistance)
                    {
                        isWalk = false;
                        transform.rotation = Quaternion.Lerp(transform.rotation,guardRotation,0.1f);//最后一个参数越小，旋转的越慢
                    }
                }
                
                break;
            case EnemyStates.PATROL://巡逻模式的敌人
                isChase = false;
                agent.speed = speed;
                //判断是否到了随机巡逻点
                if (Vector3.Distance(wayPoint,transform.position) <= agent.stoppingDistance)
                {
                    isWalk = false;
                    if (remainLookAtTime > 0)
                        remainLookAtTime -= Time.deltaTime;
                    else
                        GetNewWayPoint();
                }
                else
                {
                    isWalk = true;
                    agent.destination = wayPoint;
                }
                break;
            case EnemyStates.CHASE://追击模式
                isWalk = false;
                isChase = true;

                agent.speed = speed;
                
                if (!FoundPlayer())
                {
                    isFollow = false;
                    if (remainLookAtTime > 0)
                    {
                        agent.destination = transform.position;
                        remainLookAtTime -= Time.deltaTime;
                    }
                    
                    else if (isGuard)
                        enemyStates = EnemyStates.GUARD;
                    else
                        enemyStates = EnemyStates.PATROL;
                }
                else
                {
                    isFollow = true;
                    agent.isStopped = false;
                    agent.destination = AttackTarget.transform.position;
                }
                //在攻击范围内则攻击
                if (TargetInAttackRange())
                {
                    isFollow = false;
                    agent.isStopped = true;
                    transform.LookAt(AttackTarget.transform);
                    if (lastAttackTime < 0)
                    {
                        lastAttackTime = characterStats.characterData.coolDown;
                        
                        //暴击判断
                        characterStats.isCritical = Random.value < characterStats.characterData.criticalChance;
                        //执行攻击
                        Attack();
                    }
                }
                
                break;
            case EnemyStates.DEAD://死亡模式
                if(collider.enabled)
                    Invoke(nameof(EnemyDie), 2f);
                collider.enabled = false;//关闭collider
                agent.radius = 0;
                break;
        }
    }

    protected virtual void Attack()
    {
        if (TargetInSkillRange())
            animator.SetTrigger("Skill");//技能攻击动画
        else if (TargetInAttackRange())
            animator.SetTrigger("Attack");//近身攻击动画

    }

    //检测敌人sightRadius内是否有Player
    bool FoundPlayer()
    {
        var colliders = Physics.OverlapSphere(transform.position, sightRadius);

        foreach (var target in colliders)
        {
            if (target.CompareTag("Player"))
            {
                AttackTarget = target.gameObject;
                return true;
            }
        }

        AttackTarget = null;
        return false;
    }

    //判断是否进入基础攻击距离
    protected bool TargetInAttackRange()
    {
        if (AttackTarget != null)
            return Vector3.Distance(AttackTarget.transform.position, transform.position) <=
                   characterStats.characterData.attackRange;
        else
            return false;
    }
    
    //判断是否进入技能攻击距离
    protected bool TargetInSkillRange()
    {
        if (AttackTarget != null)
            return Vector3.Distance(AttackTarget.transform.position, transform.position) <=
                   characterStats.characterData.skillRange;
        else
            return false;
    }

    //获取随机巡逻点
    void GetNewWayPoint()
    {
        remainLookAtTime = lookAtTime;
        
        float randomX = Random.Range(-patrolRange, patrolRange);
        float randomZ = Random.Range(-patrolRange, patrolRange);

        Vector3 randomPoint = new Vector3(guardPos.x + randomX, transform.position.y, guardPos.z + randomZ);

        NavMeshHit hit;
        wayPoint = NavMesh.SamplePosition(randomPoint, out hit, patrolRange, 1) ? hit.position : transform.position;
    }


    #region 动画事件

    //Animation Event
    public virtual void Hit()
    {
        //如果攻击目标不为空，攻击目标在前方，没有被打就可以执行
        if (TargetInAttackRange() && transform.IsFacingTarget(AttackTarget.transform)/*扩展方法*/)
        {
            AudioManager.Instance.PlayAudio(3,"Attack2");
            var targetStats = AttackTarget.GetComponentInChildren<CharacterStats>();
            targetStats.TakeDamage(characterStats, targetStats);
        }
    }
    
    //Animation Event
    public virtual void KickOff()
    {
        //TODO:重击声音
        if(TargetInSkillRange() && transform.IsFacingTarget(AttackTarget.transform)/*扩展方法*/)
        {
            var targetStats = AttackTarget.GetComponentInChildren<CharacterStats>();
            targetStats.TakeDamage(characterStats, targetStats,true);
            
        }
    }

    #endregion
    
    
    //怪物胜利，游戏结束
    public void EndNotify()
    {
        //获胜动画
        animator.SetBool("Win",true);
        //停止所有移动
        playerDead = true;
        isChase = false;
        isWalk = false;
        AttackTarget = null;
        //停止Agent

    }
    
    //将各种范围可视化
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRadius);
    }

    public void EnemyDie()
    {
        switch (enemySort)
        {
            case EnemySort.MAGE:
                if (Random.value <= 0.8f)
                {
                    GameObject yao = ObjectPool.Instance.Get("RestoreDrug");
                    yao.transform.position = transform.position;
                }
                ObjectPool.Instance.Remove("Mage",gameObject);
                if (TaskManager.Instance.Tasks[2].IsAcceptTask)
                    TaskManager.Instance.Tasks[2].TaskCompletion++;
                if (TaskManager.Instance.Tasks[4].IsAcceptTask)
                    TaskManager.Instance.Tasks[4].TaskCompletion++;
                break;
            case EnemySort.WARRIOR:
                if (Random.value <= 0.5f)
                {
                    GameObject yao = ObjectPool.Instance.Get("RestoreDrug");
                    yao.transform.position = transform.position;
                }
                ObjectPool.Instance.Remove("warrior",gameObject);
                if (TaskManager.Instance.Tasks[1].IsAcceptTask)
                    TaskManager.Instance.Tasks[1].TaskCompletion++;
                if (TaskManager.Instance.Tasks[3].IsAcceptTask)
                    TaskManager.Instance.Tasks[3].TaskCompletion++;
                break;
            default:
                break;
        } 
    }
    
    public void Dispose()
    {
        collider.enabled = false;
        gameObject.SetActive(false);
    }

    public void Init()
    {
        gameObject.SetActive(true);
    }
}
