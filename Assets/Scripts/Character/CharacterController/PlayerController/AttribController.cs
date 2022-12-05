using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttribController
{
    private PlayerConctroller playerConctroller;
    private CharacterStats characterStats;
    
    private Text userName;

    private Text levelText;
    
    private Image hp;
    private Image exp;
    private Text hpText;
    private Text expText;

    private float beforeHealth = 100f;
    
    

    public AttribController()
    {
        playerConctroller = PlayerConctroller.Instance;
        characterStats = playerConctroller.characterStats;

        MainPanel.PlayerInit += Init;
    }

    private void Init()
    {
        userName = playerConctroller.PanelManager.MainPanel().userName;
        levelText = playerConctroller.PanelManager.MainPanel().levelText;
        hp = playerConctroller.PanelManager.MainPanel().hp;
        exp = playerConctroller.PanelManager.MainPanel().exp; 
        hpText = playerConctroller.PanelManager.MainPanel().hpText;
        expText = playerConctroller.PanelManager.MainPanel().expText;
    }


    // ReSharper disable Unity.PerformanceAnalysis
    public void SuperviserNumber()
    {
        userName.text = playerConctroller.PlayerAttrib[1];
        levelText.text = "LV." + characterStats.CurrentLevel;
        hp.fillAmount = characterStats.CurrentHealth / characterStats.MaxHealth;
        exp.fillAmount = characterStats.CurrentExp / characterStats.BaseExp;
        hpText.text = characterStats.CurrentHealth + "/" + characterStats.MaxHealth;
        expText.text = characterStats.CurrentExp + "/" + characterStats.BaseExp;

        BloodRecoveryEffect();
    }
    
    private void BloodRecoveryEffect()
    {
        if (beforeHealth < characterStats.CurrentHealth)
        {
            GameObject go = GameFacade.Instance.LoadSlash("Treat");
            go.transform.SetParent(GameObject.Find("Player").transform);
            go.transform.position = GameObject.Find("Player").transform.position;
            GameObject.Destroy(go,1f);
        }
        beforeHealth = characterStats.CurrentHealth;
    }
}
