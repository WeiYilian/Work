using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 管理全局东西
/// </summary>
public class GameLoop : MonoBehaviour
{
    public static GameLoop Instance { get; private set; }
    /// <summary>
    /// 场景管理器
    /// </summary>
    public SceneStateController controller { get; private set; }

    /// <summary>
    /// 显示面板  可以在框架以外的地方显示面板
    /// </summary>
    public UnityAction<BasePanel> Push { get; private set; }
    
    /// <summary>
    /// 设置Push
    /// </summary>
    /// <param name="push"></param>
    public void SetAction(UnityAction<BasePanel> push)
    {
        Push = push;
    }

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        controller = new SceneStateController();
        
        DontDestroyOnLoad(gameObject);//场景跳转之后不销毁该游戏物体
    }

    private void Start()
    {
        controller.SetState(new StartScene(controller),false);
    }
    
    private void Update()
    {
        //在不同的状态下，需要更新的数据是不一样的
        if (controller != null)
        {
            //状态更新的方法
            controller.StateUpdate();
        }
    }

   
}
