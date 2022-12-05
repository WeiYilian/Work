using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 定义一个枚举的数据类型，存储观察者和被观察者的事件码
/// </summary>
public enum EventNum
{
    NONE,
    GAMEOVER
}

//定义一个委托，作为传递的回调函数
public delegate void CallBack();
/// <summary>
/// 观察者的定义
/// </summary>
public class EvenCenter : MonoBehaviour
{
    //定义字典，用于存储事件和委托
    private static Dictionary<EventNum, Delegate> mEvent = new Dictionary<EventNum, Delegate>();

    /// <summary>
    /// 判断添加监听的委托是否同一类型
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="callBack"></param>
    private static void OnListenerAdding(EventNum eventType, Delegate callBack)
    {
        //判断需要传入的事件码是否存在
        if (!mEvent.ContainsKey(eventType))
        {
            mEvent.Add(eventType, null);
        }
    }
        
    /// <summary>
    /// 判断移除的监听是否为空，从而移除事件码
    /// </summary>
    /// <param name="evenType"></param>
    private static void OnListenerRemoved(EventNum eventType)
    {
        //如果事件码存在，但委托为空，就在字典中删除
        if (mEvent[eventType] == null)
        {
            mEvent.Remove(eventType);
        }
    }

    /// <summary>
    /// 观察者添加监听
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="callBack"></param>
    public static void AddListener(EventNum eventType, CallBack callBack)
    {
        OnListenerAdding(eventType,callBack);
        mEvent[eventType] = (CallBack) mEvent[eventType] + callBack;
    }
    
    /// <summary>
    /// 观察者移除监听
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="callBack"></param>
    public static void RemoveListener(EventNum eventType, CallBack callBack)
    {
        mEvent[eventType] = (CallBack) mEvent[eventType] - callBack;
        OnListenerRemoved(eventType);
    }

    /// <summary>
    /// 被观察者，发布事件信息
    /// </summary>
    /// <param name="eventType"></param>
    public static void BroadCast(EventNum eventType)
    {
        Delegate d;
        //如果字典中存在eventType，就返回相对应的委托给d
        if (mEvent.TryGetValue(eventType, out d))
        {
            CallBack callBack = d as CallBack;
            if (callBack != null)
            {
                callBack();
            }
            else
            {
                Debug.LogError("事件不存在");
                throw new Exception("事件不存在");
            }
        }
        
    }
}


