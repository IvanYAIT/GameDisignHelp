using TMPro;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dialog : MonoBehaviour
{
    [SerializeField] private DialogData dialogData;
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private TextMeshProUGUI npcNameText;

    public static Action<bool> OnQuestTake;
    public static Action<QuestType> ActivateQuest;
    public static Action<bool> OnDayEnd;

    private int dialogCounter;
    private int textCounter;
    private bool isDialog;

    void Update()
    {
        if(dialogData.dialogs.Count == dialogCounter)
        {
            TurnDialog(false);
        }
        if (isDialog)
        {
            dialogText.text = dialogData.dialogs[dialogCounter].texts[textCounter];
            if (Input.GetMouseButtonDown(0))
                if(dialogData.dialogs[dialogCounter].texts.Count-1 > textCounter)
                    textCounter++;
                else
                {
                    ActivateQuest?.Invoke(dialogData.dialogs[dialogCounter].questType);
                    OnDayEnd?.Invoke(dialogData.dialogs[dialogCounter].isEndOfTheDay);
                    dialogCounter++;
                    TurnDialog(false);
                    textCounter = 0;
                }
        }
            
    }

    public void View(TextMeshProUGUI text)
    {
        text.text = $"Нажмите R чтобы поговорить с {dialogData.npcName}";
    }

    public void TurnDialog(bool OnOff)
    {
        if (OnOff)
        {
            dialogPanel.SetActive(true);
            Time.timeScale = 0;
            isDialog = true;
            OnQuestTake?.Invoke(dialogData.dialogs[dialogCounter].isQuest);
            npcNameText.text = dialogData.npcName;
            dialogText.text = dialogData.dialogs[dialogCounter].texts[textCounter];
        }
        else
        {
            dialogPanel.SetActive(false);
            Time.timeScale = 1;
            isDialog = false;
        }
    }
}
