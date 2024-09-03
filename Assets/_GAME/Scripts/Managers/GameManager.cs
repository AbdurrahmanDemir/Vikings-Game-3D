
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private int[] winGem;
    [Header("Settings")]
    [SerializeField] private TextMeshProUGUI winGemText;
    [SerializeField] private LevelsManager levelManager;

    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerStartPos;

    public void GameWin()
    {
        UIManager.instance.GameUIStageChanged(UIStage.Over);

        int level = PlayerPrefs.GetInt("Level");

        if (level >= 0 && level < winGem.Length)
        {
            DataManager.instance.AddGem(winGem[level]);
        }

        level++;
        PlayerPrefs.SetInt("Level", level);
    }

    public void ClaimButton()
    {
        UIManager.instance.GameUIStageChanged(UIStage.Menu);
        levelManager.EndBattle();

        SceneManager.LoadScene(0);
    }
}
