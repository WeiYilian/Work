using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillOne : BaseSkill
{
    public SkillOne(Image image) : base(5f, 14f,0.8f,image) { }

    public override void EmitSpecialEffects()
    {
        GameObject go = GameFacade.Instance.LoadSlash("Sword_Slash_1");
        go.transform.position = EmitPoint.transform.position + Vector3.up * 0.5f;
        go.transform.rotation = PlayerConctroller.Instance.characterController.transform.rotation;
        GameObject.Destroy(go,1f);
        
        base.EmitSpecialEffects();
    }

    public override void SkillHit(CharacterStats playerStats, CharacterStats enemyStats)
    {
        //技能一：吸取总血量的10%
        int absorbHealth = (int) (playerStats.characterData.maxHealth * 0.1f);
        playerStats.characterData.currentHealth = Mathf.Min(playerStats.characterData.currentHealth + absorbHealth,playerStats.characterData.maxHealth);
    }
}
