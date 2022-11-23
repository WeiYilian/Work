using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : BasePanel
{
    private static readonly string path = "UI/Panel/SettingPanel";/*readonly：声明字段，表示声明的字段只能在声明时或同一个类的构造函数当中进行赋值*/
    
    public SettingPanel():base(new UIType(path)){}

    public override void OnEnter()
    {
        UITool.GetOrAddComponentInChildren<Button>("BtnExit").onClick.AddListener(Pop);
    }
}
