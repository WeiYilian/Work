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
    private PlayerConctroller playerConctroller;
    private Inventory playerInventory; 
    
    public GameObject slotGird;
    public GameObject equipGird;
    public Text itemInfromation;
    
    public InventoryManager()
    {
        slots = new List<GameObject>();
        playerConctroller = MainSceneManager.Instance.PlayerConctroller;
        playerInventory = MainSceneManager.Instance.PlayerConctroller.myBag;
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

        for (int i = 0; i < playerConctroller.myBag.itemList.Count; i++)
        {
            slots.Add(GameObject.Instantiate(GameFacade.Instance.LoadGameObject("Slot")));
            slots[i].transform.SetParent(slotGird.transform);
            slots[i].GetComponent<Slot>().SetupSlot(playerConctroller.myBag.itemList[i]);
        }

        EquipController equipController = MainSceneManager.Instance.PlayerConctroller.EquipController;
        if(equipController.Weapon) equipGird.transform.GetChild(0).GetComponent<Slot>().slotItem = equipController.Weapon;
        if(equipController.Armor) equipGird.transform.GetChild(1).GetComponent<Slot>().slotItem = equipController.Armor;
        if(equipController.QuickItems) equipGird.transform.GetChild(2).GetComponent<Slot>().slotItem = equipController.QuickItems;
    }
    
    
    //触发式增加物品
    public void AddNewItem(BagItem item)
    {
        if (!playerInventory.itemList.Contains(item))
        {
            if (playerInventory.itemList.Count > playerInventory.MaxItemListCount) return;
            for (int i = 0; i < playerInventory.itemList.Count; i++)
            {
                if (!playerInventory.itemList[i])
                {
                    playerInventory.itemList[i] = item;
                    return;
                }
            }
            playerInventory.itemList.Add(item);
        }
        else if (playerInventory.itemList.Contains(item))
        {
            if (playerInventory.itemList.Count > playerInventory.MaxItemListCount) return;
            for (int i = 0; i < playerInventory.itemList.Count; i++)
            {
                if (!playerInventory.itemList[i])
                {
                    playerInventory.itemList[i] = item;
                    return;
                }
            }
            playerInventory.itemList.Add(item);
        }
        else
        {
            item.itemHeld += 1;
        }
    }
}
