using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmPanel : BasePanel
{
    private static readonly string path = "UI/Panel/ConfirmPanel";

    private string message;
    public ConfirmPanel(string message) : base(new UIType(path))
    {
        this.message = message;
    }

    public override void OnEnter()
    {
        GameObject.Find("ConfirmPanel").transform.GetChild(0).GetComponent<Text>().text = message;
        
        UITool.GetOrAddComponentInChildren<Button>("BtnExit").onClick.AddListener(Pop);
        
        UITool.GetOrAddComponentInChildren<Button>("Confirm").onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayButtonAudio();
            //任务面板
            Pop();
            TaskManager.Instance.Tasks[0].IsAcceptTask = true;
            PanelManager.Push(new NoticePanel("任务接受成功！"));
        });
    }
}
