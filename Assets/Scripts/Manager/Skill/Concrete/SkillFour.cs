using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SkillFour : BaseSkill
{
    public SkillFour(Transform emitPoint):base(0f,emitPoint,false,null) { }
        
    public override void EmitSpecialEffects()
    {
        GameObject go = GameFacade.Instance.LoadSlash("Sword_Slash_A");
        go.transform.position = EmitPoint.position;
        go.transform.rotation = PlayerConctroller.Instance.characterController.transform.rotation;
        go.transform.Rotate(new Vector3(0,-90,0));
        GameObject.Destroy(go,1f);
    }
}
