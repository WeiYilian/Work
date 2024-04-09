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
    private Dictionary<string, RuntimeAnimatorController> AnimatorDic = new Dictionary<string, RuntimeAnimatorController>();

    public ResourceAssetProxy()
    {
        if (ResourceAssetFactory.ABDic.Count==0)
        {
            AssetBundle manifestAB = AssetBundle.LoadFromFile(Application.streamingAssetsPath +  "/StreamingAssets");
            AssetBundleManifest manifest = manifestAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            
            foreach (string ABName in manifest.GetAllAssetBundles())
            {
                AssetBundle ab = AssetBundle.LoadFromFile(Application.streamingAssetsPath +"/"+ ABName);
                ResourceAssetFactory.ABDic.Add(ABName, ab);
            }
        }
    }

    public GameObject LoadGameObject(string resName, string filePath)
    {
        if (GOSDic.ContainsKey(resName))
        {
            return GOSDic[resName];
        }
        else
        {
            GameObject asset = assetFactory.LoadAsset(resName, filePath) as GameObject;
            GOSDic.Add(resName, asset);
            return asset;
        }
    }

    public AudioClip loadAudioClip(string resName, string filePath)
    {
        if (AudioClipDic.ContainsKey(resName))
        {
            return AudioClipDic[resName];
        }
        else
        {
            AudioClip asset = assetFactory.LoadAsset(resName, filePath) as AudioClip;
            AudioClipDic.Add(resName, asset);
            return asset;
        }
    }

    public Sprite LoadSprite(string resName, string filePath)
    {
        if (SpriteDic.ContainsKey(resName))
        {
            return SpriteDic[resName];
        }
        else
        {
            Sprite asset = assetFactory.LoadAsset(resName, filePath) as Sprite;
            SpriteDic.Add(resName, asset);
            return asset;
        }
    }
    
    public RuntimeAnimatorController LoadAnimator(string resName, string filePath)
    {
        if (AnimatorDic.ContainsKey(resName))
        {
            return AnimatorDic[resName];
        }
        else
        {
            RuntimeAnimatorController asset = assetFactory.LoadAsset(resName, filePath) as RuntimeAnimatorController;
            AnimatorDic.Add(resName, asset);
            return asset;
        }
    }

}
