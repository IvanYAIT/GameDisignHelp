using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Slider endurance;

    public static Action<bool> OnZeroEndurance;

    private string[] items;
    private bool isQuest;
    private bool isDayEnd;

    void Start()
    {
        items = new string[] { "BucketWithWater", "Vegetables", "FertilizerBag", "Pitchfork", "Baskets", "HayForLivestock" };
        Dialog.OnQuestTake += Quest;
        QuestPlace.OnQuestComplete += Quest;
        QuestPlace.OnQuestComplete += DecreaseEndurance;
        Bed.OnDayStart += DayEnd;
    }

    private void Update()
    {
        if (endurance.value <= 0.1f)
        {
            OnZeroEndurance?.Invoke(true);
            DayEnd(true);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        try
        {
            other.gameObject.GetComponent<Item>().View(text);
        }
        catch (Exception e) { };
        try
        {
            other.gameObject.GetComponent<Door>().View(text);
        }
        catch (Exception e) { };
        try
        {
            other.gameObject.GetComponent<Dialog>().View(text);
        }
        catch (Exception e) { };
        try
        {
            other.gameObject.GetComponent<QuestPlace>().View(text);
        }
        catch (Exception e) { };
        try
        {
            other.gameObject.GetComponent<Bed>().View(text);
        }
        catch (Exception e) { };
    }

    private void FindAnItem(Collider other, string tag)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(other.CompareTag(tag))
            {
                if(tag == "Vegetables")
                {
                    if (CheckAnItemInInventory(ItemType.Baskets))
                        inventory.AddItem(other.gameObject.GetComponent<Item>().ItemData);
                }
                else if(tag == "HayForLivestock")
                {
                    if (CheckAnItemInInventory(ItemType.Pitchfork))
                        inventory.AddItem(other.gameObject.GetComponent<Item>().ItemData);
                }
                else if(tag == "BucketWithWater")
                {
                    inventory.AddItem(other.gameObject.GetComponent<Item>().ItemData);
                }
                else
                {
                    inventory.AddItem(other.gameObject.GetComponent<Item>().ItemData);
                    other.gameObject.GetComponent<Item>().DestroyItem();
                }
                    
            }
        }
    }

    private bool CheckAnItemInInventory(ItemType itemType)
    {
        foreach(ItemData item in inventory.itemDatas)
            if (item.itemType == itemType)
                return true;
        return false;
    }

    private void CheckQuest(Collider other)
    {
        if (other.CompareTag("Livestock"))
        {
            if (CheckAnItemInInventory(ItemType.Vegetables) && CheckAnItemInInventory(ItemType.BucketWithWater))
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    other.gameObject.GetComponent<QuestPlace>().Use();
                    inventory.RemoveItem(ItemType.Vegetables);
                    inventory.RemoveItem(ItemType.BucketWithWater);
                }
            }
        }
        if (other.CompareTag("Plant"))
        {
            if (CheckAnItemInInventory(ItemType.FertilizerBag) && CheckAnItemInInventory(ItemType.BucketWithWater))
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    other.gameObject.GetComponent<QuestPlace>().Use();
                    inventory.RemoveItem(ItemType.FertilizerBag);
                    inventory.RemoveItem(ItemType.BucketWithWater);
                }
            }
        }
        if (other.CompareTag("Garden"))
        {
            if (CheckAnItemInInventory(ItemType.BucketWithWater))
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    other.gameObject.GetComponent<QuestPlace>().Use();
                    inventory.RemoveItem(ItemType.BucketWithWater);
                }
            }
        }
        if (other.CompareTag("Cattle"))
        {
            if (CheckAnItemInInventory(ItemType.HayForLivestock) && CheckAnItemInInventory(ItemType.BucketWithWater))
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    other.gameObject.GetComponent<QuestPlace>().Use();
                    inventory.RemoveItem(ItemType.HayForLivestock);
                    inventory.RemoveItem(ItemType.BucketWithWater);
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Door") && Input.GetKey(KeyCode.F))
            other.gameObject.GetComponent<Door>().UseDoor();

        foreach (string str in items)
            FindAnItem(other, str);

        if (other.CompareTag("NPC") && Input.GetKey(KeyCode.R) && !isQuest && !isDayEnd)
            other.gameObject.GetComponent<Dialog>().TurnDialog(true);
        if(isQuest)
            CheckQuest(other);
        if (other.CompareTag("Bed") && Input.GetKeyDown(KeyCode.E) && isDayEnd)
            other.gameObject.GetComponent<Bed>().Sleep();

    }

    private void OnTriggerExit(Collider other)
    {
        text.text = "";
    }

    public void DecreaseEndurance(bool yes)
    {
        endurance.value -= 0.19f;
    }

    public void Quest(bool isQuest) =>
        this.isQuest = isQuest;

    public void DayEnd(bool isDayEnd)
    {
        this.isDayEnd = isDayEnd;
        if (!isDayEnd)
            endurance.value = endurance.maxValue;
    }
}
