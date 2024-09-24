using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour ,IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image itemImage;
    public Image nullImage;
    public TextMeshProUGUI itemAmount;
    public ItemUIInfo infoPanel;
    public InventoryItem item;

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        Inventory.instance.EquipItem(item.data);
    }
    //public void Select()
    //{
    //    Inventory.instance.EquipItem(item.data);
    //}

    public void UpdateUI(InventoryItem newItem)
    {
        item = newItem;

        if(item!= null)
        {
            itemImage.sprite = item.data.icon;

            if (item.stackSize > 1)
                itemAmount.text = item.stackSize.ToString();
            else
                itemAmount.text = "";
        }
    }

    public void CleanUp()
    {
        item = null;
        itemAmount.text = "";
        itemImage.sprite = nullImage.sprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        infoPanel.HideToolTip();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       
        infoPanel.ShowToolTip(item.data as ItemData);

    }
}
