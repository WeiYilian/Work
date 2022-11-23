using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseSkill
{
    public float SkillCd { get; protected set; }

    public Transform EmitPoint { get; set; }
    
    public bool IsCooling { get; set; }

    public Image Image { get; set; }

    public BaseSkill(float skillCd,Transform emitPoint,bool isCooling,Image image)
    {
        SkillCd = skillCd;
        EmitPoint = emitPoint;
        IsCooling = isCooling;
        Image = image;
    }

    public abstract void EmitSpecialEffects();
}
