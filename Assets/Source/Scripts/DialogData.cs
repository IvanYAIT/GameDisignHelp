using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog", menuName = "SO/NewDialog")]
public class DialogData : ScriptableObject
{
    public string npcName;
    public List<DialogInfo> dialogs;
}

[Serializable]
public class DialogInfo
{
    public List<string> texts;
    public bool isQuest;
    public QuestType questType;
    public bool isEndOfTheDay;
}

public enum QuestType
{
    None,
    FeedAndWaterLivestock,
    WaterTheGarden,
    WaterAndFertilizeThePlant,
    DistributeHay
}