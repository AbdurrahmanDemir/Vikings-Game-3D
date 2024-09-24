using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Stat 
{
    [SerializeField] private int baseValue;

    public List<int> modifiers;

    public int GetValue()
    {
        int finalValue= baseValue;

        foreach (var modifier in modifiers)
        {
            finalValue += modifier;
        }
        return finalValue;
    }

    public void AddModifier(float newModifiers)
    {
        modifiers.Add((int)newModifiers);
    }
    public void RemoveModifier(float newModifiers)
    {
        modifiers.Remove((int)newModifiers);
    }
}
