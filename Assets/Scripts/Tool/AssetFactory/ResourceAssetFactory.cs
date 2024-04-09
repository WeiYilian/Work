using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceAssetFactory : IAssetFactory
{
    public static Dictionary<string, AssetBundle> ABDic = new Dictionary<string, AssetBundle>();

    /// <summary>
    /// 提供给代理对象使用的资源加载，只需要获得资产，不需要实例化在场景中
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public Object LoadAsset(string resName, string filePath)
    {
        Object o = ABDic[filePath].LoadAsset<Object>(resName);
        if (o == null)
        {
            Debug.LogError("无法加载资源:" + filePath + "，名字：" + resName);
            return null;
        }

        return o;
    }

    public GameObject LoadGameObject(string resName, string filePath)
    {
        return ABDic[filePath].LoadAsset<GameObject>(resName);
    }

    public AudioClip loadAudioClip(string resName, string filePath)
    {
        return ABDic[filePath].LoadAsset<AudioClip>(resName);
    }

    public Sprite LoadSprite(string resName, string filePath)
    {
        return ABDic[filePath].LoadAsset<Sprite>(resName);
    }
    
    public RuntimeAnimatorController LoadAnimator(string resName, string filePath)
    {
        return ABDic[filePath].LoadAsset<RuntimeAnimatorController>(resName);
    }
}
