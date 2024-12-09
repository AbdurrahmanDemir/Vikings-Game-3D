
using CrazyGames;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

    public static Action onGameWin;
    public static Action onLogIn;

    int levelGold;
    private void Start()
    {
        onLogIn?.Invoke();
    }
    public void GameWin()
    {
        UIManager.instance.GameUIStageChanged(UIStage.GameWin);

        onGameWin?.Invoke();

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
        

        CrazySDK.Ad.RequestAd(CrazyAdType.Midgame, () =>
        {
            /** ad started */
        }, (error) =>
        {
            UIManager.instance.GameUIStageChanged(UIStage.Menu);
            levelManager.EndBattle();
            SceneManager.LoadScene(0);
        }, () =>
        {
            UIManager.instance.GameUIStageChanged(UIStage.Menu);
            levelManager.EndBattle();
        SceneManager.LoadScene(0);
        });

    }
    public void Claim2XButton()
    {

        CrazySDK.Ad.RequestAd(CrazyAdType.Rewarded, () =>
        {
            /** ad started */
        }, (error) =>
        {
            /** ad error */
        }, () =>
        {
            UIManager.instance.GameUIStageChanged(UIStage.Menu);

            DataManager.instance.AddGem(levelGold);
            levelManager.EndBattle();

            SceneManager.LoadScene(0);
        });

        
    }

    public void GameLose()
    {
        DataManager.instance.AddGem(50);
        UIManager.instance.GameUIStageChanged(UIStage.GameLose);

    }

    public void Lose2XButton()
    {

        CrazySDK.Ad.RequestAd(CrazyAdType.Rewarded, () =>
        {
            /** ad started */
        }, (error) =>
        {
            /** ad error */
        }, () =>
        {
            UIManager.instance.GameUIStageChanged(UIStage.Menu);

            DataManager.instance.AddGem(50);
            levelManager.EndBattle();

            SceneManager.LoadScene(0);
        });

        
    }

}
