using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadScene : SceneState
{
    public LoadScene(SceneStateController sceneStateController) : base("Loading", sceneStateController) { }

    //加载的进度条
    private Image mLoadBar;
    private Text mLoadNumber;//加载的进度百分比
    private float WaitTime = 0;//和进度条绑定的时间  实时的进度条
    private float AllTime = 10;//等待的总时间

    public override void StateStart()
    {
        AudioManager.Instance.PlayBGMAudio("Loading");
        mLoadBar = GameObject.Find("LoadBar").GetComponent<Image>();
        mLoadNumber = GameObject.Find("LoadNumber").GetComponent<Text>();
    }

    public override void StateUpdate()
    {
        WaitTime += Time.deltaTime * 20;
        mLoadBar.fillAmount = WaitTime / AllTime;
        mLoadNumber.text = ((int) WaitTime).ToString() + "%";
        if (WaitTime >= AllTime)
        {
            //进入游戏场景
            Debug.Log("进入游戏");
            mController.SetState(new MainScene("Main",mController));
        }
    }
}
