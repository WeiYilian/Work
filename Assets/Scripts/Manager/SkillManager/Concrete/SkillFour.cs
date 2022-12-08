using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SkillFour : BaseSkill
{
    public SkillFour():base(0f,3f,0.5f,null) { }
        
    public override void EmitSpecialEffects()
    {
        GameObject go = ObjectPool.Instance.Get("Sword_Slash_A");
        go.transform.position = EmitPoint.transform.position + Vector3.up * 0.5f;
        go.transform.rotation = PlayerConctroller.Instance.characterController.transform.rotation;
        go.transform.Rotate(new Vector3(0,-90,0));
        ObjectPool.Instance.Remove("Sword_Slash_A",go,1f);
        
        //TODO:重击声音
        
        base.EmitSpecialEffects();
    }
    
    public override void SkillHit(CharacterStats playerStats, CharacterStats enemyStats)
    {
        //双倍伤害
        enemyStats.SkillTakeDamage(playerStats, enemyStats,2);
    }
}
