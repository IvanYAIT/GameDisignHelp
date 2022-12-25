using System;
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
        for (int i = 0; i < 6; i++)
        {
            try
            {
                slots[i].sprite = itemDatas[i].sprite;
            }
            catch (Exception e)
            {
                slots[i].sprite = null;
            }
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
                slots[i].sprite = null;
                itemDatas.Remove(itemDatas[i]);
                break;
            }
        }
    }
}
