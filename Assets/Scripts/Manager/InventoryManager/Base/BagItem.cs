using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Inventory/New Item"),fileName = ("New Item"))]
public class BagItem : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    public int itemHeld;
    [TextArea]
    public string itemInfo;

    public bool equip;//是否为装备
}
