using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserMessagePanel : BasePanel
{
    private static readonly string path = "UI/Panel/UserMessagePanel";/*readonly：声明字段，表示声明的字段只能在声明时或同一个类的构造函数当中进行赋值*/
    
    public UserMessagePanel():base(new UIType(path)){}

    private InputField username;
    private InputField password;
    private InputField age;

    public override void OnEnter()
    {
        Init();
        
        UITool.GetOrAddComponentInChildren<Button>("BtnExit").onClick.AddListener(Pop);
        
        UITool.GetOrAddComponentInChildren<Button>("BtnConfirm").onClick.AddListener(() =>
        {
            BtnConfirm();
            DataManager.UpdataUserMessage(PlayerConctroller.Instance.PlayerAttrib);
            Pop();
            Push(new NoticePanel("修改完成"));
        });
    }

    private void BtnConfirm()
    {
        if (username != null)
            PlayerConctroller.Instance.PlayerAttrib[1] = username.text;
        if (password != null)
            PlayerConctroller.Instance.PlayerAttrib[2] = password.text;
        if (age != null)
            PlayerConctroller.Instance.PlayerAttrib[3] = age.text;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void Init()
    {
        GameObject settingPanel = GameObject.Find("Canvas/UserMessagePanel");
        
        username = settingPanel.transform.Find("Content/UserName/InputField").GetComponent<InputField>();
        password = settingPanel.transform.Find("Content/Password/InputField").GetComponent<InputField>();
        age = settingPanel.transform.Find("Content/Age/InputField").GetComponent<InputField>();
        
        username.text = PlayerConctroller.Instance.PlayerAttrib[1];
        password.text = PlayerConctroller.Instance.PlayerAttrib[2];
        age.text = PlayerConctroller.Instance.PlayerAttrib[3];
    }
}
