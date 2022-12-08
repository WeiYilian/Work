using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 场景状态
/// </summary>
public abstract class SceneState
{
    //需要加载的场景的名字
    private string mSceneName;
    //场景状态管理
    public SceneStateController mController;
    public string MSceneName { get => mSceneName;}
    public SceneState(string sceneName)
    {
        mSceneName = sceneName;
        mController = SceneStateController.Instance;
    }
    
    //每次进入到这个状态的时候调用
    public virtual void StateStart() { }
    //在这个状态下每帧更新的信息
    public virtual void StateUpdate() { }
    //离开这个状态的时候调用
    public virtual void StateEnd() { }
}
