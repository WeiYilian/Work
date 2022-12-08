using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // 单例
    private static ObjectPool _instance;

    public static ObjectPool Instance
    {
        get => _instance;
        private set => _instance = value;
    }

    private void Awake()
    {
        var find = GameObject.Find("GameLoop");

        if (find == gameObject)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);//场景跳转之后不销毁该游戏物体
        }
        else
            Destroy(gameObject);
    }

    public Dictionary<string, Queue<GameObject>> Pool = new Dictionary<string, Queue<GameObject>>(); 
    
    // 池子要存储的物体
    public GameObject Object;
    // 内存区（队列）
    public Queue<GameObject> enemyPool = new Queue<GameObject>();
    // 池子的初始容量
    public int DefaultCount = 16;
    // 池子的最大容量
    public int maxCount = 25;

    /// <summary>
    /// 对池子进行初始化（创建初始容量个数的物体）
    /// </summary>
    /// <param name="type">对象池名字</param>
    /// <param name="go">需要初始化的游戏物体</param>
    /// <param name="defaultCount">初始数量</param>
    public void Init(string type,GameObject obj,float defaultCount,Transform parent = null)
    {
        for (int i = 0; i < defaultCount; i++)
        {
            GameObject go = Instantiate(obj,parent);
            // 将生成的对象入队
            if (Pool.TryGetValue(type,out Queue<GameObject> queue))
            {
                go.GetComponent<IPoolable>().Dispose();
                queue.Enqueue(go);
            }
            else
            {
                Queue<GameObject> pool = new Queue<GameObject>();
                Pool.Add(type,pool);
                pool.Enqueue(go);
                go.GetComponent<IPoolable>().Dispose();
            }
        }
    }
    
    /// <summary>
    /// 从池子中取出物体
    /// </summary>
    /// <param name="type">对象池名字</param>
    /// <returns></returns>
    public GameObject Get(string type,Transform parent = null)
    {
        GameObject tmp;
        if (Pool.TryGetValue(type, out Queue<GameObject> pool))
        {
            // 如果池子内有物体，从池子取出一个物体
            if (pool.Count > 0)
            {
                // 将对象出队
                tmp = pool.Dequeue();
                tmp.GetComponent<IPoolable>().Init();
            }
            // 如果池子中没有物体，直接新建一个物体
            else
            {
                
                tmp = GameFacade.Instance.LoadGameObject(type);
                pool.Enqueue(tmp);
                return Instantiate(tmp,parent);
            }
        }
        else
        {
            Queue<GameObject> newPool = new Queue<GameObject>();
            Pool.Add(type,newPool);
            tmp = GameFacade.Instance.LoadGameObject(type);
            newPool.Enqueue(tmp);
            return Instantiate(tmp,parent);
        }
        return tmp;
    }
    
    /// <summary>
    /// 将物体回收进池子
    /// </summary>
    /// <param name="type">对象池名字</param>
    /// <param name="obj">需要回收的物体</param>
    /// <param name="time">需要延后的时间</param>
    public void Remove(string type,GameObject obj,float time = 0)
    {
        StartCoroutine(Delay(type,obj,time));
    }

    IEnumerator Delay(string type,GameObject obj,float time)
    {
        yield return new WaitForSeconds(time);
        
        if (Pool.TryGetValue(type, out Queue<GameObject> queue))
        {
            // 池子中的物体数目不超过最大容量
            if (queue.Count <= maxCount)
            {
                // 该对象没有在队列中
                if (!queue.Contains(obj))
                {
                    // 将对象入队
                    queue.Enqueue(obj);
                    obj.GetComponent<IPoolable>().Dispose();
                }
            }
            // 超过最大容量就销毁
            else
            {
                GameObject.Destroy(obj);
            }
        }
        else
            GameObject.Destroy(obj);
    }
}
