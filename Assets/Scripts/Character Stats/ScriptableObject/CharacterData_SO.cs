using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Data", menuName = "Character Stats/Data")]
public class CharacterData_SO : ScriptableObject
{
    [Header("Stats Info")] 
    public int maxHealth;

    public int currentHealth;
    
    public int baseDefence;

    public int currentDefence;
    
    [Header("Kill")] 
    public int killPoint;
    
    [Header("Attack")]
    
    public float attackRange;//判断基本的攻击距离
    
    public float skillRange;//判断技能攻击距离

    public float coolDown;

    public int minDamge;
   
    public int maxDamge;

    public float criticalMultiplier;//暴击之后的加成百分比

    public float criticalChance;//暴击率
    
    [Header("Level")] 
    public int currentLevel;

    public int maxLevel;

    public int baseExp;

    public int currentExp;
    //每升一级需要提升的数据百分比（Buff）
    public float levelBuff;

    public float LevelMultiplier
    {
        get { return 1 + (currentLevel - 1) * levelBuff;/*获得当前等级所需要获得的总的提升比例*/ }
    }

    /// <summary>
    /// 获得经验值
    /// </summary>
    /// <param name="point"></param>
    public void UpdateExp(int point)
    {
        currentExp += point;
        
        if (currentExp >= baseExp)
            LeveUp();
    }

    /// <summary>
    /// 升级的数据提升
    /// </summary>
    private void LeveUp()
    {
        //所有想要提升的数据方法都可以写到这里
        currentLevel = Mathf.Clamp(currentLevel + 1, 0, maxLevel);//限制等级不能超过最大等级
        
        baseExp += (int) (baseExp * LevelMultiplier/*以原始经验值的比例提升*/);//每一级所需要的经验增加
        currentExp = 0;//经验值清零

        maxHealth = (int) (maxHealth * LevelMultiplier/*可以换成LevelBuff,就是以当前总生命值的比例提升*/);//每升一级，提升最大血量
        currentHealth = maxHealth;//恢复满血

        Debug.Log("LEVEL UP!" +currentLevel+ "Max Health:" + maxHealth);
    }
}