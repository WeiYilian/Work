using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager
{
    private static InventoryManager _instance;
    
    public static InventoryManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new InventoryManager();
            }
            return _instance;
        }
    }
    
    public List<GameObject> slots;
    
    public GameObject slotGird;
    public Text itemInfromation;
    
    public InventoryManager()
    {
        slots = new List<GameObject>();
        slotGird = PanelManager.Instance.CharacterPanel().slotGird;
        itemInfromation = PanelManager.Instance.CharacterPanel().itemInfromation;
    }
    
    public void UpdateItemInfo(string itemDescription)
    {
        itemInfromation.text = itemDescription;
    }
    
    
    /// <summary>
    /// 背包系统
    /// </summary>
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
            slots.Add(GameFacade.Instance.LoadGameObject("Slot"));
            slots[i].transform.SetParent(slotGird.transform);
            slots[i].GetComponent<Slot>().SetupSlot(PlayerConctroller.Instance.myBag.itemList[i]);
        }
    }
}
