using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTwo : BaseSkill
{
    public SkillTwo(Image image):base(10f,8f,0.9f,image) { }

    
    
    public override void EmitSpecialEffects()
    {
        GameObject go = ObjectPool.Instance.Get("Sword_Slash_2");
        go.transform.position = EmitPoint.transform.position;
        go.transform.rotation = PlayerConctroller.Instance.characterController.transform.rotation;
        go.GetComponentInChildren<ParticleSystem>().Play();
        ObjectPool.Instance.Remove("Sword_Slash_2",go,1f); 
        
        //TODO:重击声音
        
        base.EmitSpecialEffects();
    }

    public override void SkillHit(CharacterStats playerStats, CharacterStats enemyStats)
    {
        enemyStats.TakeDamage(playerStats, enemyStats);
        //技能四，眩晕敌人1.5秒
        enemyStats.GetComponent<Animator>().SetTrigger("Dizzy");//播放敌人眩晕动画
        
    }
}
