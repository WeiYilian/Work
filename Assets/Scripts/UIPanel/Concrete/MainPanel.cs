using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : BasePanel
{
    private static readonly string path = "UI/Panel/MainPanel";
    
    public MainPanel():base(new UIType(path)){}
    
    public Text userName;

    public Text hpText;
    public Text expText;
    public Image hp;
    public Image exp;

    public Text levelText;

    public Image skill1;
    public Image skill2;
    public Image skill3;

    private bool openBag;

    public override void OnEnter()
    {
        Init();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public override void OnUpdata()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Push(new CharacterPanel());
        }
    }

    public void Init()
    {
        GameObject mainPanel = GameObject.Find("Canvas/MainPanel");
        userName = mainPanel.transform.Find("Header/Left/UserName").GetComponent<Text>();
        levelText = mainPanel.transform.Find("Header/Left/LV").GetComponent<Text>();
        hp = mainPanel.transform.Find("Header/Left/Bg/HP").GetComponent<Image>();
        exp = mainPanel.transform.Find("Header/Left/Bg/Exp").GetComponent<Image>();
        hpText = mainPanel.transform.Find("Header/Left/Bg/HP/Text").GetComponent<Text>();
        expText = mainPanel.transform.Find("Header/Left/Bg/Exp/Text").GetComponent<Text>();
        skill1 = mainPanel.transform.Find("Content/SkillPanel/skillOne/Image/skill1").GetComponent<Image>();
        skill2 = mainPanel.transform.Find("Content/SkillPanel/skillTwo/Image/skill2").GetComponent<Image>();
        skill3 = mainPanel.transform.Find("Content/SkillPanel/skillThree/Image/skill3").GetComponent<Image>();
        GameObject player = GameObject.Find("Player");
        player.transform.GetChild(1).gameObject.SetActive(true);
        player.transform.GetChild(0).gameObject.SetActive(true);
    }
}
