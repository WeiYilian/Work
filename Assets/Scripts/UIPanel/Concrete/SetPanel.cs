using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPanel : BasePanel
{
    private static readonly string path = "UI/Panel/SetPanel";
    
    public SetPanel():base(new UIType(path)){}

    public override void OnEnter()
    {
        UITool.GetOrAddComponentInChildren<Button>("BtnExit").onClick.AddListener(Pop);
        
        UITool.GetOrAddComponentInChildren<Button>("Archive").onClick.AddListener(() =>
        {
            DataManager.UpdataUserMessage(PlayerConctroller.Instance.PlayerAttrib);
            Push(new NoticePanel("保存成功"));
        });
        
        UITool.GetOrAddComponentInChildren<Button>("MainMenu").onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayButtonAudio();
            SceneStateController.Instance.SetState(new StartScene());
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
        
        UITool.GetOrAddComponentInChildren<Button>("Setting").onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayButtonAudio();
            Push(new SettingPanel());
        });
    }

    public override void OnUpdata()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pop();
        }
    }
}
