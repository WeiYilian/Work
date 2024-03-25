using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class EnhancedPanel : BasePanel
{
    private static readonly string path = "EnhancedPanel";

    public EnhancedPanel() : base(new UIType(path)) { }

    public BagItem CurrentEquip;
    
    private Image currentWeaponLevelIamge;
    private Image nextWeaponLevelIamge;
    private Text currentWeaponLevelText;
    private Text nextWeaponLevelText;
    private Text spent;
    private Button intensificationBtn;

    private int equipLevel;
    private int nextWeaponLevel;

    public override void OnEnter()
    {
        Init();
        
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
                equipLevel = equipLevel + 1;
                nextWeaponLevel = nextWeaponLevel + 1;
                CurrentEquip.equipLevel = equipLevel;
                
                currentWeaponLevelText.text = "+" + equipLevel.ToString();
                nextWeaponLevelText.text = "+" + nextWeaponLevel.ToString();
                spent.text = nextWeaponLevel + "0";
            }
        });
    }

    private void Init()
    {
        GameObject enhancedPanel = GameObject.Find("Canvas/EnhancedPanel");

        currentWeaponLevelIamge = enhancedPanel.transform.Find("Content/Enhance1/Item/Item Image").GetComponent<Image>();
        nextWeaponLevelIamge = enhancedPanel.transform.Find("Content/Enhance2/Item/Item Image").GetComponent<Image>();
        currentWeaponLevelText = enhancedPanel.transform.Find("Content/Enhance1/Item/Text").GetComponent<Text>();
        nextWeaponLevelText = enhancedPanel.transform.Find("Content/Enhance2/Item/Text").GetComponent<Text>();
        spent = enhancedPanel.transform.Find("Content/Spend/Text").GetComponent<Text>();
        intensificationBtn = enhancedPanel.transform.Find("Content/Button").GetComponent<Button>();

        InitEquip();
        
        
        Transform EquipListTra = enhancedPanel.transform.Find("EquipListBG/EquipList");

        foreach (BagItem bagItem in MainSceneManager.Instance.PlayerConctroller.myBag.itemList)
        {
            if (bagItem && bagItem.equip)
            {
                GameObject EquipItem = GameFacade.Instance.LoadGameObject("EquipItem");
                EquipItem.transform.GetChild(0).GetComponent<EnhancedEquip>().Item = bagItem;
                GameObject item = GameObject.Instantiate(EquipItem, EquipListTra);
            }
        }
    }

    public void InitEquip()
    {
        Color color = currentWeaponLevelIamge.color;
        if (!CurrentEquip)
        {
            color.a = 0;
            currentWeaponLevelIamge.color = color;
            nextWeaponLevelIamge.color = color;
        }
        else
        {
            color.a = 1;
            currentWeaponLevelIamge.color = color;
            nextWeaponLevelIamge.color = color;
            
            equipLevel = CurrentEquip.equipLevel;
            currentWeaponLevelIamge.sprite = CurrentEquip.itemImage;
            nextWeaponLevelIamge.sprite = CurrentEquip.itemImage;

            if (equipLevel == 0) currentWeaponLevelText.text = " ";
            else currentWeaponLevelText.text = "+" + equipLevel.ToString();
            nextWeaponLevel = equipLevel + 1;
            nextWeaponLevelText.text = "+" + nextWeaponLevel.ToString();

            spent.text = nextWeaponLevel + "0";
        }
    }

}
