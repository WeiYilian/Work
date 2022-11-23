using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class NoticePanel : BasePanel
{
    private static readonly string path = "UI/Panel/NoticePanel";

    private string message;
    public NoticePanel(string message) : base(new UIType(path))
    {
        this.message = message;
    }
    
    public override void OnEnter()
    {
        GameObject.Find("NoticePanel").transform.GetChild(0).GetComponent<Text>().text = message;
        
        UITool.GetOrAddComponent<Button>().onClick.AddListener(Pop);
    }
}
