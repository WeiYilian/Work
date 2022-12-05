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
        AudioManager.Instance.StopAudio(0);

        if (SceneManager.GetActiveScene().name != "Main"/*如果当前的场景名不为sceneName*/)
        {
            SceneManager.LoadScene("Main");//加载名为sceneName的场景
        }
        else
        {
            PanelManager.Instance.Push(new MainPanel());
        }
    }

    public override void StateEnd()
    {
        PanelManager.Instance.PopAll();
    }

    
}
