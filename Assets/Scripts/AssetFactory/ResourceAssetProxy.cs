using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceAssetProxy : IAssetFactory
{
    //获得资源工厂的引用
    private ResourceAssetFactory assetFactory = new ResourceAssetFactory();
    //声明资源的字典，保存加载的数据，避免重复加载一样的资源
    private Dictionary<string, GameObject> SlashDic = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> AccountDic = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> SlotDic = new Dictionary<string, GameObject>();
    
    /// <summary>
    /// 代理加载特效
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject LoadSlash(string name)
    {
        if (SlashDic.ContainsKey(name))
        {
            return GameObject.Instantiate(SlashDic[name]);
        }
        else
        {
            GameObject asset = assetFactory.LoadAsset(assetFactory.SlashPath) as GameObject;
            SlashDic.Add(name, asset);
            return GameObject.Instantiate(asset);
        }
    }

    /// <summary>
    /// 代理加载用户信息面板
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject LoadAccount(string name)
    {
        if (AccountDic.ContainsKey(name))
        {
            return GameObject.Instantiate(AccountDic[name]);
        }
        else
        {
            GameObject asset = assetFactory.LoadAsset(assetFactory.AccountPath) as GameObject;
            AccountDic.Add(name, asset);
            return GameObject.Instantiate(asset);
        }
    }

    /// <summary>
    /// 代理加载背包格子
    /// </summary>
    /// <returns></returns>
    public GameObject LoadSlot(string name)
    {
        if (SlotDic.ContainsKey(name))
        {
            return GameObject.Instantiate(SlotDic[name]);
        }
        else
        {
            GameObject asset = assetFactory.LoadAsset(assetFactory.SlotPath) as GameObject;
            SlotDic.Add(name, asset);
            return GameObject.Instantiate(asset);
        }
    }
}
