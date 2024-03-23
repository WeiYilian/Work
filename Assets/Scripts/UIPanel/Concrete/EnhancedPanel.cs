using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class EnhancedPanel : BasePanel
{
    private static readonly string path = "EnhancedPanel";

    public EnhancedPanel() : base(new UIType(path)) { }

    private Text currentWeaponLevelText;
    private Text nextWeaponLevelText;
    private Text spent;
    private Button intensificationBtn;

    private int weaponLevel;
    private int nextWeaponLevel;

    public override void OnEnter()
    {
        GameObject enhancedPanel = GameObject.Find("Canvas/EnhancedPanel");

        currentWeaponLevelText = enhancedPanel.transform.Find("Content/Enhance1/Text").GetComponent<Text>();
        nextWeaponLevelText = enhancedPanel.transform.Find("Content/Enhance2/Text").GetComponent<Text>();
        spent = enhancedPanel.transform.Find("Content/Spend/Text").GetComponent<Text>();
        intensificationBtn = enhancedPanel.transform.Find("Content/Button").GetComponent<Button>();

        weaponLevel = MainSceneManager.Instance.PlayerConctroller.characterStats.WeaponLevel;

        if (weaponLevel == 0) currentWeaponLevelText.text = " ";
        else currentWeaponLevelText.text = "+" + weaponLevel.ToString();

        nextWeaponLevel = weaponLevel + 1;
        nextWeaponLevelText.text = "+" + nextWeaponLevel.ToString();

        spent.text = nextWeaponLevel + "0";
        
        UITool.GetOrAddComponentInChildren<Button>("BtnExit").onClick.AddListener(()=>
        {
            AudioManager.Instance.PlayButtonAudio();
            Pop();
        });
        
        intensificationBtn.onClick.AddListener(() =>
        {
            int money = MainSceneManager.Instance.PlayerConctroller.characterStats.Money;
            int spend = nextWeaponLevel * 10;
            
            AudioManager.Instance.PlayButtonAudio();
            
            if(money < spend) Debug.Log("无法强化");
            else
            {
                MainSceneManager.Instance.PlayerConctroller.characterStats.Money = money - spend;
                weaponLevel = weaponLevel + 1;
                nextWeaponLevel = nextWeaponLevel + 1;
                MainSceneManager.Instance.PlayerConctroller.characterStats.WeaponLevel = weaponLevel;
                
                currentWeaponLevelText.text = "+" + weaponLevel.ToString();
                nextWeaponLevelText.text = "+" + nextWeaponLevel.ToString();
                spent.text = nextWeaponLevel + "0";
            }
        });
    }
    
    
    public override void OnUpdata()
    {
        
    }

    public override void OnPause()
    {
        
    }

    public override void OnResume()
    {
        
    }
    
    
}
