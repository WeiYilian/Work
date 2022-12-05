using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 场景的状态管理系统
/// </summary>
public class SceneStateController
{
    private static SceneStateController _instance;
    
    public static SceneStateController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SceneStateController();
            }
            return _instance;
        }
    }
    
    //场景当前的状态
    public SceneState mSceneState;
    //异步操作符，场景使用异步加载的方式
    private AsyncOperation mAo;
    //如果加载成功，并且在运行，就设置为true，防止多次初始化数据
    private bool mIsRunStart;

    private float progressValue;

    public bool IsGameOver { get; set; }

    // ReSharper disable Unity.PerformanceAnalysis
    /// <summary>
    /// 设置场景状态,更新场景
    /// </summary>
    /// <param name="state">要更新的场景</param>
    /// <param name="isLoadScene"></param>
    public void SetState(SceneState state, bool isLoadScene = true)
    {
        //场景初始状态为空，如果不是第一次进入则把上一个场景状态的资源释放
        if (mSceneState != null)
        {
            mSceneState.StateEnd();
        }
        
        //更新当前场景的状态
        mSceneState = state;
        
        //判断是否需要加载场景
        if (isLoadScene)
        {
            //需要加载
            mAo = SceneManager.LoadSceneAsync(mSceneState.MSceneName);//将需要加载的场景名字传入mAo中
            mIsRunStart = false;
        }
        else
        {
            //不需要加载
            mSceneState.StateStart();
            mIsRunStart = true;
        }
    }

    /// <summary>
    /// 当前状态的实时更新内容
    /// </summary>
    public void StateUpdate()
    {
        //场景正在切换，还没有加载完成就直接返回，阻止下一步操作
        if (mAo != null && mAo.isDone == false) return;
        
        
        //异步操作如果完成了，就跳转场景
        if (mAo != null && mAo.isDone && mIsRunStart == false)
        {
            mSceneState.StateStart();
            mIsRunStart = true;
        }
        if (mSceneState != null)
            mSceneState.StateUpdate();
    }
}
