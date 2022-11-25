using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(CharacterController))]
public class PlayerConctroller : MonoBehaviour
{
    public static PlayerConctroller Instance;

    [HideInInspector] public CharacterController characterController;
    [HideInInspector] public Animator animator;
    [HideInInspector] public AttactController attactController;
    
    private void Awake()
    {
        Instance = this;
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        attactController = GetComponentInChildren<AttactController>();
    }

    #region 基础移动参数

    [Header("基础移动参数")]
    //基本速度
    private float speed;
    //行走的速度
    private float walkSpeed = 2f; 
    //奔跑时的速度
    private float runSpeed = 4f;
    //跳起来的速度，其实是跳起来的一个高度
    private float jumpSpeed = 4.0f; 
    //设置重力的大小
    private float gravity = 10.0f; 
    //设置翻滚的速度
    private float rollSpeed = 6f;
    //镜头旋转角度
    private Quaternion rot;
    //镜头旋转的速度
    private float rotateSpeed = 4.0f;
    //以玩家自身为坐标系的移动，用以移动动画的播放
    private Vector3 animationMove = Vector3.zero;
    //用以承载角色移动向量
    [HideInInspector] public Vector3 moveDirection = Vector3.zero;
    //判断是否可以移动
    [HideInInspector] public bool canMove = true;
    //判断是否在慢行
    [HideInInspector] public bool isRun;
    //判断是否被击打
    [HideInInspector] public bool isHit;

    [SerializeField] private CameraController photographer;

    [SerializeField] private Transform followingTarget;

    #endregion

    void Start()
    {
        photographer.InitCamera(followingTarget);
    }

    void Update()
    {
       PlayerAction();
       attactController.Attack();
    }

    #region 玩家基础移动

    /// <summary>
        /// 玩家行为
        /// </summary>
        public void PlayerAction()
        {
            //如果挨打，直接返回
            if (isHit) return;
            //改变移动速度
            if (animator.GetCurrentAnimatorStateInfo(1).IsName("Roll"))
                speed = rollSpeed;
            else
                speed = isRun ? runSpeed : walkSpeed;
            
            if (characterController.isGrounded && canMove)
            {
                BaseMove();
                //奔跑
                if (Input.GetKey(KeyCode.LeftShift))
                    isRun = true;
                else
                    isRun = false;
                //跳跃
                if (Input.GetButton("Jump") && !animator.GetCurrentAnimatorStateInfo(0).IsName("jump"))
                {
                    moveDirection.y = jumpSpeed;
                    animator.SetTrigger("Jump");
                }
                //翻滚
                if (Input.GetMouseButtonDown(1) && moveDirection != Vector3.zero && !animator.GetCurrentAnimatorStateInfo(1).IsName("Roll"))
                {
                    animator.SetTrigger("Roll");
                }
            }
    
    
            //模拟重力
            moveDirection.y -= gravity * Time.deltaTime;
            
            if (attactController.isAttack) attactController.AttackMove(ref moveDirection);
            
            if(canMove) characterController.Move(moveDirection * (speed * Time.deltaTime));
            
            SwitchAnimation(animationMove);
        }
    
         /// <summary>
        /// 基础移动
        /// </summary>
        void BaseMove()
        {
            float curSpeedX = canMove ? Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? Input.GetAxis("Horizontal") : 0;
            rot = Quaternion.Euler(0,photographer.Yaw,0);
            moveDirection = rot * Vector3.forward * curSpeedX + rot * Vector3.right * curSpeedY;
            if(moveDirection != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotateSpeed * Time.deltaTime);//顺滑转向
            moveDirection = Vector3.ClampMagnitude(moveDirection, 1);
            animationMove = Vector3.forward * (curSpeedX * speed) + Vector3.right * (curSpeedY * speed);
        }
         
        /// <summary>
        /// 播放动画
        /// </summary>
        /// <param name="moveDirection"></param>
        public void SwitchAnimation(Vector3 moveDirection)
        {
            if (!isRun)
            {
                moveDirection.x = Mathf.Clamp(moveDirection.x, -2f,2f);
                moveDirection.z = Mathf.Clamp(moveDirection.z, -2f,2f);
            }
            animator.SetFloat("Walk", moveDirection.x);
            animator.SetFloat("Turn", moveDirection.z);
        }
    
       

    #endregion
    
    
}