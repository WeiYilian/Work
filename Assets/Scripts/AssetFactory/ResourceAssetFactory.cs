using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceAssetFactory : IAssetFactory
{
    public string SlashPath = "Slash/";
    public string DataPath = "GameData/";
    
    /// <summary>
    /// 实例化特效
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public virtual GameObject LoadSlash(string name)
    {
        return LoadGameObject(SlashPath + name);
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
}
