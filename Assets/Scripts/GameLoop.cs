using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

/// <summary>
/// 管理全局东西
/// </summary>
public class GameLoop : MonoBehaviour
{
    public static GameLoop Instance { get; private set; }
    
    

    private void Awake()
    {
        var find = GameObject.Find("GameLoop");

        if (find == gameObject)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);//场景跳转之后不销毁该游戏物体
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        //SceneStateController.Instance.SetState(new StartScene(),false);
        SceneStateController.Instance.SetState(new MainScene(),false);
    }

    private void Update()
    {
        //在不同的状态下，需要更新的数据是不一样的
        if (SceneStateController.Instance != null)
        {
            //状态更新的方法
            SceneStateController.Instance.StateUpdate();
        }
        
        if(PanelManager.Instance.CurrentPanel() != null)
            PanelManager.Instance.CurrentPanel().OnUpdata();
    }

}
