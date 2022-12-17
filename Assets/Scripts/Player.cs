using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private TextMeshProUGUI text;
    private string[] items;
    
    void Start()
    {
        items = new string[] { "BucketWithWater", "Vegetables", "FertilizerBag", "Pitchfork", "EmptyBucket", "HayForLivestock" };
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }

    private void FindAnItem(Collider other, string tag)
    {
        if (other.CompareTag(tag) && Input.GetKey(KeyCode.E))
        {
            inventory.AddItem(other.gameObject.GetComponent<Item>().ItemData);
            other.gameObject.GetComponent<Item>().DestroyItem();
            text.text = "";
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Door") && Input.GetKey(KeyCode.F))
            other.gameObject.GetComponent<Door>().UseDoor();

        foreach (string str in items)
            FindAnItem(other, str);
    }

    private void OnTriggerExit(Collider other)
    {
        text.text = "";
    }
}
