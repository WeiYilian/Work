using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : BasePanel
{
    private static readonly string path = "MainPanel";

    public MainPanel() : base(new UIType(path)) { }
    
    public static event Action PlayerInit;
    
    private PlayerConctroller playerConctroller;
    private CharacterStats characterStats;
    
    public Text userName;

    public Text hpText;
    public Text mpText;
    public Text expText;
    public Image hp;
    public Image mp;
    public Image exp;

    public Text levelText;

    public Text coinText;

    public Image skill1;
    public Image skill2;
    public Image skill3;

    private bool openBag;
    
    private float beforeHealth = 100f;

    public override void OnEnter()
    {
        Init();
        playerConctroller = MainSceneManager.Instance.PlayerConctroller;
        characterStats = playerConctroller.characterStats;
        EvenCenter.AddListener(EventNum.GAMEOVER,ShowEndPanel);
        PlayerInit?.Invoke();
    }
    
    public void Init()
    {
        GameObject mainPanel = GameObject.Find("Canvas/MainPanel");
        userName = mainPanel.transform.Find("Header/Left/UserName").GetComponent<Text>();
        levelText = mainPanel.transform.Find("Header/Left/ExpBG/LV").GetComponent<Text>();
        hp = mainPanel.transform.Find("Header/Left/Bg/HP").GetComponent<Image>();
        mp = mainPanel.transform.Find("Header/Left/Bg/mp").GetComponent<Image>();
        exp = mainPanel.transform.Find("Header/Left/ExpBG/Exp").GetComponent<Image>();
        hpText = mainPanel.transform.Find("Header/Left/Bg/HP/Text").GetComponent<Text>();
        mpText = mainPanel.transform.Find("Header/Left/Bg/mp/Text").GetComponent<Text>();
        expText = mainPanel.transform.Find("Header/Left/ExpBG/Text").GetComponent<Text>();
        coinText = mainPanel.transform.Find("Header/Left/Coin/Text").GetComponent<Text>();
        
        skill1 = mainPanel.transform.Find("Bottom/SkillPanel/skillOne/Image/skill1").GetComponent<Image>();
        skill2 = mainPanel.transform.Find("Bottom/SkillPanel/skillTwo/Image/skill2").GetComponent<Image>();
        skill3 = mainPanel.transform.Find("Bottom/SkillPanel/skillThree/Image/skill3").GetComponent<Image>();
        MainSceneManager.Instance.isTimeOut = false;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public override void OnUpdata()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Push(new CharacterPanel());
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Push(new PlayerTaskPanel());
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Push(new SetPanel());
        }
    }
    
    public void SuperviserNumber()
    {
        userName.text = playerConctroller.PlayerAttrib[1];
        levelText.text = "LV." + characterStats.CurrentLevel;
        hp.fillAmount = characterStats.CurrentHealth / characterStats.MaxHealth;
        mp.fillAmount = characterStats.CurrentMana / characterStats.MaxMana;
        exp.fillAmount = characterStats.CurrentExp / characterStats.BaseExp;
        hpText.text = characterStats.CurrentHealth + "/" + characterStats.MaxHealth;
        mpText.text = characterStats.CurrentMana + "/" + characterStats.MaxMana;
        expText.text = characterStats.CurrentExp + "/" + characterStats.BaseExp;
        coinText.text = "￥：" + characterStats.Money;

        BloodRecoveryEffect();
        
    }
    
    private void BloodRecoveryEffect()
    {
        if (beforeHealth < characterStats.CurrentHealth)
        {
            GameObject go = ObjectPool.Instance.Get("Treat", playerConctroller.transform);
            go.transform.position = playerConctroller.transform.position;
            ObjectPool.Instance.Remove("Treat",go,1);
        }
        beforeHealth = characterStats.CurrentHealth;
    }

    public override void OnPause()
    {
        base.OnPause();
        MainSceneManager.Instance.isTimeOut = true;
    }

    public override void OnResume()
    {
        base.OnResume();
        MainSceneManager.Instance.isTimeOut = false;
    }

    public override void OnExit()
    {
        EvenCenter.RemoveListener(EventNum.GAMEOVER,ShowEndPanel);
    }

    
    
    
    

    private void ShowEndPanel()
    {
        if(!GameObject.Find("Canvas/OverPanel"))
            PanelManager.Instance.Push(new OverPanel("任务失败"));
    }
}
