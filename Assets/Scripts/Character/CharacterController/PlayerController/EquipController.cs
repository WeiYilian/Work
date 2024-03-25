using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipController
{
    private BagItem weapon;//武器

    private BagItem armor;//防具

    private BagItem quickItems;//快捷物品

    public BagItem Weapon
    {
        get => weapon;
        private set => weapon = value;
    }

    public BagItem Armor
    {
        get => armor;
        private set => armor = value;
    }

    public BagItem QuickItems
    {
        get => quickItems;
        private set => quickItems = value;
    }
    
    /// <summary>
    /// 更换装备
    /// </summary>
    public void ReplaceEquip(BagItem weapon,BagItem armor,BagItem quickItems)
    {
        this.weapon = weapon;
        this.armor = armor;
        this.quickItems = armor;
    }
}
