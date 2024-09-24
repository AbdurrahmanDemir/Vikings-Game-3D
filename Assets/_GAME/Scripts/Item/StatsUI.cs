using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsUI : MonoBehaviour
{
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI strenghtText;
    public TextMeshProUGUI critText;

    public GameObject player;

    private void Update()
    {
       
        if (player != null)
        {

            damageText.text = player.GetComponent<PlayerStats>().damage.GetValue().ToString();
            healthText.text = player.GetComponent<PlayerStats>().maxHealth.GetValue().ToString();
            strenghtText.text = (player.GetComponent<PlayerStats>().strength.GetValue()*5).ToString();
            critText.text = (player.GetComponent<PlayerStats>().critChance.GetValue() * 5).ToString();
        }
    }
}
