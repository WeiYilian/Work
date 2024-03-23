using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
    private static readonly string path = "LoginPanel";

    private string username;

    public LoginPanel() : base(new UIType(path))
    {
        username = null;
    }

    public LoginPanel(string username) : base(new UIType(path))
    {
        this.username = username;
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    public override void OnEnter()
    {
        UITool.FindChildGameObject("UserName").GetComponentInChildren<InputField>().text = username;
        //退出
        UITool.GetOrAddComponentInChildren<Button>("BtnExit").onClick.AddListener(Pop);
        //注册
        UITool.GetOrAddComponentInChildren<Button>("BtnRegister").onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayButtonAudio();
            Push(new RegisterPanel());
        });
        //登录
        UITool.GetOrAddComponentInChildren<Button>("BtnPlay").onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayButtonAudio();
            //检查账号密码
            string username = UITool.FindChildGameObject("UserName").GetComponentInChildren<InputField>().text;
            string password = UITool.FindChildGameObject("PassWord").GetComponentInChildren<InputField>().text;
            bool isLogin = DataManager.ClickLogin(username, password);
            if (isLogin)
            {
                Debug.Log("进入下一个场景");
                //PlayerPrefs.SetString("Player",username);
                GameFacade.Instance.PlayerName = username;
                SceneStateController.Instance.SetState(new MainScene());
            }
            else
            {
                Push((new NoticePanel("用户名或密码错误！！")));
            }
        });
    }
}
