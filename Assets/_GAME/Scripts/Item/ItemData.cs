using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public enum EquipmentType
{
    Weapon,
    Armor,
    Ring,
    Trinket
}
[CreateAssetMenu(fileName = "Data", menuName = "Item/ItemData", order = 1)]
public class ItemData : ScriptableObject
{
    public EquipmentType equipmentType;
    public string Name;
    public Sprite icon;


    public int damage;
    public int health;
    public int strenght;
    public int critChance;

    protected StringBuilder sb= new StringBuilder();

    public void AddModifiers()
    {
        PlayerStats playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();

        playerStats.damage.AddModifier(damage);
        playerStats.maxHealth.AddModifier(health);
        playerStats.strength.AddModifier(strenght);
        playerStats.critChance.AddModifier(critChance);
    }

    public void RemoveModifiers()
    {
        PlayerStats playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();

        playerStats.damage.RemoveModifier(damage);
        playerStats.maxHealth.RemoveModifier(health);
        playerStats.strength.RemoveModifier(strenght);
        playerStats.critChance.RemoveModifier(critChance);
    }

    private void AddInfo(int _value, string name)
    {
        if (_value != 0)
        {
            if(sb.Length > 0)
            {
                sb.AppendLine();
            }
            if (_value > 0)
            {
                sb.Append(" "+name + ": " + _value);
            }
        }
    }


    public string GetDesc()
    {
        sb.Length = 0;

        AddInfo(strenght, "Strength");
        AddInfo(damage, "Damage");
        AddInfo(health, "Health");

        return sb.ToString();
    }
}
