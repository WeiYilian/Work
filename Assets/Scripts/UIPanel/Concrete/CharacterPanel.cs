using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CharacterPanel : BasePanel
{
    private static readonly string path = "UI/Panel/CharacterPanel";

    public CharacterPanel() : base(new UIType(path)) { }

    public GameObject slotGird;
    public Text itemInfromation;
    
    // ReSharper disable Unity.PerformanceAnalysis
    public override void OnEnter()
    {
        init();
        InventoryManager.Instance.RefreshBagItem();
        UITool.GetOrAddComponentInChildren<Button>("BtnExit").onClick.AddListener(Pop);
    }

    public override void OnUpdata()
    {
        if (Input.GetKeyDown(KeyCode.B))
            Pop();
    }

    private void init()
    {
        GameObject CharacterPanel = GameObject.Find("Canvas/CharacterPanel");
        slotGird = CharacterPanel.transform.Find("Center/Right/ItemView/Viewport/ItemList").gameObject;
        itemInfromation = CharacterPanel.transform.Find("Center/Right/Footer").GetComponentInChildren<Text>();
        CharacterPanel.transform.Find("Center/Lift/Top/Level/Text").GetComponent<Text>().text = PlayerConctroller.Instance.PlayerAttrib[5];
        CharacterPanel.transform.Find("Center/Lift/Top/name/Text").GetComponent<Text>().text = PlayerConctroller.Instance.PlayerAttrib[1];
        CharacterPanel.transform.Find("Center/Lift/Button/Damage/InputField").GetComponent<Text>().text = 
            PlayerConctroller.Instance.characterStats.characterData.minDamage + "~" + PlayerConctroller.Instance.characterStats.characterData.minDamage;
        CharacterPanel.transform.Find("Center/Lift/Button/Defence/InputField").GetComponent<Text>().text =
            PlayerConctroller.Instance.characterStats.characterData.baseDefence.ToString();
        CharacterPanel.transform.Find("Center/Lift/Button/Health/InputField").GetComponent<Text>().text =
            PlayerConctroller.Instance.characterStats.MaxHealth.ToString();
        itemInfromation.text = "";
    }
}
