using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class AttackController
{
    private Animator animator;

    private CharacterStats characterStats;

    private PlayerConctroller PlayerConctroller;
    
    public AttackController()
    {
        PlayerConctroller = PlayerConctroller.Instance;
        
        animator = PlayerConctroller.animator;
        characterStats = PlayerConctroller.characterStats;
        Skills = new List<BaseSkill>();
        MainPanel.PlayerInit += Init;
    }

    public void Init()
    {
        Skills.Add(new SkillOne(PlayerConctroller.PanelManager.MainPanel().skill1));
        Skills.Add(new SkillTwo(PlayerConctroller.PanelManager.MainPanel().skill2));
        Skills.Add(new SkillThree(PlayerConctroller.PanelManager.MainPanel().skill3));
        Skills.Add(new SkillFour());
    }

    #region 普通攻击参数设置
    
    [Header("攻击参数设置")]
    //连击次数
    public int comboStep;
    //允许连击的时间
    public float interval = 2f;
    //计时器
    private float timer;

    #endregion

    #region 技能攻击参数设置

    //技能列表，存储技能
    private List<BaseSkill> Skills;

    //计时器
    private float cd1Time;
    private float cd2Time;
    private float cd3Time;

    #endregion

    /// <summary>
    /// 玩家攻击
    /// </summary>
    public void Attack()
    {
        //动作被打断
        if (PlayerConctroller.isHit)
        {
            comboStep = 0;
            return;
        }

        SkillTrigger();

        NormalAttack();
    }
    
    #region 普通攻击
    
    /// <summary>
    /// 普通攻击
    /// </summary>
    public void NormalAttack()
    {
        if (Input.GetMouseButtonDown(0) && !PlayerConctroller.isAttack && !animator.GetCurrentAnimatorStateInfo(0).IsName("jump"))
        {
            PlayerConctroller.isAttack = true;
            //连击次数递增
            comboStep++;
            if (comboStep > 4)
                comboStep = 1;
            timer = interval;
            animator.SetTrigger("Attack");
            animator.SetInteger("ComboStep",comboStep);
        }
        else if (Input.GetMouseButtonDown(0) && !PlayerConctroller.isAttack && animator.GetCurrentAnimatorStateInfo(0).IsName("jump"))
        {
            animator.SetTrigger("JumpAttack");
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

    #endregion

    #region 技能攻击

    // ReSharper disable Unity.PerformanceAnalysis
    /// <summary>
    /// 检测是否触发技能
    /// </summary>
    public void SkillTrigger()
    {
        if (Input.GetKeyUp(KeyCode.Q) && !PlayerConctroller.isAttack && !Skills[0].IsCooling)
        {
            PlayerConctroller.isAttack = true;
            Skills[0].Image.fillAmount = 1;
            animator.SetTrigger("SkillOne");
        }
        if (Input.GetKeyUp(KeyCode.E) && !PlayerConctroller.isAttack && !Skills[1].IsCooling)
        {
            PlayerConctroller.isAttack = true;
            Skills[1].Image.fillAmount = 1;
            animator.SetTrigger("SkillTwo");
        }
        if (Input.GetKeyUp(KeyCode.R) && !PlayerConctroller.isAttack && !Skills[2].IsCooling)
        {
            PlayerConctroller.isAttack = true;
            Skills[2].Image.fillAmount = 1;
            animator.SetTrigger("SkillThree");
        }
        
        CdDecline(0,ref cd1Time);
        CdDecline(1,ref cd2Time);
        CdDecline(2,ref cd3Time);
    }

    /// <summary>
    /// 发出技能后冷却
    /// </summary>
    /// <param name="skill"></param>
    /// <param name="cd"></param>
    /// <param name="index"></param>
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
    #endregion

    #region 动画事件
    
    /// <summary>
    /// 动画事件，技能释放
    /// </summary>
    public void AttackEffection(int index)
    {
        Skills[index].EmitSpecialEffects();
    }
    
    /// <summary>
    /// 普通伤害计算
    /// </summary>
    public void Hit()
    {
        var colliders = Physics.OverlapSphere(PlayerConctroller.Instance.transform.position, characterStats.characterData.attackRange);

        foreach (var enemy in colliders)
        {
           
            if (enemy.GetComponent<IEnemy>() != null && PlayerConctroller.Instance.transform.IsFacingTarget(enemy.transform)/*扩展方法*/)
            {
                AudioManager.Instance.PlayAudio(2,"Attack1",1);
                var targetStats = enemy.GetComponent<CharacterStats>();
                targetStats.TakeDamage(characterStats, targetStats);
            }
            else
                AudioManager.Instance.PlayAudio(2,"SwingEmpty",0.5f);
        }
    }
    
    /// <summary>
    /// 动画事件，玩家攻击结束
    /// </summary>
    public void AttackOver()
    {
        PlayerConctroller.isAttack = false;
    }


    #endregion
}
