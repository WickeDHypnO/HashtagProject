using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    InventoryUI _inventoryUI;
    public Item item;
    // Start is called before the first frame update
    void Start()
    {
        _inventoryUI = FindFirstObjectByType<InventoryUI>();
    }

    public void AddItem()
    {
        if (_inventoryUI.AddItem(item))
        {
            Debug.Log(item.name + " added");
        }
        else
        {
            Debug.LogWarning(item.name + " not added");
        }
    }
}
