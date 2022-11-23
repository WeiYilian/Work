using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterPanel : BasePanel
{
    private static readonly string path = "UI/Panel/RegisterPanel";
    
    public RegisterPanel():base(new UIType(path)){}

    public override void OnEnter()
    {
        UITool.GetOrAddComponentInChildren<Button>("BtnExit").onClick.AddListener(() =>
        {
            PanelManager.Pop();
        });
        UITool.GetOrAddComponentInChildren<Button>("BtnRegister").onClick.AddListener(() =>
        {
            //在框架外使用一个委托用来接提示框
            //GameLoop.Instance.Push((new NoticePanel(" ")));
            //将账号密码加入数据库
            string username = UITool.FindChildGameObject("UserName").GetComponentInChildren<InputField>().text;
            string password = UITool.FindChildGameObject("PassWord").GetComponentInChildren<InputField>().text;
            string age = UITool.FindChildGameObject("Age").GetComponentInChildren<InputField>().text;
            string gender = UITool.FindChildGameObject("Gender").transform.GetChild(1).GetComponentInChildren<Text>().text;
            Debug.Log(gender);
            bool isRegister = DataManager.ClickRegister(username, password,age,gender);
            if (isRegister)
            {
                GameLoop.Instance.Push((new NoticePanel("注册成功")));
            }
            else if (username == "" || password == "" || age == "" || gender == "")
            {
                GameLoop.Instance.Push((new NoticePanel("请填写完信息！！")));
            }
            else
            {
                GameLoop.Instance.Push((new NoticePanel("注册失败，用户名已被使用")));
            }
        });
    }
}
