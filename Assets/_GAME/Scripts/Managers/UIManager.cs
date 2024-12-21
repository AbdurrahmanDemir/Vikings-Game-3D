using DG.Tweening;
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
    [SerializeField] private GameObject QuestPanel;
    [SerializeField] private GameObject StartTextPanel;
    [SerializeField] private GameObject _popUpPrefabs;
    [SerializeField] private TextMeshProUGUI _popUpPrefabsText;
    [SerializeField] private GameObject _keyTutorialPanel;
    [Header("SO")]
    [SerializeField] private EnemyPopUpSO[] enemySO;
    [SerializeField] private GameObject enemyPopUpPanel;
    public TextMeshProUGUI levelIndex;
    public Image enemyIcon;
    public TextMeshProUGUI enemyName;
    public TextMeshProUGUI enemyTitle;
    public TextMeshProUGUI enemyPrice;

    [Header("CombatSystem")]
    [SerializeField] private GameObject combatPrefabs;
    [SerializeField] private TextMeshProUGUI combatText;


    [Header("Buttons")]
    public GameObject[] levelButtons;

    [Header("Item")]
    [SerializeField] private GameObject itemShop;
    [SerializeField] private GameObject itemPanel;

    public GameObject platformPanel;

    [Header("First Line Settings")]
    public TextMeshProUGUI firstLineText; // Ýlk satýr için Text bileþeni
    public float fadeInDuration = 2f;     // Yavaþça belirme süresi

    [Header("Main Text Settings")]
    public TextMeshProUGUI introText;     // Ana metin için Text bileþeni
    public string mainText = "The harsh winds of the north and the dark nights of winter envelop the village of Skjoldr. For many years the village has lived in peace, protected from the shadow of war by its strong leaders. Now, however, the village leader, the old and respected Jarl Eirik, has died.\n\nAccording to village tradition, a new leader must be chosen. But only the most skilled and brave warriors of the village should be given this task. Eirik's death is a turning point that will decide the fate of the village.";
    public float letterDelay = 0.05f;     // Harfler arasý gecikme süresi

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
        {
            StartTextPanel.SetActive(true);
            StartCoroutine(ShowIntroSequence());

        }

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
        //SaveAchievements(currentLevel);


    }
    IEnumerator ShowIntroSequence()
    {
        // Ýlk satýrý yavaþça belirginleþtir
        firstLineText.DOFade(1, fadeInDuration);

        // Ýlk satýrýn belirme süresi boyunca bekle
        yield return new WaitForSeconds(fadeInDuration + 1f);

        // Ana metni harf harf göster
        yield return StartCoroutine(DisplayMainText());
    }

    IEnumerator DisplayMainText()
    {
        introText.text = "";
        foreach (char letter in mainText)
        {
            introText.text += letter;
            yield return new WaitForSeconds(letterDelay);
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
        EnemyPopUpPanelOff();

        EnemyPopUpSO enemy = enemySO[index];

        levelIndex.text= "Level " + enemy.levelIndex.ToString();
        enemyIcon.sprite = enemy.enemyIcon;
        enemyName.text= enemy.enemyName;
        enemyTitle.text = enemy.enemyTitle;
        enemyPrice.text= enemy.enemyPrice.ToString();

    }
    public void OpenQuestPanel()
    {
        if (QuestPanel.activeSelf)
        {
            QuestPanel.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack).OnComplete(() => QuestPanel.SetActive(false));
        }
        else
        {
            QuestPanel.SetActive(true);
            QuestPanel.transform.localScale = Vector3.zero;
            QuestPanel.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);

        }
    }
    public void EnemyPopUpPanelOff()
    {
        if (enemyPopUpPanel.activeSelf)
        {
            enemyPopUpPanel.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack).OnComplete(() => enemyPopUpPanel.SetActive(false));
        }
        else
        {
            enemyPopUpPanel.SetActive(true);
            enemyPopUpPanel.transform.localScale = Vector3.zero;
            enemyPopUpPanel.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);

        }
    }


    public void ShopPanelOff()
    {
        if (ShopPanel.activeSelf)
        {
            ShopPanel.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack).OnComplete(() => ShopPanel.SetActive(false));
        }
        else
        {
            ShopPanel.SetActive(true);
            ShopPanel.transform.localScale = Vector3.zero;
            ShopPanel.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);

        }
    }

    public void InventoryPanelOff()
    {
        if (InventoryPanel.activeSelf)
        {
            InventoryPanel.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack).OnComplete(() => InventoryPanel.SetActive(false));
        }
        else
        {
            InventoryPanel.SetActive(true);
            InventoryPanel.transform.localScale = Vector3.zero;
            InventoryPanel.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);

        }
    }
    public void KeyTutorialPanelOff()
    {
        if (_keyTutorialPanel.activeSelf)
        {
            _keyTutorialPanel.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack).OnComplete(() => _keyTutorialPanel.SetActive(false));
        }
        else
        {
            _keyTutorialPanel.SetActive(true);
            _keyTutorialPanel.transform.localScale = Vector3.zero;
            _keyTutorialPanel.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);

        }
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
    public void OpenPopUp(string text)
    {
        if (_popUpPrefabs.activeSelf)
        {
            _popUpPrefabs.transform.DOScale(Vector3.zero, 0.2f)
                .SetEase(Ease.InBack)
                .OnComplete(() => _popUpPrefabs.SetActive(false));
        }
        else
        {
            _popUpPrefabs.SetActive(true);
            _popUpPrefabs.transform.localScale = Vector3.zero;
            _popUpPrefabsText.text = text;
            _popUpPrefabs.transform.DOScale(Vector3.one, 0.2f)
                .SetEase(Ease.OutBack)
                .OnComplete(() => StartCoroutine(ClosePanelAfterDelay(1.8f)));
        }
    }

    private IEnumerator ClosePanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (_popUpPrefabs.activeSelf)
        {
            _popUpPrefabs.transform.DOScale(Vector3.zero, 0.2f)
                .SetEase(Ease.InBack)
                .OnComplete(() => _popUpPrefabs.SetActive(false));
        }
    }
    public IEnumerator popUpCreat(string massage)
    {

        _popUpPrefabs.SetActive(true);
        _popUpPrefabsText.text = massage;

        yield return new WaitForSeconds(1.8f);
        _popUpPrefabs.SetActive(false);

    }

    public void ShowCombatMultiplier(int multiplier)
    {
        combatPrefabs.SetActive(true);

        combatText.text = "COMBAT x" + multiplier;

        // Text'i sýfýr saydamlýkla baþlat
        combatText.alpha = 0;

        // Text'i görünür hale getir ve bir süre sonra kaybolmasýný saðla
        combatText.DOFade(1, 0.5f)      // 0.5 saniyede görünür hale gelir
                  .OnComplete(() =>
                  {
                      // 1 saniye bekledikten sonra tekrar kaybol
                      combatText.DOFade(0, 0.5f).SetDelay(1f);
                      combatPrefabs.SetActive(false);
                  });
    }

    public void DiscordLink()
    {
        Application.OpenURL("https://discord.gg/npmtDMbfC3");
    }
}
