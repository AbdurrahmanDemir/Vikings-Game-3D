using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "Data", menuName = "Enemy/PopUp", order = 1)]
public class EnemyPopUpSO : ScriptableObject
{
    [Header("Settings")]
    public int levelIndex;
    public Sprite enemyIcon;
    public string enemyName;
    public string enemyTitle;
    public int enemyPrice;


    public void Config(int index, Image icon, string name, string title, int price)
    {
        levelIndex = index;
        enemyIcon = icon.sprite;
        enemyName = name;
        enemyTitle = title;
        enemyPrice = price;
    }
   
}
