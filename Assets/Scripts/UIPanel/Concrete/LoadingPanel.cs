using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPanel : BasePanel
{
    private static readonly string path = "UI/Panel/LoadingPanel";

    public LoadingPanel() : base(new UIType(path)) { }
    
    //加载的进度条
    private Image mLoadBar;
    private Text mLoadNumber;//加载的进度百分比
    private float WaitTime = 0;//和进度条绑定的时间  实时的进度条
    private float AllTime = 100;//等待的总时间

    public override void OnEnter()
    {
        AudioManager.Instance.PlayBGMAudio("Loading");
        mLoadBar = GameObject.Find("LoadBar").GetComponent<Image>();
        mLoadNumber = GameObject.Find("LoadNumber").GetComponent<Text>();
    }
}
