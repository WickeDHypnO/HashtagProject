using System.Collections.Generic;
using System.Linq;
using UnityEditor.Search;
using UnityEngine;

public class InventoryManager: MonoBehaviour
{
    public int maxSize = 10;
    [SerializeField]
    List<Item> _items = new List<Item>();
    [SerializeField]
    ActionsManager _actionsManager;
    [SerializeField]
    BattleState _battleState;

    public int currentSize => _items.Sum(i => i.size);
    public bool AddItem(Item item)
    {
        if (currentSize + item.size > maxSize)
        {
            return false;
        }
        _items.Add(item);
        return true;
    }

    public bool RemoveItem(Item item) 
    {
        _items.Remove(item);
        return true;
    }

    public bool UseItem(Item item)
    {
        //TODO: Hardcoded use action[0], if multiple choices => base action on clicked UI element + hardcoded target to first enemy
        _actionsManager.ExecuteAction(item.actions[0], _battleState.player, _battleState.enemies[0]);
        if (item.Use())
        {
            _items.Remove(item);
            return true;
        };
        return false;

    }
    
    public List<Item> GetItemList()
    {
        return _items;
    }

}