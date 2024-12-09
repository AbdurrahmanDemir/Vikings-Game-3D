using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private Quest[] quests;
    private Dictionary<int, Quest> uncompletedQuestDictionnary = new Dictionary<int, Quest>();

    [Header("Elements")]
    [SerializeField] private QuestContainer QuestContainerPrefab;
    [SerializeField] private Transform questContainerParent;


    private void Awake()
    {
        QuestContainer.onRewardClaimed += QuestRewardClaimedCallback;
    }

    private void OnDestroy()
    {
        QuestContainer.onRewardClaimed -= QuestRewardClaimedCallback;
    }

    private void Start()
    {
        CreatQuestContainers();
    }
    private void QuestRewardClaimedCallback(int questIndex)
    {


        // g�revi kaydediyoruz
        SetQuestComplete(questIndex);

        // oyuncuya �d�l�n� veriyoruz
        int reward = quests[questIndex].reward;
        DataManager.instance.AddGem(reward);

        UpdateQuest();


    }

    private void UpdateQuest()
    {

        // �nceki g�revleri yok ediyoruz
        foreach (Transform child in questContainerParent)
        {
            Destroy(child.gameObject);
        }

        CreatQuestContainers(); //yeni g�rev olu?turuyoruz
    }
    public void CreatQuestContainers() //G�rev barlar?n? olu?turuyoruz
    {
        StoreUncompletedMissions();

        foreach (KeyValuePair<int, Quest> questData in uncompletedQuestDictionnary)
        {
            CreatQuestContainer(questData);
        }
    }

    private void StoreUncompletedMissions() //G�sterilecek g�revleri buluyoruz
    {
        uncompletedQuestDictionnary.Clear(); //Tamamlanmam?? g�revler listesini temizle


        //G�revler aras?nda gez, tamamlanmam?? olanlardan 3 tanesini listeye ekle
        for (int i = 0; i < quests.Length; i++)
        {
            if (IsQuestComplete(i))
                continue;

            Quest quest = quests[i];
            quest.progress = GetQuestProgress(new KeyValuePair<int, Quest>(i, quest));

            uncompletedQuestDictionnary.Add(i, quest);

            if (uncompletedQuestDictionnary.Count >= 3)
                break;
        }
    }

    public void CreatQuestContainer(KeyValuePair<int, Quest> questData) //G�rev bar?n? olu?turuyoruz ve UI'da g�rev barlar?n? dolduruyoruz
    {
        QuestContainer QuestContainerInstance = Instantiate(QuestContainerPrefab, questContainerParent);


        string title = GetQuestTitle(questData.Value);
        string rewardString = questData.Value.reward.ToString();
        float progress = GetQuestProgress(questData);

        QuestContainerInstance.Configure(title, rewardString, progress, questData.Key);


        Debug.Log("KEY" + QuestContainerInstance.GetKey());
    }


    private string GetQuestTitle(Quest quest) //Aktif g�revin ismini al?yoruz
    {
        switch (quest.Type)
        {
            case QuestType.ItemPickUp:
                return "Kill " + quest.target.ToString() + " enemies";

            case QuestType.Battle:
                return "Use " + quest.target.ToString() + " power";

            case QuestType.AdsWatch:
                return "Match " + quest.target.ToString() + " card";
            case QuestType.WinGame:
                return "Match " + quest.target.ToString() + " card";
            case QuestType.LogIn:
                return "Match " + quest.target.ToString() + " card";


            default:
                return "Blank";
        }
    }

    public void UpdateQuestProgress(int questIndex, float newProgress)
    {
        Debug.Log("New Progress : " + newProgress);

        // g�rev ilerlemesini kaydet
        SaveQuestProgress(questIndex, newProgress);

        Quest quest = quests[questIndex];
        quest.progress = newProgress;
        quests[questIndex] = quest;

        uncompletedQuestDictionnary[questIndex] = quest;

        if (questContainerParent != null)
        {
            for (int i = 0; i < questContainerParent.childCount; i++)
            {
                QuestContainer questContainer = questContainerParent.GetChild(i).GetComponent<QuestContainer>();

                if (questContainer.GetKey() != questIndex)
                    continue;


                questContainer.UpdateProgress(newProgress);
            }
        }

    }

    public Dictionary<int, Quest> GetCurrentQuest() //Aktif g�revi di?er scriptlerde kullanmak i�in al?yoruz
    {
        return uncompletedQuestDictionnary;
    }
    private float GetQuestProgress(KeyValuePair<int, Quest> questData) //G�rev ilerlemesini al?yoruz
    {
        return PlayerPrefs.GetFloat("QuestProgress" + questData.Key);
    }

    private void SaveQuestProgress(int key, float progress) //G�rev ilerlemesini kaydediyoruz
    {
        PlayerPrefs.SetFloat("QuestProgress" + key, progress);

    }

    private void SetQuestComplete(int questIndex) //G�rev tamamlan?nca kaydediyoruz
    {
        PlayerPrefs.SetInt("Quest" + questIndex, 1);
    }

    private bool IsQuestComplete(int questIndex) //G�rev tamamland? m? sorguluyoruz
    {
        return PlayerPrefs.GetInt("Quest" + questIndex) == 1;

    }
}
public enum QuestType { ItemPickUp, Battle, AdsWatch, WinGame, LogIn} //G�rev t�rlerini belirliyoruz

[System.Serializable]
public struct Quest
{
    public QuestType Type;
    public int target;
    public int reward;
    public float progress;
}
