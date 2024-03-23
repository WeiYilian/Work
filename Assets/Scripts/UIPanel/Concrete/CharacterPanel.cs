using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CharacterPanel : BasePanel
{
    private static readonly string path = "CharacterPanel";

    public CharacterPanel() : base(new UIType(path)) { }

    private CharacterStats characterStats;

    private GameObject damageBtn;
    
    private GameObject defenceBtn;
    
    private GameObject healthBtn;

    private Text damage;
    
    private Text defence;
    
    private Text health;
    

    // ReSharper disable Unity.PerformanceAnalysis
    public override void OnEnter()
    {
        init();
        InventoryManager.Instance.RefreshBagItem();
        UITool.GetOrAddComponentInChildren<Button>("BtnExit").onClick.AddListener(Pop);
        
        damageBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            if (characterStats.AttributePoints > 0)
            {
                characterStats.MaxDamage++;
                characterStats.MinDamage++;
                characterStats.AttributePoints--;
                damage.text = characterStats.characterData.minDamage + "~" + characterStats.characterData.maxDamage;
                JudgeAttributePoints();
            }
        });
        
        defenceBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            if (characterStats.AttributePoints > 0)
            {
                characterStats.BaseDefence++;
                characterStats.CurrentDefence++;
                characterStats.AttributePoints--;
                defence.text = characterStats.characterData.baseDefence.ToString();
                JudgeAttributePoints();
            }
        });
        
        healthBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            if (characterStats.AttributePoints > 0)
            {
                characterStats.MaxHealth += 5;
                characterStats.CurrentHealth += 5;
                characterStats.AttributePoints--;
                health.text = characterStats.MaxHealth.ToString();
                JudgeAttributePoints();
            }
        });
    }
    
    public override void OnUpdata()
    {
        if (Input.GetKeyDown(KeyCode.B))
            Pop();
    }

    private void init()
    {
        characterStats = playerConctroller.characterStats;
        GameObject CharacterPanel = GameObject.Find("Canvas/CharacterPanel");
        //背包
        InventoryManager.Instance.slotGird = CharacterPanel.transform.Find("Center/Right/ItemView/Viewport/ItemList").gameObject;
        Text itemInfromation = CharacterPanel.transform.Find("Center/Right/Footer").GetComponentInChildren<Text>();
        InventoryManager.Instance.itemInfromation = itemInfromation;
        //基本属性
        CharacterPanel.transform.Find("Center/Left/Top/Level/Text").GetComponent<Text>().text = playerConctroller.PlayerAttrib[5];
        CharacterPanel.transform.Find("Center/Left/Top/name/Text").GetComponent<Text>().text = playerConctroller.PlayerAttrib[1];

        damage = CharacterPanel.transform.Find("Center/Left/Button/Damage/InputField/Text").GetComponent<Text>();
        damage.text = characterStats.characterData.minDamage + "~" + characterStats.characterData.maxDamage;
        defence = CharacterPanel.transform.Find("Center/Left/Button/Defence/InputField/Text").GetComponent<Text>();
        defence.text = characterStats.characterData.baseDefence.ToString();
        health = CharacterPanel.transform.Find("Center/Left/Button/Health/InputField/Text").GetComponent<Text>();
        health.text = characterStats.MaxHealth.ToString();

        damageBtn = CharacterPanel.transform.Find("Center/Left/Button/Damage/Button").gameObject;
        defenceBtn = CharacterPanel.transform.Find("Center/Left/Button/Defence/Button").gameObject;
        healthBtn = CharacterPanel.transform.Find("Center/Left/Button/Health/Button").gameObject;
        JudgeAttributePoints();
            
        itemInfromation.text = "";
    }

    private void JudgeAttributePoints()
    {
        if (characterStats.AttributePoints == 0)
        {
            damageBtn.SetActive(false);
            defenceBtn.SetActive(false);
            healthBtn.SetActive(false);
        }
        else
        {
            damageBtn.SetActive(true);
            defenceBtn.SetActive(true);
            healthBtn.SetActive(true);
        }
    }
}
