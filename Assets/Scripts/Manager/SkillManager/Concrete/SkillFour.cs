using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SkillFour : BaseSkill
{
    public SkillFour():base(0f,3f,0.5f,null) { }
        
    public override void EmitSpecialEffects()
    {
        GameObject go = GameFacade.Instance.LoadSlash("Sword_Slash_A");
        go.transform.position = EmitPoint.transform.position + Vector3.up * 0.5f;
        go.transform.rotation = PlayerConctroller.Instance.characterController.transform.rotation;
        go.transform.Rotate(new Vector3(0,-90,0));
        GameObject.Destroy(go,1f);
        
        base.EmitSpecialEffects();
    }
    
    public override void SkillHit(CharacterStats playerStats, CharacterStats enemyStats)
    {
        //第四下攻击增加 5 点真实伤害
        enemyStats.characterData.currentHealth -= 5;
    }
}
