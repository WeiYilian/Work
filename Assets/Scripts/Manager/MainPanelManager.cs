using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanelManager : MonoBehaviour
{
    public Text userName;
    
    public Image hp;
    public Image exp;
    
    public Image skill1;
    public Image skill2;
    public Image skill3;

    private float cd1Time;
    private float cd2Time;
    private float cd3Time;

    private AttactController AttactController;

    public void Awake()
    {
        AttactController = GameObject.Find("Player").GetComponentInChildren<AttactController>();
    }

    public void Start()
    {
        //TODO:进入场景时同步修改参数
        //userName.text = GameLoop.Instance.UserName;
    }

    private void Update()
    {
        AttactController.CdDecline(0,ref cd1Time);
        AttactController.CdDecline(1,ref cd2Time);
        AttactController.CdDecline(2,ref cd3Time);
    }

    
}
