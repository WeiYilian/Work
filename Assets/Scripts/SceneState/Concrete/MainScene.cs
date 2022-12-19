using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Main场景
/// </summary>
public class MainScene : SceneState
{
    public MainScene() : base("Main") { }

    // ReSharper disable Unity.PerformanceAnalysis
    public override void StateStart()
    {
        AudioManager.Instance.StopAudio(0);

        if (SceneManager.GetActiveScene().name != "Main"/*如果当前的场景名不为sceneName*/)
        {
            SceneManager.LoadScene("Main");//加载名为sceneName的场景
        }
        PanelManager.Instance.Push(new MainPanel());
        Init();
    }

    public override void StateEnd()
    {
        PanelManager.Instance.PopAll();
    }

    public void Init()
    {
        ObjectPool.Instance.Init("Health",GameFacade.Instance.LoadGameObject("Health"),10);
        ObjectPool.Instance.Init("Mage",GameFacade.Instance.LoadGameObject("Mage"),5);
        ObjectPool.Instance.Init("Warrior",GameFacade.Instance.LoadGameObject("Warrior"),10);
        
        
        ObjectPool.Instance.Init("Sword_Slash_A",GameFacade.Instance.LoadGameObject("Sword_Slash_A"),1);
        ObjectPool.Instance.Init("Sword_Slash_1",GameFacade.Instance.LoadGameObject("Sword_Slash_1"),1);
        ObjectPool.Instance.Init("Sword_Slash_2",GameFacade.Instance.LoadGameObject("Sword_Slash_2"),1);
        ObjectPool.Instance.Init("Sword_Slash_3",GameFacade.Instance.LoadGameObject("Sword_Slash_3"),1);
        ObjectPool.Instance.Init("MageSkill",GameFacade.Instance.LoadGameObject("MageSkill"),1);
        ObjectPool.Instance.Init("MageAttack",GameFacade.Instance.LoadGameObject("MageAttack"),1);
        ObjectPool.Instance.Init("UPLevel",GameFacade.Instance.LoadGameObject("UPLevel"),1);
        ObjectPool.Instance.Init("Treat",GameFacade.Instance.LoadGameObject("Treat"),1);
    }
}
