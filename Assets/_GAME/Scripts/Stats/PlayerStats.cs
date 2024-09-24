using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public Stat damage;
    public Stat maxHealth;
    public Stat strength;
    public Stat critChance;

    public int currentHealth;
    public void Start()
    {
        currentHealth = maxHealth.GetValue();
    }

    public int CalculateDamage()
    {
        int totalDamage= damage.GetValue()+ strength.GetValue();

        if (CanCrit())
        {
            totalDamage= CalculateCriticalDamage();
        }
        return totalDamage;
    }
    private int CalculateCriticalDamage()
    {
        int totalCritDamage= CalculateDamage()+50;
        return totalCritDamage;
    }
    private bool CanCrit()
    {
        int finalCritChance= critChance.GetValue()*2;

        if (Random.Range(0, 100) < finalCritChance)
        {
            return true;
        }
        return false;
    }

    public int CalculatedCritChance()
    {
        int finalChance= critChance.GetValue()*2;
        return finalChance;
    }
}
