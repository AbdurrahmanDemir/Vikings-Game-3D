
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

public class GameManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private int[] winGem;
    [Header("Settings")]
    [SerializeField] private TextMeshProUGUI winGemText;
    [SerializeField] private LevelsManager levelManager;

    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerStartPos;

    int levelGold;
    private void Start()
    {
        LaggedAPIUnity.Instance.CheckRewardAd();
    }
    public void GameWin()
    {
        UIManager.instance.GameUIStageChanged(UIStage.GameWin);

        int level = PlayerPrefs.GetInt("Level");

        if (level >= 0 && level < winGem.Length)
        {
            DataManager.instance.AddGem(winGem[level]);
        }
        winGemText.text= winGem[level].ToString();
        levelGold = winGem[level];
        level++;
        PlayerPrefs.SetInt("Level", level);
    }

    public void ClaimButton()
    {
        UIManager.instance.GameUIStageChanged(UIStage.Menu);
        levelManager.EndBattle();

        LaggedAPIUnity.Instance.ShowAd();

        SceneManager.LoadScene(0);
    }
    public void Claim2XButton()
    {

        LaggedAPIUnity.Instance.PlayRewardAd();

        UIManager.instance.GameUIStageChanged(UIStage.Menu);
     
        DataManager.instance.AddGem(levelGold);
        levelManager.EndBattle();

        SceneManager.LoadScene(0);
    }

    public void GameLose()
    {
        DataManager.instance.AddGem(50);
        UIManager.instance.GameUIStageChanged(UIStage.GameLose);

    }

    public void Lose2XButton()
    {

        LaggedAPIUnity.Instance.PlayRewardAd();

        UIManager.instance.GameUIStageChanged(UIStage.Menu);

        DataManager.instance.AddGem(50);
        levelManager.EndBattle();

        SceneManager.LoadScene(0);
    }

}
