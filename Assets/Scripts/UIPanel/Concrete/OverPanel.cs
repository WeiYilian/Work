using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverPanel : BasePanel
{
    private static readonly string path = "OverPanel";

    private string message;

    public OverPanel(string message) : base(new UIType(path))
    {
        this.message = message;
    }

    public override void OnEnter()
    {
        GameObject.Find("Canvas").transform.Find("OverPanel/Text").GetComponent<Text>().text = message;
        
        UITool.GetOrAddComponentInChildren<Button>("Resume").onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayButtonAudio();
            SceneStateController.Instance.SetState(new StartScene());
        });
        
        UITool.GetOrAddComponentInChildren<Button>("Quit").onClick.AddListener(() =>
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
