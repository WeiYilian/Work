using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserMessagePanel : BasePanel
{
    private static readonly string path = "UI/Panel/UserMessagePanel";/*readonly：声明字段，表示声明的字段只能在声明时或同一个类的构造函数当中进行赋值*/
    
    public UserMessagePanel():base(new UIType(path)){}

    private string username;
    private string password;
    private string age;

    public override void OnEnter()
    {
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
        GameObject settingPanel = GameObject.Find("Canvas/SettingPanel");
        Debug.Log(settingPanel);
        Debug.Log(settingPanel.transform.Find("Content/UserName/InputField"));
        username = settingPanel.transform.Find("Content/UserName/InputField/Text").GetComponent<Text>().text;
        password = settingPanel.transform.Find("Content/Password/InputField/Text").GetComponent<Text>().text;
        age = settingPanel.transform.Find("Content/Age/InputField/Text").GetComponent<Text>().text;
        if (username != null)
            PlayerConctroller.Instance.PlayerAttrib[1] = username;
        if (password != null)
            PlayerConctroller.Instance.PlayerAttrib[2] = password;
        if (age != null)
            PlayerConctroller.Instance.PlayerAttrib[3] = age;
    }
}
