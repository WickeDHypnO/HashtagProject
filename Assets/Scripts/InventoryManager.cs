using System.Collections.Generic;
using System.Linq;
using UnityEditor.Search;

public class InventoryManager
{
    public int maxSize = 10;
    List<Item> items = new List<Item>();

    public int currentSize => items.Sum(i => i.size);
    public bool AddItem(Item item)
    {
        if (currentSize + item.size > maxSize)
        {
            return false;
        }
        items.Add(item);
        return true;
    }

    public bool RemoveItem(Item item) 
    {
        items.Remove(item);
        return true;
    }

    public Action UseItem(Item item)
    {
        if(item.Use())
        {
            items.Remove(item);
        }; 
        //Hardcoded use action[0], if multiple choices => base action on clicked UI element
        return item.actions[0];
    }
    
    public List<Item> GetItemList()
    {
        return items;
    }

}