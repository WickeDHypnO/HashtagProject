using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestUI : MonoBehaviour
{
    [SerializeField]
    InventoryManager _inventoryManager;
    [SerializeField]
    ItemBuilder _itemBuilder;
    [SerializeField]
    List<Item> _items = new List<Item>();
    public InventoryTile itemPrefab;

    public void GenerateChest()
    {
        for (int i = 0; i < 10; i++)
        {
            var prefab = Instantiate(itemPrefab, transform);
            prefab.FillTile(_itemBuilder.GenerateRandomItem(), _inventoryManager);
            prefab.transform.position = new Vector3(Random.Range(200f, 400f), Random.Range(200f, 400f));
        }
    }
}
