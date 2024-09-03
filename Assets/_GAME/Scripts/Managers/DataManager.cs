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

  
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        LoadData();

    }
  

    public bool TryPurchaseGem(int price)
    {
        if (price <= gem)
        {
            gem -= price;
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

   
}
