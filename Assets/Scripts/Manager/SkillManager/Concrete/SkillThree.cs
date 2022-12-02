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
        GameObject go = GameFacade.Instance.LoadSlash("Sword_Slash_3");
        go.transform.position = EmitPoint.transform.position + Vector3.up * 0.5f;
        go.transform.rotation = PlayerConctroller.Instance.characterController.transform.rotation;
        GameObject.Destroy(go,2f);
        
        base.EmitSpecialEffects();
    }

    public override void SkillHit(CharacterStats playerStats, CharacterStats enemyStats)
    {
         //十倍伤害
         for (int i = 0; i < 9; i++) 
         {
            enemyStats.TakeDamage(playerStats, enemyStats); 
         }
                
    }
}
