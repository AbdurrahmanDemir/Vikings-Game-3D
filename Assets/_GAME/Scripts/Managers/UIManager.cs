using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum UIStage
{
    Menu,
    Game,
    Over
}
public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    UIStage stage;

    [Header("Elements")]
    [SerializeField] private GameObject MenuPanel;
    [SerializeField] private GameObject GamePanel;
    [SerializeField] private GameObject LevelsPanel;
    [SerializeField] private GameObject GameWinPanel;
    [Header("SO")]
    [SerializeField] private EnemyPopUpSO[] enemySO;
    [SerializeField] private GameObject enemyPopUpPanel;
    public TextMeshProUGUI levelIndex;
    public Image enemyIcon;
    public TextMeshProUGUI enemyName;
    public TextMeshProUGUI enemyTitle;
    public TextMeshProUGUI enemyPrice;

    [Header("Buttons")]
    public GameObject[] levelButtons;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        int currentLevel = PlayerPrefs.GetInt("Level", 0);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            for (int k = 0; i < currentLevel; i++)
            {
                levelButtons[k].gameObject.GetComponent<Button>().interactable = true;
            }
            levelButtons[i].gameObject.GetComponent<Button>().interactable = false;

        }
        levelButtons[0].gameObject.GetComponent<Button>().interactable = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameUIStageChanged(UIStage stage)
    {
        switch (stage)
        {
            case UIStage.Menu:
                MenuPanel.SetActive(true);
                GamePanel.SetActive(false);
                LevelsPanel.SetActive(false);
                GameWinPanel.SetActive(false);
                break;
            case UIStage.Game:
                MenuPanel.SetActive(false);
                GamePanel.SetActive(true);
                LevelsPanel.SetActive(false);
                GameWinPanel.SetActive(false);

                break;
            case UIStage.Over:
                MenuPanel.SetActive(false);
                GamePanel.SetActive(false);
                LevelsPanel.SetActive(false);
                GameWinPanel.SetActive(true);

                break;
            default:
                break;
        }

    }

    public void PlayButton()
    {
        LevelsPanel.SetActive(true);
    }

    public void EnemyPlayButton(int index)
    {
        enemyPopUpPanel.SetActive(true);

        EnemyPopUpSO enemy = enemySO[index];

        levelIndex.text= "Level " + enemy.levelIndex.ToString();
        enemyIcon.sprite = enemy.enemyIcon;
        enemyName.text= enemy.enemyName;
        enemyTitle.text = enemy.enemyTitle;
        enemyPrice.text= enemy.enemyPrice.ToString();

    }
    public void EnemyPopUpPanelOff()
    {
        if (enemyPopUpPanel.activeSelf)
            enemyPopUpPanel.SetActive(false);
        else
            enemyPopUpPanel.SetActive(true);
    }
}
