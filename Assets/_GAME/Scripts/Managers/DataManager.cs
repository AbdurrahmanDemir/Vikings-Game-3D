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

    }

    private void Start()
    {

        LaggedAPIUnity.Instance.CheckRewardAd();
    }


    public bool TryPurchaseGem(int price)
    {
        if (price <= gem)
        {
            gem -= price;
            SaveData();
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

        LaggedAPIUnity.Instance.PlayRewardAd();
        LaggedAPIUnity.Instance.CheckRewardAd();
        AddGem(100);
    }
    public void Gold300()
    {


        LaggedAPIUnity.Instance.PlayRewardAd();
        LaggedAPIUnity.Instance.CheckRewardAd();        
        number += 1;

        if (number == 2)
        {
            AddGem(300);
        }


    }

}
