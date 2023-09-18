using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserManagerPanel : BasePanel
{
    private static readonly string path = "UserManagerPanel";
    
    public UserManagerPanel():base(new UIType(path)){}

    public override void OnEnter()
    {
        UITool.GetOrAddComponentInChildren<Button>("BtnExit").onClick.AddListener(Pop);
    }
}
