using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所有UI面板的父类，包含UI面板的状态信息
/// </summary>
public abstract class BasePanel
{
    //UI信息
    public UIType UIType { get; private set; }
    //UI管理工具
    public UITool UITool { get; private set; }
    //面板管理器
    public PanelManager PanelManager { get; private set; }
    //UI管理器
    public UIManager UIManager { get; private set; }

    public static PlayerConctroller playerConctroller;
    
    public BasePanel(UIType uIType)
    {
        UIType = uIType;
        playerConctroller = MainSceneManager.Instance.PlayerConctroller;
    }
    //初始化UITool
    public void Initialize(UITool tool)
    {
        UITool = tool;
    }
    //初始化面板管理器
    public void Initialize(PanelManager panelManager)
    {
        PanelManager = panelManager;
    }
    //初始化UI管理器
    public void Initialize(UIManager uiManager)
    {
        UIManager = uiManager;
    }

    /// <summary>
    /// UI进入时执行的操作，只会执行一次
    /// </summary>
    public virtual void OnEnter() { }

    /// <summary>
    /// UI暂停时执行的操作
    /// </summary>
    public virtual void OnPause()
    {
        UITool.GetOrAddComponent<CanvasGroup>().blocksRaycasts = false;
    }

    /// <summary>
    /// UI打开时进行的操作
    /// </summary>
    public virtual void OnUpdata() { }
    
    /// <summary>
    /// UI继续时执行的操作
    /// </summary>
    public virtual void OnResume()
    {
        UITool.GetOrAddComponent<CanvasGroup>().blocksRaycasts = true;
    }
    /// <summary>
    /// UI退出时执行的操作
    /// </summary>
    public virtual void OnExit()
    {
        AudioManager.Instance.PlayButtonAudio();
        UIManager.DestroyUI(UIType);
    }

    /// <summary>
    /// 显示一个面板
    /// </summary>
    /// <param name="panel"></param>
    public void Push(BasePanel panel) => PanelManager?.Push(panel);

    /// <summary>
    /// 关闭一个面板
    /// </summary>
    public void Pop() => PanelManager?.Pop();
}
