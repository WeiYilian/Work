using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFacade : MonoBehaviour
{
    public static GameFacade Instance;

    /// <summary>
    /// 保存用户名方便其他模块调用
    /// </summary>
    private string playerName;
    
    /// <summary>
    /// 工厂实例
    /// </summary>
    private IAssetFactory assetFactory;

    public string PlayerName
    {
        get => playerName;
        set => playerName = value;
    }

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);//场景跳转之后不销毁该游戏物体
        assetFactory = new ResourceAssetProxy();
        // GameObject canvas = GameObject.Find("Canvas");
        // mainPanelManager = canvas.transform.GetChild(0).GetComponent<MainPanelManager>();
    }

    #region 加载资源

    /// <summary>
    /// 获得技能特效
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject LoadSlash(string name)
    {
        return assetFactory.LoadSlash(name);
    }

    /// <summary>
    /// 获得开始界面的玩家信息面板
    /// </summary>
    /// <returns></returns>
    public GameObject LoadAccount()
    {
        return assetFactory.LoadAccount("Account");
    }

    /// <summary>
    /// 加载背包系统的格子
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject LoadSlot()
    {
        return assetFactory.LoadSlot("Slot");
    }
    
    #endregion
    
}
