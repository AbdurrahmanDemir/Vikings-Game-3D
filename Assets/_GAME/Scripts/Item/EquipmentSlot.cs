using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlot : ItemUI
{
    public EquipmentType type;

    public Image nullEquipmentImage;

    public override void OnPointerDown(PointerEventData eventData)
    {
        Inventory.instance.UnEquipment(item.data as ItemData);

        item = null;
        itemImage.sprite = nullEquipmentImage.sprite;
    }
}
