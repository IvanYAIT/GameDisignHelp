using TMPro;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class Dialog : MonoBehaviour
{
    [SerializeField] private DialogData dialogData;
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private TextMeshProUGUI npcNameText;
    [SerializeField] private PlayerInput playerInput;

    public static Action<bool> OnQuestTake;
    public static Action<QuestType> ActivateQuest;

    private int dialogCounter;
    private int textCounter;
    private bool isDialog;
    private bool isDayEnd;

    private void Start()
    {
        Player.OnZeroEndurance += DayEnd;
        Bed.OnDayStart += DayEnd;
    }

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
                    dialogCounter++;
                    TurnDialog(false);
                    textCounter = 0;
                }
        }
            
    }

    public void View(TextMeshProUGUI text)
    {
        if(!isDayEnd)
            text.text = $"Нажмите R чтобы поговорить с {dialogData.npcName}";
    }

    public void TurnDialog(bool OnOff)
    {
        if (!isDayEnd)
        {
            if (OnOff)
            {
                playerInput.DeactivateInput();
                dialogPanel.SetActive(true);
                Time.timeScale = 0;
                isDialog = true;
                if (dialogData.dialogs[dialogCounter].questType == QuestType.None)
                    OnQuestTake?.Invoke(false);
                else
                    OnQuestTake?.Invoke(true);
                npcNameText.text = dialogData.npcName;
                dialogText.text = dialogData.dialogs[dialogCounter].texts[textCounter];
            }
            else
            {
                playerInput.ActivateInput();
                dialogPanel.SetActive(false);
                Time.timeScale = 1;
                isDialog = false;
            }
        }
            
    }

    public void DayEnd(bool isDayEnd) =>
        this.isDayEnd = isDayEnd;
}
