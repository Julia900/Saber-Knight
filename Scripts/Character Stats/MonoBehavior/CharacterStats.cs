using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using UnityEditor.Experimental.GraphView;

public class CharacterStats : MonoBehaviour
{
    public event Action<int, int> UpdateHealthBarOnAttack;
    public CharacterData_SO templateData;
    public CharacterData_SO characterData;
    public AttackData_SO attackData;
    [HideInInspector]
    public bool isCritical;

    void Awake()
    {
        if (templateData != null)
        {
            characterData = Instantiate(templateData);
        }
    }

    #region Read from Data_SO
    public int MaxHealth 
    {
        get{if (characterData != null) return characterData.maxHealth; else return 0;}
        set{characterData.maxHealth = value;}
    }

    public int CurrentHealth
    {
        get
        {
            if (characterData != null)
            {
                return characterData.currentHealth;
            }
            else return 0;
        }
        set
        {
            characterData.currentHealth = value;
        }
    }

    public int BaseDefense
    {
        get
        {
            if (characterData != null)
            {
                return characterData.baseDefense;
            }
            else return 0;
        }
        set
        {
            characterData.baseDefense = value;
        }
    }

    public int CurrentDefense
    {
        get
        {
            if (characterData != null)
            {
                return characterData.currentDefense;
            }
            else return 0;
        }
        set
        {
            characterData.currentDefense = value;
        }
    }
    #endregion

    #region Character Combat
    public void TakeDamage(CharacterStats attacker, CharacterStats defender)
    {
        int damage = Mathf.Max(attacker.currentDamage() - defender.CurrentDefense, 0);
        CurrentHealth = Mathf.Max(CurrentHealth-damage, 0);
        if (attacker.isCritical)
        {
            defender.GetComponent<Animator>().SetTrigger("Hit");
        }
        UpdateHealthBarOnAttack?.Invoke(CurrentHealth, MaxHealth);
        if(CurrentHealth <= 0)
            attacker.characterData.UpdateExp(characterData.killPoint);
    }
    public void TakeDamage(int damage, CharacterStats defender)
    {
        int currentDamage = Mathf.Max(damage - defender.CurrentDefense, 0);
        CurrentHealth = Mathf.Max(CurrentHealth - currentDamage, 0);
        UpdateHealthBarOnAttack?.Invoke(CurrentHealth, MaxHealth);
        if (CurrentHealth <= 0)
            GameManager.Instance.playerStats.characterData.UpdateExp(characterData.killPoint);
    }

    private int currentDamage()
    {
        float coreDamage = UnityEngine.Random.Range(attackData.minDamage, attackData.maxDamage);
        if (isCritical)
        {
            coreDamage *= attackData.criticalMultiplier;
        }
        return (int) coreDamage;
    }

    #endregion
}
