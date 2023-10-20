using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ChestUI : MonoBehaviour
{
    [SerializeField]
    InventoryManager _inventoryManager;
    [SerializeField]
    ItemBuilder _itemBuilder;
    [SerializeField]
    List<Item> _items = new List<Item>();
    public InventoryTile itemPrefab;

    private void Start()
    {
        GenerateChest();
    }
    public void GenerateChest()
    {
        for (int i = 0; i < 3; i++)
        {
            var prefab = Instantiate(itemPrefab, transform);
            var item = Instantiate(_itemBuilder.GenerateRandomItem());
            prefab.FillTile(item, _inventoryManager);
            prefab.transform.localPosition = new Vector3(Random.Range(-300f, -340f), Random.Range(-50f, -30f));
        }
    }
}
