using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private string name;

    public ItemData ItemData => itemData;

    public void DestroyItem()
    {
        Destroy(gameObject);
    }

    public void View(TextMeshProUGUI text)
    {
        text.text = $"Нажмите E чтобы взять {name}";
    }
}
