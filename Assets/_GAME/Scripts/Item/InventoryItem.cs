using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class InventoryItem
{
    public ItemData data;
    public int stackSize;

    public InventoryItem(ItemData newData)
    {
        this.data = newData;
        AddStack();
    }

    public void AddStack()
    {
        stackSize++;    
    }

    public void RemoveStack()
    {
        stackSize--;
    }
}
