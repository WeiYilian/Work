using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public event Action<int, int> UpdateHealthBarOnAttack;

    public CharacterData_SO templateData;//模板数据
    
    public CharacterData_SO characterData;//实际数据

    [HideInInspector]//虽然是public的，但是在inspector窗口中屏蔽了
    public bool isCritical;
     private void Awake()
    {
        if (templateData != null)
            characterData = Instantiate(templateData);
    }
     
    #region Read from Data_SO

    public int MaxHealth
    {
        get { if (characterData != null) return characterData.maxHealth;else return 0; }
        set { characterData.maxHealth = value; }
    }
    
    public int CurrentHealth
    {
        get { if (characterData != null) return characterData.currentHealth;else return 0; }
        set { characterData.currentHealth = value; }
    }
    
    public int BaseDefence
    {
        get { if (characterData != null) return characterData.baseDefence;else return 0; }
        set { characterData.baseDefence = value; }
    }
    
    public int CurrentDefence
    {
        get { if (characterData != null) return characterData.currentDefence;else return 0; }
        set { characterData.currentDefence = value; }
    }

    #endregion

    #region Character Combat

    /// <summary>
    /// 攻击时数据更迭
    /// </summary>
    /// <param name="attacker"></param>
    /// <param name="defener"></param>
    public void TakeDamage(CharacterStats attacker, CharacterStats defener, bool isSkill = false)
    {
        int damage = Mathf.Max(attacker.CurrentDamage() - defener.CurrentDefence, 0);
        CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
        if (defener.CompareTag("Player"))
        {
            if(isSkill) 
                defener.GetComponentInChildren<Animator>().SetTrigger("Repelled");
            else
                defener.GetComponentInChildren<Animator>().SetTrigger("Hit");
        }
        else
            defener.GetComponent<Animator>().SetTrigger("Hit");//播放敌人被打动画

        //update UI
        UpdateHealthBarOnAttack?.Invoke(CurrentHealth,MaxHealth);
        //经验update
        if(CurrentHealth <= 0)
            attacker.characterData.UpdateExp(characterData.killPoint);
        
    }

    /// <summary>
    /// 计算当前伤害，判断是否暴击
    /// </summary>
    /// <returns></returns>
    private int CurrentDamage()
    {
        float coreDamage = UnityEngine.Random.Range(characterData.minDamge, characterData.maxDamge);

        if (isCritical)
        {
            coreDamage *= characterData.criticalMultiplier;
            Debug.Log("暴击"+coreDamage);
        }

        return (int)coreDamage;
    }
    
    #endregion
    
    
}
