using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillThree :BaseSkill
{
    public SkillThree(Image image) : base(8f,5f,-1f,image) { }

    private float timer;

    public override void EmitSpecialEffects()
    {
        GameObject go = ObjectPool.Instance.Get("Sword_Slash_3");
        go.transform.position = EmitPoint.transform.position + Vector3.up * 0.5f;
        go.transform.rotation = playerConctroller.characterController.transform.rotation;
        ObjectPool.Instance.Remove("Sword_Slash_3",go,1f);
        
        AudioManager.Instance.PlayAudio(2,"Skill3");
        
        base.EmitSpecialEffects();
    }

    public override void SkillHit(CharacterStats playerStats, CharacterStats enemyStats)
    {
         //十倍伤害
         enemyStats.SkillTakeDamage(playerStats, enemyStats,10);
    }
}
