using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillThree :BaseSkill
{
    
    
    public SkillThree(Transform emitPoint,Image image) : base(8f,emitPoint,false,image) { }

    public override void EmitSpecialEffects()
    {
        GameObject go = GameFacade.Instance.LoadSlash("Sword_Slash_3");
        go.transform.position = EmitPoint.position;
        go.transform.rotation = PlayerConctroller.Instance.characterController.transform.rotation;
        GameObject.Destroy(go,2f);
    }
}
