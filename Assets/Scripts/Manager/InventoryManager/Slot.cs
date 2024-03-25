using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public BagItem slotItem;
    public Image slotImage;
    public Text slotNum;
    public string slotInfo;

    public GameObject itemInSlot;

    public bool WeaponBox;
    public bool ArmorBox;
    public bool QuickItemsBox;

    public void ItemOncliked()
    {
        InventoryManager.Instance.UpdateItemInfo(slotInfo);
    }

    private void Start()
    {
        if (WeaponBox || ArmorBox || QuickItemsBox)
            SetupSlot(slotItem);
            
    }

    public void SetupSlot(BagItem item)
    {
        if (item == null)
        {
            itemInSlot.SetActive(false);
            return;
        }
        slotItem = item;
        slotImage.sprite = item.itemImage;
        if (item.equip)
        {
            if (item.equipLevel > 0) slotNum.text = "+" + item.equipLevel;
            else slotNum.text = " ";
        }
        else slotNum.text = item.itemHeld.ToString();
        slotInfo = item.itemInfo;
    }

    public void DestroySlot()
    {
        Destroy(gameObject);
    }
}
