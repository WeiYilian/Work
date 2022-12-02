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

    public void ItemOncliked()
    {
        PanelManager.Instance.CharacterPanel().UpdateItemInfo(slotInfo);
    }
    
    public void SetupSlot(BagItem item)
    {
        if (item == null)
        {
            itemInSlot.SetActive(false);
            return;
        }
        slotImage.sprite = item.itemImage;
        slotNum.text = item.itemHeld.ToString();
        slotInfo = item.itemInfo;
    }

    public void DestroySlot()
    {
        Destroy(gameObject);
    }
}
