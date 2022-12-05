using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController
{
    private Animator animator;

    private CharacterController characterController;
   
    private CameraController photographer;
    
    private Transform followingTarget;
    
    private PlayerConctroller PlayerConctroller;
    
    public MoveController()
    {
        PlayerConctroller = PlayerConctroller.Instance;
        
        characterController = PlayerConctroller.characterController;
        animator = PlayerConctroller.animator;
        photographer = PlayerConctroller.photographer;
        followingTarget = PlayerConctroller.followingTarget;
        
        photographer.InitCamera(followingTarget);
    }
    
    
    #region 基础移动参数

    [Header("基础移动参数")]
    //基本速度
    private float speed;
    //行走的速度
    private float mWlkSpeed = 2f; 
    //奔跑时的速度
    private float mRunSpeed = 4f;
    //设置翻滚的速度
    private float rollSpeed = 6f;
    //跳起来的速度，其实是跳起来的一个高度
    private float jumpSpeed = 4.0f; 
    //设置重力的大小
    private float gravity = 10.0f; 
    //镜头旋转角度
    private Quaternion rot;
    //镜头旋转的速度
    private float rotateSpeed = 5.0f;
    //以玩家自身为坐标系的移动，用以移动动画的播放
    private Vector3 animationMove = Vector3.zero;
    //用以承载角色移动向量
    public Vector3 moveDirection = Vector3.zero;
   
    //判断是否在慢行
    public bool isRun;
    
    #endregion

    #region 玩家基础移动

    /// <summary>
    /// 玩家行为
    /// </summary>
    public void PlayerAction()
    {
        //如果挨打，直接返回
        if (PlayerConctroller.isHit) return;
        
        //改变移动速度
        if (animator.GetCurrentAnimatorStateInfo(1).IsName("Roll"))
            speed = rollSpeed;
        else
        {
            if (isRun)
            {
                if (Math.Abs(speed - 4) > 0)
                {
                    speed = Mathf.Lerp(speed, mRunSpeed, 5f * Time.deltaTime);
                }
            }
            else
            {
                if (Math.Abs(speed - 2) > 0)
                {
                    speed = Mathf.Lerp(speed, mWlkSpeed, 5f * Time.deltaTime);
                }
            }
        }
        
        if (characterController.isGrounded)
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
        
        moveDirection.y -= gravity * Time.deltaTime; //模拟重力

        if (PlayerConctroller.canMove) characterController.Move(moveDirection * (speed * Time.deltaTime));

        SwitchAnimation(animationMove);
    }
    
    /// <summary>
    /// 基础移动
    /// </summary>
    void BaseMove()
    {
        float curX = PlayerConctroller.canMove ? Input.GetAxis("Vertical") : 0;
        float curY = PlayerConctroller.canMove ? Input.GetAxis("Horizontal") : 0;
        rot = Quaternion.Euler(0,photographer.Yaw,0);
        moveDirection = rot * Vector3.forward * curX + rot * Vector3.right * curY;
        if(moveDirection != Vector3.zero || PlayerConctroller.isAttack)
            PlayerConctroller.transform.rotation = Quaternion.Slerp(PlayerConctroller.transform.rotation, rot, rotateSpeed * Time.deltaTime);//顺滑转向
        animationMove = Vector3.forward * (curX * speed) + Vector3.right * (curY * speed);
    }

    /// <summary>
    /// 播放动画
    /// </summary>
    /// <param name="moveDirection"></param>
    public void SwitchAnimation(Vector3 moveDirection)
    {
        animator.SetFloat("Walk", moveDirection.x);
        animator.SetFloat("Turn", moveDirection.z);
    }

    #endregion
    
    
}
