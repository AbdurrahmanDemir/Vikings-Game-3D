using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData data;
    [SerializeField] private int price;
    public static Action itemPick;

    //private void Start()
    //{
    //    LaggedAPIUnity.Instance.CheckRewardAd();
    //}
    public void PickUp()
    {
        //LaggedAPIUnity.Instance.PlayRewardAd();
        //LaggedAPIUnity.Instance.CheckRewardAd();

        if (DataManager.instance.TryPurchaseGem(price))
        {
            Inventory.instance.AddItem(data);
            itemPick?.Invoke();
            UIManager.instance.OpenPopUp("ITEM PURCHASED");

        }
        else
        {
            UIManager.instance.OpenPopUp("YOU DON'T HAVE ENOUGH GOLD. BUY FROM MARKET");

        }

    }
}
