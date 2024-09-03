using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class DialogSystem : MonoBehaviour
{
    public static DialogSystem instance;

    [Header("Elements")]
    [SerializeField] private Dialog[] dialogs;

    [Header("Settings")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private GameObject dialogPanel;
    bool nextDialog=false;


    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(instance);
    }

    public void DialogCreat(int index)
    {
        dialogPanel.SetActive(true);

        nameText.text = dialogs[index].name;
        titleText.text = dialogs[index].title[0];
    }
    public void NextTitle(int index)
    {
        if (nextDialog)
            dialogPanel.SetActive(false);
        nextDialog = true;   
        titleText.text = dialogs[index].title[1];

        
    }
}

[Serializable]
public struct Dialog
{
    public string name;
    public string[] title;
}
