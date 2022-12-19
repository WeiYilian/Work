using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnSlot : MonoBehaviour
{
    public BagItem thisItem;
    public Inventory PlayerInventory;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AddNewItem();
            ObjectPool.Instance.Remove(transform.name,gameObject);
        }
    }

    public void AddNewItem()
    {
        if (!PlayerInventory.itemList.Contains(thisItem))
        {
            if (PlayerInventory.itemList.Count > PlayerInventory.MaxItemListCount) return;
            for (int i = 0; i < PlayerInventory.itemList.Count; i++)
            {
                if (!PlayerInventory.itemList[i])
                {
                    PlayerInventory.itemList[i] = thisItem;
                    return;
                }
            }
            PlayerInventory.itemList.Add(thisItem);
        }
        else
        {
            thisItem.itemHeld += 1;
        }
    }
}
