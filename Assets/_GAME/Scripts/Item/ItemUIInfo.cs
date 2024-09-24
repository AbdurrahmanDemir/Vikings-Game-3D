using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemUIInfo : MonoBehaviour
{
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    public TextMeshProUGUI itemType;


    public void ShowToolTip(ItemData item)
    {
        if (item == null)
        {
            return;
        }

        itemName.text = item.Name;
        itemType.text = item.equipmentType.ToString();

        itemDescription.text = item.GetDesc();

        gameObject.SetActive(true);
    }

    public void HideToolTip()
    {
        gameObject.SetActive(false);
    }

}
