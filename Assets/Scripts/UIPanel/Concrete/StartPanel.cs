using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 开始主面板
/// </summary>
public class StartPanel : BasePanel
{
    private static readonly string path = "UI/Panel/StartPanel"; /*readonly：声明字段，表示声明的字段只能在声明时或同一个类的构造函数当中进行赋值*/

    public StartPanel() : base(new UIType(path))
    {
    }

    public override void OnEnter()
    {
        UITool.GetOrAddComponentInChildren<Button>("BtnSetting").onClick.AddListener(() =>
        {
            //点击事件加到此处
            Push(new SettingPanel());
        }); 
        UITool.GetOrAddComponentInChildren<Button>("BtnPlay").onClick.AddListener(() =>
        {
            //GameLoop.Instance.SceneSystem.SetScene(new MainScene());
            //打开登录面板
            Push(new LoginPanel());
        });
        UITool.GetOrAddComponentInChildren<Button>("BtnUserManager").onClick.AddListener(() =>
        {
            //打开账号管理面板
            Push(new UserManagerPanel());
        });
    }
}
