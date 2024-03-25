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

    public int equipLevel;//装备等级
    
    public int price;

    public bool equip;//是否为装备

    public int equipType;//1是武器，2是防具

    public bool useable;//是否可以使用
}
