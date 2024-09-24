using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.AudioSettings;

public enum UIStage
{
    Menu,
    Game,
    GameWin,
    GameLose
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
    [SerializeField] private GameObject GameLosePanel;
    [SerializeField] private GameObject ShopPanel;
    [SerializeField] private GameObject InventoryPanel;
    [SerializeField] private GameObject StartTextPanel;
    [SerializeField] private GameObject _popUpPrefabs;
    [SerializeField] private TextMeshProUGUI _popUpPrefabsText;
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

    [Header("Item")]
    [SerializeField] private GameObject itemShop;
    [SerializeField] private GameObject itemPanel;

    public GameObject platformPanel;

    //public GameObject orientationPanel;
    //private bool isMobileDevice = false;

    //// JS fonksiyonunu çaðýrma
    //[DllImport("__Internal")]
    //private static extern bool IsMobileDevice();
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

        if (!PlayerPrefs.HasKey("FirstLogin"))
            StartTextPanel.SetActive(true);

        if (!PlayerPrefs.HasKey("Platform"))
        {
            platformPanel.SetActive(true);
        }


        int currentLevel = PlayerPrefs.GetInt("Level", 0);

            for (int i = 0; i < levelButtons.Length; i++)
            {
                // Eðer bu buton daha önce tamamlanmýþsa, yeþil yap ve týklanamaz hale getir
                if (i < currentLevel)
                {
                    levelButtons[i].gameObject.GetComponent<Button>().interactable = false;
                    levelButtons[i].gameObject.GetComponent<Image>().color = Color.green; // Buton rengini yeþil yap
                }
                // Eðer bu buton tamamlanabilir seviyedeyse, týklanabilir yap
                else if (i == currentLevel)
                {
                    levelButtons[i].gameObject.GetComponent<Button>().interactable = true;
                    levelButtons[i].gameObject.GetComponent<Image>().color = Color.white; // Varsayýlan buton rengi
                }
                // Gelecek seviyeler kilitli, týklanamaz olmalý
                else
                {
                    levelButtons[i].gameObject.GetComponent<Button>().interactable = false;
                    levelButtons[i].gameObject.GetComponent<Image>().color = Color.gray; // Kilitli seviye rengi
                }
            }
        SaveAchievements(currentLevel);


    }
    //private void Update()
    //{
    //    if (isMobileDevice)
    //    {
    //        CheckOrientation();

    //    }
    //}
    //void CheckOrientation()
    //{
    //    if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown)
    //    {
    //        // Dikey modda paneli aç
    //        orientationPanel.SetActive(true);
    //    }
    //    else if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight)
    //    {
    //        // Yatay modda paneli kapat
    //        orientationPanel.SetActive(false);
    //    }
    //}
    void SaveAchievements(int currentLevel)
    {
        switch (currentLevel)
        {
            case 1:
                LaggedAPIUnity.Instance.SaveAchievement("vikings_questpartt_mfqcy001");
                Debug.Log("Level 1 achievement saved");
                break;
            case 2:
                LaggedAPIUnity.Instance.SaveAchievement("vikings_questpartt_mfqcy002");
                Debug.Log("Level 2 achievement saved");
                break;
            case 3:
                LaggedAPIUnity.Instance.SaveAchievement("vikings_questpartt_mfqcy003");
                Debug.Log("Level 3 achievement saved");
                break;
            case 4:
                LaggedAPIUnity.Instance.SaveAchievement("vikings_questpartt_mfqcy004");
                Debug.Log("Level 4 achievement saved");
                break;
            case 5:
                LaggedAPIUnity.Instance.SaveAchievement("vikings_questpartt_mfqcy005");
                Debug.Log("Level 5 achievement saved");
                break;
            case 6:
                LaggedAPIUnity.Instance.SaveAchievement("vikings_questpartt_kznwx007");
                Debug.Log("Level 6 achievement saved");
                break;
            case 7:
                LaggedAPIUnity.Instance.SaveAchievement("vikings_questpartt_kznwx008");
                Debug.Log("Level 7 achievement saved");
                break;
            case 8:
                LaggedAPIUnity.Instance.SaveAchievement("vikings_questpartt_kznwx009");
                Debug.Log("Level 8 achievement saved");
                break;
            case 9:
                LaggedAPIUnity.Instance.SaveAchievement("vikings_questpartt_kznwx0010");
                Debug.Log("Level 9 achievement saved");
                break;
            case 10:
                LaggedAPIUnity.Instance.SaveAchievement("vikings_questpartt_kznwx0011");
                Debug.Log("Level 10 achievement saved");
                break;
            default:
                Debug.Log("No achievements for this level");
                break;
        }
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
                GameLosePanel.SetActive(false);
                break;
            case UIStage.Game:
                MenuPanel.SetActive(false);
                GamePanel.SetActive(true);
                LevelsPanel.SetActive(false);
                GameWinPanel.SetActive(false);
                GameLosePanel.SetActive(false);


                break;
            case UIStage.GameWin:
                MenuPanel.SetActive(false);
                GamePanel.SetActive(false);
                LevelsPanel.SetActive(false);
                GameWinPanel.SetActive(true);
                GameLosePanel.SetActive(false);


                break;
            case UIStage.GameLose:

                MenuPanel.SetActive(false);
                GamePanel.SetActive(false);
                LevelsPanel.SetActive(false);
                GameWinPanel.SetActive(false);
                GameLosePanel.SetActive(true);
                break;

            default:
                break;
        }

    }

    public void PlayButton()
    {
        if (LevelsPanel.activeSelf)
        {
            LevelsPanel.SetActive(false);
            
        }
        else
            LevelsPanel.SetActive(true);
    }
    public void StartGame()
    {
        PlayerPrefs.SetInt("FirstLogin", 1);
        StartTextPanel.SetActive(false);
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

    public void ShopPanelOff()
    {
        if (ShopPanel.activeSelf)
            ShopPanel.SetActive(false);
        else
            ShopPanel.SetActive(true);
    }

    public void InventoryPanelOff()
    {
        if (InventoryPanel.activeSelf)
            InventoryPanel.SetActive(false);
        else
            InventoryPanel.SetActive(true);
    }
    public void PlatformOff()
    {
        if (platformPanel.activeSelf)
        {
            platformPanel.SetActive(false);
            PlayerPrefs.SetInt("Platform", 1);
        }
        else
            platformPanel.SetActive(true);
    }

    public void ItemShop()
    {
            itemPanel.SetActive(false);
            itemShop.SetActive(true);
    }
    public void ItemPanel()
    {
        itemPanel.SetActive(true);
        itemShop.SetActive(false);
    }

    public IEnumerator popUpCreat(string massage)
    {

        _popUpPrefabs.SetActive(true);
        _popUpPrefabsText.text = massage;

        yield return new WaitForSeconds(1.8f);
        _popUpPrefabs.SetActive(false);

    }
}
