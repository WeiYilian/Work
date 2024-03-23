using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏进行时的管理者
/// </summary>
public class MainSceneManager : MonoBehaviour
{
    public static MainSceneManager Instance
    {
        get;
        private set;
    }

    public PlayerConctroller PlayerConctroller;

    //判断是否暂停
    [HideInInspector] public bool isTimeOut;
    
    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimeOut)
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
        }
        
        //游戏界面的数据实时更新
        PlayerConctroller.PanelManager.MainPanel().SuperviserNumber();
    }
}
