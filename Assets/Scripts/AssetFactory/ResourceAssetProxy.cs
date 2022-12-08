using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceAssetProxy : IAssetFactory
{
    //获得资源工厂的引用
    private ResourceAssetFactory assetFactory = new ResourceAssetFactory();
    //声明资源的字典，保存加载的数据，避免重复加载一样的资源
    private Dictionary<string, GameObject> GOSDic = new Dictionary<string, GameObject>();
    private Dictionary<string, AudioClip> AudioClipDic = new Dictionary<string, AudioClip>();
    private Dictionary<string, Sprite> SpriteDic = new Dictionary<string, Sprite>();
    

    public GameObject LoadGameObject(string name)
    {
        if (GOSDic.ContainsKey(name))
        {
            return GOSDic[name];
        }
        else
        {
            GameObject asset = assetFactory.LoadAsset(assetFactory.GOSPath + name) as GameObject;
            GOSDic.Add(name, asset);
            return asset;
        }
    }

    public AudioClip loadAudioClip(string name)
    {
        if (AudioClipDic.ContainsKey(name))
        {
            return AudioClipDic[name];
        }
        else
        {
            AudioClip asset = assetFactory.loadAudioClip(assetFactory.AudioClipPath + name);
            AudioClipDic.Add(name, asset);
            return asset;
        }
    }

    public Sprite LoadSprite(string name)
    {
        if (SpriteDic.ContainsKey(name))
        {
            return SpriteDic[name];
        }
        else
        {
            Sprite asset = assetFactory.LoadSprite(assetFactory.SpritePath + name);
            SpriteDic.Add(name, asset);
            return asset;
        }
    }

}
