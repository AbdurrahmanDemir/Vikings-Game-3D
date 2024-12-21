//using CrazyGames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject[] levels;
    private int currentLevel;


    [Header("Settings")]
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject menuCamera;
    [SerializeField] private GameObject gameplayCamera;
    [SerializeField] private GameObject menuEnvo;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerPos;
    [Header("SO")]
    [SerializeField] private EnemyPopUpSO[] enemySO;
    GameObject levelInstance;

    public static Action onPlayNumber;

    //private void Start()
    //{
    //    LoadLevel();
    //    Debug.Log(currentLevel);
    //}
    public void LoadLevel()
    {
        currentLevel = PlayerPrefs.GetInt("Level", 0);

        if (DataManager.instance.TryPurchaseGem(enemySO[currentLevel].enemyPrice))
        {
            player.gameObject.SetActive(false);

            //CrazySDK.Ad.RequestAd(CrazyAdType.Midgame, () =>
            //{
            //    /** ad started */
            //}, (error) =>
            //{
            //}, () =>
            //{
            //});


            //currentLevel = PlayerPrefs.GetInt("Level", 0);
            Debug.Log(currentLevel);

            if (currentLevel >= levels.Length)
            {
                currentLevel = 0;
            }
            levelInstance = Instantiate(levels[currentLevel]);
            StartCoroutine(EnableLevelCoroutine());

            IEnumerator EnableLevelCoroutine()
            {
                yield return new WaitForSeconds(Time.deltaTime);
                levelInstance.SetActive(true);
            StartBattle();
            }
        }
        else
        {
            UIManager.instance.OpenPopUp("YOU DON'T HAVE ENOUGH GOLD. BUY FROM MARKET");
            UIManager.instance.ShopPanelOff();
        }


        

    }
    public void StartBattle()
    {
        onPlayNumber?.Invoke();

        camera.gameObject.SetActive(false);
        UIManager.instance.GameUIStageChanged(UIStage.Game);
        menuCamera.gameObject.SetActive(false);
        menuEnvo.SetActive(false);

        DialogSystem.instance.DialogCreat(currentLevel);
        player.transform.position = playerPos.transform.position;
        player.gameObject.SetActive(true);


        camera.gameObject.transform.position = Vector3.Lerp(menuCamera.transform.position, gameplayCamera.transform.position, 3f);
    }
    public void EndBattle()
    {
        camera.gameObject.SetActive(true);
        menuCamera.gameObject.SetActive(true);
        menuEnvo.SetActive(true);
        Destroy(levelInstance);
    }
    public void SetLevel()
    {
        currentLevel++;
        PlayerPrefs.SetInt("Level", currentLevel);
    }

}
