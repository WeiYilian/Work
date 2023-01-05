using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*声音对象池，待完善，可能存在同一时间多种声音源在播放，硬切或者播放完毕再切，无法判定是哪种，无法准确释放AudioObjectPool
 这里只是在开局使用对象池生成了指定个数的播放器，没有用到获取和释放播放器对象*/
public class AudioObjectPool
{
 
    //要生成的对象池预设
    private GameObject prefab;
    //对象池列表
    private List<GameObject> pool;
    //构造函数
    public AudioObjectPool(GameObject prefab, int initialSize)
    {
        this.prefab = prefab;
        pool = new List<GameObject>();
        for (int i = 0; i < initialSize; i++)
        {
            //进行初始化
            AllLocateInstance();
        }
    }
    //获取实例
    public GameObject GetInstance()
    {
        if (pool.Count == 0)
        {
            //创建实例
        }
        GameObject instance = pool[0];
        pool.RemoveAt(0);
        instance.SetActive(true);
        return instance;
    }
    //释放实例
    public void ReleaseInstance(GameObject instance)
    {
        instance.SetActive(false);
        pool.Add(instance);
    }
    //生成本地实例
    private GameObject AllLocateInstance()
    {
        GameObject instance = (GameObject)GameObject.Instantiate(prefab);
        instance.transform.SetParent(AudioManager.Instance.transform);
        instance.SetActive(false);
        pool.Add(instance);
        return instance;
    }
}