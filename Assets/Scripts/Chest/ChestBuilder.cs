using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ChestBuilder : MonoBehaviour
{
    [SerializeField] 
    private ItemBuilder _itemBuilder;

    public List<Item> GenerateChestItems(int amountOfItems)
    {
        var items = new List<Item>();
        for (int i = 0; i < amountOfItems; i++)
        {
            var item = _itemBuilder.GenerateRandomItem();
            items.Add(item);
        }
        return items;
    }
}
