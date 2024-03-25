using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipController
{
    private BagItem weapon;//武器

    private BagItem armor;//防具

    private BagItem talisman;//护符

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

    public BagItem Talisman
    {
        get => talisman;
        private set => talisman = value;
    }
    
    /// <summary>
    /// 更换装备
    /// </summary>
    public void ReplaceEquip(BagItem weapon,BagItem armor,BagItem talisman)
    {
        this.weapon = weapon;
        this.armor = armor;
        this.talisman = armor;
    }
}
