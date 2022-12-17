using System.Collections;
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
            slots[i].sprite = itemDatas[i].sprite;
        }
    }

    public void AddItem(ItemData item)
    {
        itemDatas.Add(item);
    }
}
