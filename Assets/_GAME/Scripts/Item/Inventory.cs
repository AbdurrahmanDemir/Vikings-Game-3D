using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<InventoryItem> inventoryItems= new List<InventoryItem> ();
    public Dictionary<ItemData, InventoryItem> inventoryDictionary= new Dictionary<ItemData, InventoryItem> ();

    public List<InventoryItem> equipment= new List<InventoryItem> ();
    public Dictionary <ItemData, InventoryItem> equipmentDictionary = new Dictionary<ItemData, InventoryItem> ();

    public Transform inventorySlot;
    public ItemUI[] itemSlot;

    public Transform equipmentSlotParent;
    public EquipmentSlot[] equipmentSlots;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        itemSlot= inventorySlot.GetComponentsInChildren<ItemUI>();
        equipmentSlots= equipmentSlotParent.GetComponentsInChildren<EquipmentSlot>();
    }

    public void EquipItem(ItemData _item)
    {
        ItemData newEquipment = _item as ItemData;
        InventoryItem newItem = new InventoryItem(newEquipment);



        ItemData oldEquipment = null;

        foreach (KeyValuePair<ItemData, InventoryItem> item in equipmentDictionary)
        {
            if (item.Key.equipmentType == _item.equipmentType)
                oldEquipment = item.Key;
        }

        if (oldEquipment != null)
            UnEquipment(oldEquipment);

        equipment.Add(newItem);
        //InventoryItem newItem = new InventoryItem(_item);
        equipmentDictionary.Add(newEquipment, newItem);
        UpdateUI();
        RemoveItem(_item);

        newEquipment.AddModifiers();
    }

    public void UnEquipment(ItemData removedItem)
    {
        if (equipmentDictionary.TryGetValue(removedItem, out InventoryItem value))
        {
            AddItem(removedItem);
            equipment.Remove(value);
            equipmentDictionary.Remove(removedItem);
            removedItem.RemoveModifiers();
        }
    }

    public void AddItem(ItemData item)
    {
        if(inventoryDictionary.TryGetValue(item,out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(item);

            inventoryItems.Add(newItem);
            inventoryDictionary.Add(item, newItem);
        }

        UpdateUI();
    }

    public void RemoveItem(ItemData item)
    {
        if (inventoryDictionary.TryGetValue(item, out InventoryItem value))
        {
            if (value.stackSize <= 1)
            {
                inventoryItems.Remove(value);
                inventoryDictionary.Remove(item);
            }
            else
            {
                value.RemoveStack();
            }
        }
        UpdateUI();
    }

    void UpdateUI()
    {

        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            foreach(KeyValuePair<ItemData,InventoryItem> item in equipmentDictionary)
            {
                if (item.Key.equipmentType == equipmentSlots[i].type)
                    equipmentSlots[i].UpdateUI(item.Value);
            }
        }
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].CleanUp();
        }

        for (int i = 0; i < inventoryItems.Count; i++)
        {
            itemSlot[i].UpdateUI(inventoryItems[i]);
        }
    }
}
