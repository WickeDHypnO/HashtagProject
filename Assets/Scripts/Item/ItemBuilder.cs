using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemBuilder: MonoBehaviour
{
    [SerializeField]
    List<Item> availableItems;

    public Item GenerateItem(ItemId itemId)
    {
        return availableItems.Single(item => item.id == itemId);
    }

    public Item GenerateRandomItem()
    {
        var rnd = new System.Random();
        return availableItems[rnd.Next(availableItems.Count)];
    }
}
