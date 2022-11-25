using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTwo : BaseSkill
{
    public SkillTwo(Transform emitPoint,Image image):base(10f,emitPoint,false,image) { }

    public override void EmitSpecialEffects()
    {
        GameObject go = GameFacade.Instance.LoadSlash("Sword_Slash_2");
        go.transform.position = EmitPoint.position;
        go.transform.rotation = PlayerConctroller.Instance.characterController.transform.rotation;
        go.GetComponentInChildren<ParticleSystem>().Play();
        GameObject.Destroy(go,1f);
    }
}
