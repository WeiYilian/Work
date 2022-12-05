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

    public StartPanel() : base(new UIType(path)) { }

    private bool isMute; 
    
    public override void OnEnter()
    {
        AudioManager.Instance.PlayBGMAudio("Start");
        
        UITool.GetOrAddComponentInChildren<Button>("BtnVoice").onClick.AddListener(() =>
        {
            isMute = !isMute;
            if (!isMute)
                GameObject.Find("StartPanel").transform.Find("BtnVoice").GetComponent<Image>().sprite =
                    GameFacade.Instance.LoadSprite("audio_on");
            else
                GameObject.Find("StartPanel").transform.Find("BtnVoice").GetComponent<Image>().sprite =
                    GameFacade.Instance.LoadSprite("audio_mute");
            AudioManager.Instance.IsMute = isMute;
        });
        
        UITool.GetOrAddComponentInChildren<Button>("BtnPlay").onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayButtonAudio();
            //打开登录面板
            Push(new LoginPanel());
        });
        UITool.GetOrAddComponentInChildren<Button>("BtnUserManager").onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayButtonAudio();
            //打开账号管理面板
            Push(new UserManagerPanel());
        });
        
        UITool.GetOrAddComponentInChildren<Button>("ExitGame").onClick.AddListener(() =>
       {
           AudioManager.Instance.PlayButtonAudio();
           #if UNITY_EDITOR
           UnityEditor.EditorApplication.isPlaying = false;//在编辑器中退出
           #else
           Application.Quit();//在打包之后的退出游戏
           #endif
       }); 
    }
}
