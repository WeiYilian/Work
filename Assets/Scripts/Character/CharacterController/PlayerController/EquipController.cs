using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipController
{
    private PlayerConctroller playerConctroller;

    private Animator animator;
    
    private BagItem weapon;//武器

    private BagItem armor;//防具

    private BagItem quickItems;//快捷物品

    public EquipController()
    {
        playerConctroller = MainSceneManager.Instance.PlayerConctroller;
        animator = playerConctroller.animator;
    }
    
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
    /// 更换武器
    /// </summary>
    public void ReplaceWeapon(BagItem weapon)
    {
        this.weapon = weapon;
        ReplaceWeaponState();
    }

    public void ReplaceWeaponState()
    {
        //角色状态切换
        if (!weapon)
        {
            playerConctroller.PlayerState = PlayerState.Unarmed;
            animator.runtimeAnimatorController = GameFacade.Instance.LoadAnimator("PlayerUnarmedController");
            foreach (Transform o in playerConctroller.WeaponObj.transform)
            {
                o.gameObject.SetActive(false);
            }
        }
        else if (weapon.itemName == "剑")
        {
            playerConctroller.PlayerState = PlayerState.Sword;
            animator.runtimeAnimatorController = GameFacade.Instance.LoadAnimator("PlayerSwordController");
            foreach (Transform o in playerConctroller.WeaponObj.transform)
            {
                if (o.name == "Sword") o.gameObject.SetActive(true);
                else o.gameObject.SetActive(false);
            }
        }
        else if (weapon.itemName == "弓箭") playerConctroller.PlayerState = PlayerState.Bow;
        else if (weapon.itemName == "法杖")
        {
            playerConctroller.PlayerState = PlayerState.Staffs;
            animator.runtimeAnimatorController = GameFacade.Instance.LoadAnimator("PlayerMagicController");
            foreach (Transform o in playerConctroller.WeaponObj.transform)
            {
                if (o.name == "Staffs") o.gameObject.SetActive(true);
                else o.gameObject.SetActive(false);
            }
        }
        
    }

    /// <summary>
    /// 更换防具
    /// </summary>
    /// <param name="armor"></param>
    public void ReplaceArmor(BagItem armor)
    {
        this.armor = armor;
    }

    /// <summary>
    /// 更换快捷物品
    /// </summary>
    /// <param name="quickItems"></param>
    public void ReplaceQuickItems(BagItem quickItems)
    {
        this.quickItems = quickItems;
    }
}
