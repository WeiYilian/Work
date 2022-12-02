using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Main场景
/// </summary>
public class MainScene : SceneState
{
    public MainScene(string sceneName, SceneStateController stateController) : base("Main", stateController) { }

    // ReSharper disable Unity.PerformanceAnalysis
    public override void StateStart()
    {
        if (SceneManager.GetActiveScene().name != "Main"/*如果当前的场景名不为sceneName*/)
        {
            SceneManager.LoadScene("Main");//加载名为sceneName的场景
            SceneManager.sceneLoaded += SceneLoaded;
        }
        else
        {
            PanelManager.Instance.Push(new MainPanel());
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
        
    }
}
