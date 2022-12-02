using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CharacterPanel : BasePanel
{
    private static readonly string path = "UI/Panel/CharacterPanel";

    public CharacterPanel() : base(new UIType(path)) { }

    public List<GameObject> slots;
    
    public GameObject slotGird;
    public Text itemInfromation;
    
    // ReSharper disable Unity.PerformanceAnalysis
    public override void OnEnter()
    {
        init();
        RefreshBagItem();
        UITool.GetOrAddComponentInChildren<Button>("BtnExit").onClick.AddListener(Pop);
    }

    public override void OnUpdata()
    {
        if (Input.GetKeyDown(KeyCode.B))
            Pop();
    }

    private void init()
    {
        slots = new List<GameObject>();
        
        GameObject CharacterPanel = GameObject.Find("Canvas/CharacterPanel");
        slotGird = CharacterPanel.transform.Find("Center/Right/ItemView/Viewport/ItemList").gameObject;
        itemInfromation = CharacterPanel.transform.Find("Center/Right/Footer").GetComponentInChildren<Text>();
        
        itemInfromation.text = "";
    }
    
    public void UpdateItemInfo(string itemDescription)
    {
        itemInfromation.text = itemDescription;
    }
    
    public void RefreshBagItem()
    {
        for (int i = 0; i < slotGird.transform.childCount; i++)
        {
            if (slotGird.transform.childCount == 0) break;
            slotGird.transform.GetChild(i).GetComponent<Slot>().DestroySlot();
            slots.Clear();
        }

        for (int i = 0; i < PlayerConctroller.Instance.myBag.itemList.Count; i++)
        {
            slots.Add(GameFacade.Instance.LoadSlot());
            slots[i].transform.SetParent(slotGird.transform);
            slots[i].GetComponent<Slot>().SetupSlot(PlayerConctroller.Instance.myBag.itemList[i]);
        }
    }
}
