//using CrazyGames;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    [Header(" Elements ")]
    [SerializeField] private TextMeshProUGUI gemText;

    [Header(" Data ")]
    [SerializeField] private int gem;

    int number;

  
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        LoadData();

        //if (CrazySDK.IsAvailable)
        //{
        //    CrazySDK.Init(() =>
        //    {
        //        Debug.Log("CrazySDK initialized");
        //        CrazySDK.Game.GameplayStart();
        //    });
        //}
    }


    public bool TryPurchaseGem(int price)
    {
        if (price <= gem)
        {
            gem -= price;
            SaveData(); UpdateGemText();
            return true;
        }

        return false;
    }

    public void AddGem(int value)
    {
        gem += value;

        UpdateGemText();

        SaveData();
    }


    private void UpdateGemText()
    {
        gemText.text = gem.ToString();
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("Gem", gem);
    }

    private void LoadData()
    {       
        int gems = PlayerPrefs.GetInt("Gem",500);
        gem = gems;
        UpdateGemText();
    }



    public void Gold100()
    {
        //CrazySDK.Ad.RequestAd(CrazyAdType.Rewarded, () =>
        //{
        //    /** ad started */
        //}, (error) =>
        //{
        //    /** ad error */
        //}, () =>
        //{
        //    AddGem(100);
        //});
        AddGem(100);
    }
    public void Gold300()
    {
        //CrazySDK.Ad.RequestAd(CrazyAdType.Rewarded, () =>
        //{
        //    /** ad started */
        //}, (error) =>
        //{
        //    /** ad error */
        //}, () =>
        //{
        //    number += 1;

        //    if (number == 2)
        //    {
        //        AddGem(300);
        //    }
        //});

        AddGem(300);

    }

    public void Gold50()
    {
        //CrazySDK.Ad.RequestAd(CrazyAdType.Rewarded, () =>
        //{
        //    /** ad started */
        //}, (error) =>
        //{
        //    /** ad error */
        //}, () =>
        //{
            
        //        AddGem(50);
            
        //});



    }

}
