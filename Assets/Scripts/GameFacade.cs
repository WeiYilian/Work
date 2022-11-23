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

    public MainPanelManager PanelManager;

    public string PlayerName
    {
        get => playerName;
        private set => playerName = value;
    }

    private void Awake()
    {
        Instance = this;
        PlayerName = PlayerPrefs.GetString("Player");
        assetFactory = new ResourceAssetFactory();
        PanelManager = GameObject.Find("MainPanel").GetComponent<MainPanelManager>();
    }

    public GameObject LoadSlash(string name)
    {
        return assetFactory.LoadSlash(name);
    }
}
