using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Image> slots;
    public List<ItemData> itemDatas { get; private set; }

    void Start()
    {
        itemDatas = new List<ItemData>();
    }

    void Update()
    {
        for (int i = 0; i < itemDatas.Count; i++)
        {
            if(itemDatas[i] != null)
                slots[i].sprite = itemDatas[i].sprite;
        }
    }

    public void AddItem(ItemData item)
    {
        itemDatas.Add(item);
    }

    public void RemoveItem(ItemType itemType)
    {
        for (int i = 0; i < itemDatas.Count; i++)
        {
            if(itemDatas[i].itemType == itemType)
            {
                Debug.Log(itemDatas[i]);
                slots[i].sprite = null;
                itemDatas.Remove(itemDatas[i]);
                break;
            }
        }
    }
}
