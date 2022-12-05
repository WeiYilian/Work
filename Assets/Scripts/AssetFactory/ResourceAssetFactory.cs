using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceAssetFactory : IAssetFactory
{
    public string SlashPath = "Slash/";
    public string AccountPath = "UI/Account/";
    public string SlotPath = "UI/Slot/";
    public string EnemyPath = "Enemy/";
    public string AudioClipPath = "music/";
    public string SpritePath = "Sprite/";

    /// <summary>
    /// 实例化特效
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public virtual GameObject LoadSlash(string name)
    {
        return LoadGameObject(SlashPath + name);
    }

    public GameObject LoadAccount(string name)
    {
        return LoadGameObject(AccountPath + name);
    }

    public GameObject LoadSlot(string name)
    {
        return LoadGameObject(SlotPath + name);
    }

    public GameObject LoadEnemy(string name)
    {
        return LoadGameObject(EnemyPath + name);
    }

    public AudioClip loadAudioClip(string name)
    {
        return Resources.Load<AudioClip>(name);
    }

    public Sprite LoadSprite(string name)
    {
        return Resources.Load<Sprite>(name);
    }


    private GameObject LoadGameObject(string path)
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
