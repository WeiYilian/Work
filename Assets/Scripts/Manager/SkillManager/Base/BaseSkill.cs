using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseSkill
{
    protected GameObject Player { get; private set; }
    
    protected PlayerConctroller playerConctroller { get; private set; }

    public float SkillCd { get; protected set; }
    
    protected float SkillRange { get;  set; }
    
    protected float SkillAngle { get; set; }

    public GameObject EmitPoint { get; set; }
    
    public bool IsCooling { get; set; }

    public Image Image { get; set; }

    public BaseSkill(float skillCd,float skillRange,float skillAngle,Image image)
    {
        playerConctroller = MainSceneManager.Instance.PlayerConctroller;
        Player = playerConctroller.gameObject;
        SkillCd = skillCd;
        SkillRange = skillRange;
        SkillAngle = skillAngle;
        EmitPoint = Player;
        IsCooling = false;
        Image = image;
    }

    /// <summary>
    /// 释放特效
    /// </summary>
    public virtual void EmitSpecialEffects()
    {
        SkillDetermination();
    }
    
    /// <summary>
    /// 技能判定
    /// </summary>
    public void SkillDetermination()
    {
        CharacterStats playerCharacterStats = Player.GetComponentInChildren<CharacterStats>();
        
        var colliders = Physics.OverlapSphere(Player.transform.position, SkillRange);

        foreach (var enemy in colliders)
        {
            if (enemy.GetComponent<IEnemy>() != null && Player.transform.IsFacingTarget(enemy.transform,SkillAngle)/*扩展方法*/)
            {
                var targetStats = enemy.GetComponent<CharacterStats>();
                SkillHit(playerCharacterStats, targetStats);
            }
        }
    }

    /// <summary>
    /// 这里加不同技能需要的不同效果
    /// </summary>
    public abstract void SkillHit(CharacterStats playerStats,CharacterStats enemyStats);
}
