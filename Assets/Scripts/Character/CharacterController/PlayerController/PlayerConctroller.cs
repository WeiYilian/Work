using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Windows.WebCam;

[RequireComponent(typeof(CharacterController))]
public class PlayerConctroller : MonoBehaviour
{
    //public static PlayerConctroller Instance;
    
    [HideInInspector] public CharacterController characterController;

    [HideInInspector] public Animator animator;

    [HideInInspector] public CharacterStats characterStats;
    
    // //摄像机的位置
    // [HideInInspector] public CameraController photographer;
    // //摄像机跟随的位置
    // [HideInInspector] public Transform followingTarget;

    [HideInInspector] public Camera MainCamera;
    
    //背包
    public Inventory myBag;

    public PanelManager PanelManager;

    private MoveController moveController;

    private AttackController attackController;

    public EquipController EquipController;

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
        MainCamera = Camera.main;

        MainSceneManager.Instance.PlayerConctroller = this;
        
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        characterStats = GetComponent<CharacterStats>();
        // photographer = GameObject.Find("photograther").GetComponent<CameraController>();
        // followingTarget = transform.GetChild(3);

        PanelManager = PanelManager.Instance;
        moveController = new MoveController();
        attackController = new AttackController();
        EquipController = new EquipController();
    }

    private void Start()
    {
        Init();
    }

    public void Update()
    {
        if (MainSceneManager.Instance.isTimeOut) return;
        
        if (characterStats.CurrentHealth == 0)
        {
            PlayerDeath();
            return;
        }
        moveController.PlayerAction();
        attackController.Attack();
        
    }

    private void Init()
    {
        GameFacade.Instance.PlayerName = "11";
        PlayerName = GameFacade.Instance.PlayerName;
        PlayerAttrib = DataManager.SelectUser(PlayerName);
        
        //将数据库中的数据装入游戏中
        characterStats.CurrentLevel = Convert.ToInt32(playerAttrib[5]);
        characterStats.CurrentExp = Convert.ToInt32(playerAttrib[6]);
        characterStats.CurrentHealth = Convert.ToInt32(playerAttrib[7]);
        characterStats.CurrentMana = Convert.ToInt32(playerAttrib[8]);
        characterStats.Money = Convert.ToInt32(playerAttrib[9]);
        
        
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void PlayerDeath()
    {
        isDeath = true;
        AudioManager.Instance.PlayAudio(5,"失败音效");
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