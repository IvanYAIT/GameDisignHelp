using TMPro;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QuestPlace : MonoBehaviour
{
    [SerializeField] QuestType questType;

    public bool IsActive { get; private set; }

    public static Action<bool> OnQuestComplete;

    private void Start()
    {
        Dialog.ActivateQuest += ActivatePlace;
    }

    public void Use()
    {
        IsActive = false;
        OnQuestComplete?.Invoke(false);
    }

    public void ActivatePlace(QuestType questType)
    {
        if (this.questType == questType)
            IsActive = true;
    }

    public void View(TextMeshProUGUI text)
    {
        if(IsActive)
            switch (questType)
            {
                case QuestType.DistributeHay:
                    text.text = "������� Q ����� ������������ ����";
                    break;
                case QuestType.FeedAndWaterLivestock:
                    text.text = "������� Q ����� ��������� � ������� ����";
                    break;
                case QuestType.WaterAndFertilizeThePlant:
                    text.text = "������� Q ����� ������ � �������� ��������";
                    break;
                case QuestType.WaterTheGarden:
                    text.text = "������� Q ����� ������ ������";
                    break;
                default:
                    break;
            }
    }
}
