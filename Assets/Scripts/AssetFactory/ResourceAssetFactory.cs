using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceAssetFactory : IAssetFactory
{
    public string AudioClipPath = "music/";
    public string SpritePath = "Sprite/";
    public string GOSPath = "GameObject/";

    public AudioClip loadAudioClip(string name)
    {
        return Resources.Load<AudioClip>(name);
    }

    public Sprite LoadSprite(string name)
    {
        return Resources.Load<Sprite>(name);
    }

    public GameObject LoadGameObject(string name)
    {
        return LoadGOS(GOSPath + name);
    }


    private GameObject LoadGOS(string path)
    {
        Object o = Resources.Load(path);

        if (o == null) 
        {
            Debug.LogError("无法加载资源，路径：" + path);
            return null;
        }
        return GameObject.Instantiate(o) as GameObject;
    }
    
    /// <summary>
    /// 提供给代理对象使用的资源加载，只需要获得资产，不需要实例化在场景中
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public Object LoadAsset(string path)
    {
        Object o = Resources.Load(path);
        if (o == null)
        {
            Debug.LogError("无法加载资源，路径：" + path);
            return null;
        }

        return o;
    }
}
