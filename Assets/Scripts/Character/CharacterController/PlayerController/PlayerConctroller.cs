using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Windows.WebCam;

[RequireComponent(typeof(CharacterController))]
public class PlayerConctroller : MonoBehaviour
{
    public static PlayerConctroller Instance;
    
    [HideInInspector] public CharacterController characterController;

    [HideInInspector] public Animator animator;

    [HideInInspector] public CharacterStats characterStats;
    
    //摄像机的位置
    [HideInInspector] public CameraController photographer;
    //摄像机跟随的位置
    [HideInInspector] public Transform followingTarget;
    //背包
    public Inventory myBag;

    public PanelManager PanelManager;

    private MoveController moveController;

    private AttackController attackController;

    private AttribController attribController;

    private string playerName;

    private List<string> playerAttrib;

    //判断是否可以移动
    [HideInInspector] public bool canMove = true;
    //判断是否被击打
    [HideInInspector] public bool isHit;
    //判断是否攻击
    [HideInInspector] public bool isAttack;
    //判断是否死亡
    [HideInInspector] public bool isDeath;
    //判断是否暂停
    [HideInInspector] public bool isTimeOut;

    public string PlayerName
    {
        get => playerName;
        private set => playerName = value;
    }

    public List<string> PlayerAttrib
    {
        get => playerAttrib;
        set => playerAttrib = value;
    }

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    private void Start()
    {
        Init();
    }

    public void Update()
    {
        if (characterStats.CurrentHealth == 0)
        {
            PlayerDeath();
            return;
        }
        moveController.PlayerAction();
        attackController.Attack();
        attribController.SuperviserNumber();
    }

    private void Init()
    {
        //TODO:PlayerName = PlayerPrefs.GetString("Player");
        PlayerName = "吴先谱";
        PlayerAttrib = DataManager.SelectUser(PlayerName);

        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        characterStats = GetComponent<CharacterStats>();
        photographer = GameObject.Find("photograther").GetComponent<CameraController>();
        followingTarget = transform.GetChild(3);
        
        PanelManager = PanelManager.Instance;
        moveController = new MoveController();
        attackController = new AttackController();
        attribController = new AttribController();
        
        //TODO:测试用
        PanelManager.Instance.Push(new MainPanel());
        ObjectPool.Instance.Init("Mage",GameFacade.Instance.LoadGameObject("Mage"),5);
        ObjectPool.Instance.Init("Warrior",GameFacade.Instance.LoadGameObject("Warrior"),5);
        ObjectPool.Instance.Init("Sword_Slash_A",GameFacade.Instance.LoadGameObject("Sword_Slash_A"),1);
        ObjectPool.Instance.Init("Sword_Slash_1",GameFacade.Instance.LoadGameObject("Sword_Slash_1"),1);
        ObjectPool.Instance.Init("Sword_Slash_2",GameFacade.Instance.LoadGameObject("Sword_Slash_2"),1);
        ObjectPool.Instance.Init("Sword_Slash_3",GameFacade.Instance.LoadGameObject("Sword_Slash_3"),1);
        ObjectPool.Instance.Init("MageSkill",GameFacade.Instance.LoadGameObject("MageSkill"),1);
        ObjectPool.Instance.Init("MageAttack",GameFacade.Instance.LoadGameObject("MageAttack"),1);
        ObjectPool.Instance.Init("UPLevel",GameFacade.Instance.LoadGameObject("UPLevel"),1);
        ObjectPool.Instance.Init("Treat",GameFacade.Instance.LoadGameObject("Treat"),1);

        //将数据库中的数据装入游戏中
        characterStats.CurrentHealth = Convert.ToSingle(playerAttrib[7]);
        characterStats.CurrentExp = Convert.ToSingle(playerAttrib[6]);
        characterStats.CurrentLevel = Convert.ToInt32(playerAttrib[5]);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void PlayerDeath()
    {
        isDeath = true;
        if(!animator.GetCurrentAnimatorStateInfo(4).IsName("Death"))
            animator.SetBool("Death",isDeath);
        EvenCenter.BroadCast(EventNum.GAMEOVER);
    }

    #region 动画事件

    public void Hit()
    {
        attackController.Hit();
    }

    public void AttackEffection(int index)
    {
        attackController.AttackEffection(index);
    }

    public void AttackOver()
    {
        attackController.AttackOver();
    }

    #endregion
    
}