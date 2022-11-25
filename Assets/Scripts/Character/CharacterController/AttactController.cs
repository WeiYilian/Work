using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class AttactController : MonoBehaviour
{
    private Animator animator;

    private CharacterStats characterStats;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        characterStats = GetComponent<CharacterStats>();
    }

    #region 普通攻击参数设置
    
    [Header("攻击参数设置")]
    //连击次数
    public int comboStep;
    //允许连击的时间
    public float interval = 1f;
    //计时器
    private float timer;
    //判断是否攻击
    [HideInInspector]public bool isAttack;
    //普通攻击的补偿速度
    private float speed = 7f;
    //攻击范围
    public float attackRange;
    //普通攻击范围
    private float ordinaryRange = 1f;
    //第四下攻击范围
    private float fourAttackRange = 3f;

    #endregion

    #region 技能攻击参数设置

    //技能列表，存储技能
    private List<BaseSkill> Skills;
    
    //技能特效位置
    public Transform emitPoint;

    #endregion
    
    private void Start()
    {
        Skills = new List<BaseSkill>();
        
        Skills.Add(new SkillOne(emitPoint,GameFacade.Instance.PanelManager.skill1));
        Skills.Add(new SkillTwo(emitPoint,GameFacade.Instance.PanelManager.skill2));
        Skills.Add(new SkillThree(emitPoint,GameFacade.Instance.PanelManager.skill3));
        Skills.Add(new SkillFour(emitPoint));
    }
    
    #region 普通攻击

    /// <summary>
    /// 玩家攻击
    /// </summary>
    public void Attack()
    {
        if (PlayerConctroller.Instance.isHit) return;
        SkillTrigger();
        
        if (Input.GetMouseButtonDown(0) && !isAttack && !animator.GetCurrentAnimatorStateInfo(0).IsName("jump"))
        {
            isAttack = true;
            //连击次数递增
            comboStep++;
            if (comboStep > 4)
                comboStep = 1;
            
            //根据连击次数更换攻击范围
            if (comboStep == 4)
                attackRange = fourAttackRange;
            else
                attackRange = ordinaryRange;
            timer = interval;
            PlayerConctroller.Instance.animator.SetTrigger("Attack");
            PlayerConctroller.Instance.animator.SetInteger("ComboStep",comboStep);
        }
        else if (Input.GetMouseButtonDown(0) && !isAttack && animator.GetCurrentAnimatorStateInfo(0).IsName("jump"))
        {
            PlayerConctroller.Instance.animator.SetTrigger("JumpAttack");
        }

        if (timer != 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
                comboStep = 0;
            }
        }
    }

    /// <summary>
    /// 动画事件，玩家攻击结束
    /// </summary>
    void AttackOver()
    {
        isAttack = false;
    }

    /// <summary>
    /// 玩家攻击往前略微移动
    /// </summary>
    /// <param name="moveDirection"></param>
    public void AttackMove(ref Vector3 moveDirection)
    {
        moveDirection *= speed;
    }

    #endregion

    #region 技能攻击

    // ReSharper disable Unity.PerformanceAnalysis
    /// <summary>
    /// 检测是否触发技能
    /// </summary>
    public void SkillTrigger()
    {
        if (Input.GetKeyUp(KeyCode.Q) && !isAttack && !Skills[0].IsCooling)
        {
            isAttack = true;
            Skills[0].Image.fillAmount = 1;
            animator.SetTrigger("SkillOne");
        }
        if (Input.GetKeyUp(KeyCode.E) && !isAttack && !Skills[1].IsCooling)
        {
            isAttack = true;
            Skills[1].Image.fillAmount = 1;
            animator.SetTrigger("SkillTwo");
        }
        if (Input.GetKeyUp(KeyCode.R) && !isAttack && !Skills[2].IsCooling)
        {
            isAttack = true;
            Skills[2].Image.fillAmount = 1;
            animator.SetTrigger("SkillThree");
        }
    }
    
    /// <summary>
    /// 发出技能后冷却
    /// </summary>
    /// <param name="skill"></param>
    /// <param name="cd"></param>
    /// <param name="cdTime"></param>
    public void CdDecline(int index,ref float cdTime)
    {
        //玩家面板技能冷却
        if (Skills[index].Image.fillAmount != 0)
        {
            Skills[index].IsCooling = true;
            cdTime += Time.deltaTime;
            Skills[index].Image.fillAmount = (Skills[index].SkillCd - cdTime) / Skills[index].SkillCd;
        }
        if (cdTime >= Skills[index].SkillCd)
        {
            Skills[index].IsCooling = false;
            Skills[index].Image.fillAmount = 0;
            cdTime = 0;
        }
    }
    
    
    /// <summary>
    /// 帧事件，播放特效
    /// </summary>
    public void AttackEffection(int index)
    {
        Skills[index].EmitSpecialEffects();
        Hit();
    }

    #endregion
    
    public void Hit()
    {
        var colliders = Physics.OverlapSphere(transform.position, attackRange);

        foreach (var enemy in colliders)
        {
            if (transform.IsFacingTarget(enemy.transform)/*扩展方法*/)
            {
                var targetStats = enemy.GetComponent<CharacterStats>();
                targetStats.TakeDamage(characterStats, targetStats);
            }
        }
    }
    
    /// <summary>
    /// Animation Even
    /// </summary>
    void Rise()
    {
        PlayerConctroller.Instance.isHit = false;
    }
    void Dead()
    {
        PlayerConctroller.Instance.isHit = true;
    }
    
}