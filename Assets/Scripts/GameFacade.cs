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
        var find = GameObject.Find("GameLoop");

        if (find == this.gameObject)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);//场景跳转之后不销毁该游戏物体
        }
        else
            Destroy(gameObject);
        
        assetFactory = new ResourceAssetProxy();
    }

    #region 加载资源

    

    /// <summary>
    /// 获得游戏物体
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject LoadGameObject(string name)
    {
        return assetFactory.LoadGameObject(name);
    }

    /// <summary>
    /// 获得音效
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public AudioClip LoadAudioClip(string name)
    {
        return assetFactory.loadAudioClip(name);
    }
    
    public Sprite LoadSprite(string name)
    {
        return assetFactory.LoadSprite(name);
    }

    #endregion
    
}
