using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Data", menuName = "Character Stats/Data")]
public class CharacterData_SO : ScriptableObject
{
    [Header("Stats Info")] 
    public float maxHealth;

    public float currentHealth;

    public float maxMana;

    public float currentMana;
    
    public int baseDefence;

    public int currentDefence;
    
    [Header("Kill")] 
    public int killPoint;
    
    [Header("Attack")]
    
    public float attackRange;//判断基本的攻击距离
    
    public float skillRange;//判断技能攻击距离

    public float coolDown;

    public int minDamage;
   
    public int maxDamage;

    public float criticalMultiplier;//暴击之后的加成百分比

    public float criticalChance;//暴击率
    
    [Header("Level")] 
    public int currentLevel;

    public int maxLevel;

    public float baseExp;

    public float currentExp;
    //每升一级需要提升的数据百分比（Buff）
    public float levelBuff;

    public int attributePoints;
    
    //武器等级
    public int weaponLevel;

    public int money;

    public float LevelMultiplier
    {
        get { return 1 + levelBuff;/*获得当前等级所需要获得的总的提升比例*/ }
    }

    /// <summary>
    /// 获得经验值
    /// </summary>
    /// <param name="point"></param>
    public void UpdateExp(int point)
    {
        currentExp += point;
        
        MainSceneManager.Instance.PlayerConctroller.PlayerAttrib[6] = currentExp.ToString();
        
        if (currentExp >= baseExp)
            LeveUp();
    }

    /// <summary>
    /// 升级的数据提升
    /// </summary>
    private void LeveUp()
    {
        GameObject go = ObjectPool.Instance.Get("UPLevel", MainSceneManager.Instance.PlayerConctroller.transform);
        go.transform.position = MainSceneManager.Instance.PlayerConctroller.transform.position;
        ObjectPool.Instance.Remove("UPLevel",go,1);

        MainSceneManager.Instance.PlayerConctroller.PlayerAttrib[5] = currentLevel.ToString();

        //所有想要提升的数据方法都可以写到这里
        currentLevel = Mathf.Clamp(currentLevel + 1, 0, maxLevel);//限制等级不能超过最大等级
        
        baseExp += baseExp;//每一级所需要的经验翻倍
        currentExp = 0;//经验值清零
        MainSceneManager.Instance.PlayerConctroller.PlayerAttrib[6] = currentExp.ToString();

        attributePoints++;//属性点增加

        maxHealth = (int) (maxHealth * LevelMultiplier/*可以换成LevelBuff,就是以当前总生命值的比例提升*/);//每升一级，提升最大血量
        currentHealth = maxHealth;//恢复满血
        maxMana = (int) (maxMana * LevelMultiplier);
        currentMana = maxMana;//恢复满蓝
    }
}