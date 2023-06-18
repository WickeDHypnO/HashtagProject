using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private InventoryManager inventoryManager;
    // Start is called before the first frame update

    InventoryUI()
    {
        inventoryManager = new InventoryManager();
    }
}
