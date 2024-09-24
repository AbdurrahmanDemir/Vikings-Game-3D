using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData data;

    private void Start()
    {
        LaggedAPIUnity.Instance.CheckRewardAd();
    }
    public void PickUp()
    {
        LaggedAPIUnity.Instance.PlayRewardAd();
        LaggedAPIUnity.Instance.CheckRewardAd();

        Inventory.instance.AddItem(data);
    }
}
