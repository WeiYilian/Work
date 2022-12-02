using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 开始场景
/// </summary>
public class StartScene : SceneState
{
    public StartScene(SceneStateController sceneStateController) : base("Start", sceneStateController) { }

    public override void StateStart()
    {
        if (SceneManager.GetActiveScene().name != "Start"/*如果当前的场景名不为sceneName*/)
        {
            SceneManager.LoadScene("Start");//加载名为sceneName的场景
            PanelManager.Instance.Push(new StartPanel());
        }
        else
        {
            PanelManager.Instance.Push(new StartPanel());
        }
    }

    public override void StateEnd()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
        PanelManager.Instance.PopAll();
    }

    /// <summary>
    /// 场景加载后执行的方法
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="loadSceneMode"></param>
    private void SceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        //PanelManager.Instance.Push(new StartPanel());
    }
}
