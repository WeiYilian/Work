using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillOne : BaseSkill
{
    public SkillOne(Transform emitPoint,Image image):base(5f,emitPoint,false,image) { }

    public override void EmitSpecialEffects()
    {
        GameObject go = GameFacade.Instance.LoadSlash("Sword_Slash_1");
        go.transform.position = EmitPoint.position;
        go.transform.rotation = MoveConctroller.Instance.characterController.transform.rotation;
        GameObject.Destroy(go,1f);
    }
}
