using Codice.Client.BaseCommands.Merge.Xml;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Search;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryManager: MonoBehaviour
{
    public int maxSize = 10;
    [SerializeField]
    List<Item> _items = new List<Item>();
    [SerializeField]
    ActionsManager _actionsManager;
    [SerializeField]
    BattleState _battleState;
    [SerializeField]
    InventoryUI _inventoryUI;

    public int currentSize => _items.Sum(i => i.size);
    public bool CanAddItem(Item item)
    {
        if (currentSize + item.size > maxSize)
        {
            return false;
        }
        return true;
    }

    public void AddItem(InventoryTile inventoryTile) 
    {
        _items.Add(inventoryTile.item);
        _inventoryUI.AddItem(inventoryTile);
    }

    public bool RemoveItem(Item item) 
    {
        _items.Remove(item);
        return true;
    }

    public bool UseItem(Item item)
    {
        if(!item.isEquipped && item.itemType != ItemType.Consumable)
        {
            Debug.Log($"{item.itemName} is not equipped");
            return false;
        }
        //TODO: Hardcoded use action[0], if multiple choices => base action on clicked UI element + hardcoded target to first enemy
        _actionsManager.ExecutePlayerAction(item.actions[0], _battleState.player, _battleState.enemies[0], item.elementType);
        
        if (item.Use())
        {
            _items.Remove(item);
            FindObjectOfType<FightController>().NextTurn();
            return true;
        };
        FindObjectOfType<FightController>().NextTurn();
        return false;

    }

    public void ReduceCharges(ItemType itemType, int amount)
    {
        var item = _items.Find(x => x.isEquipped && x.itemType == itemType);
        if(item == null)
        {
            return;
        }
        if(item.UseManyCharges(amount))
        {
            _items.Remove(item);
            _inventoryUI.backpackTiles.Find(x => x.item == item).DestroyItem();
        }
    }
    public void EquipItem(Item item)
    {
        item.isEquipped = true;
        handleEquipItem(item, true);
    }

    private void handleEquipItem(Item item, bool isEquiping)
    {
        switch(item.itemType)
        {
            case ItemType.Armor:
                _battleState.player.element = isEquiping ? item.elementType : ElementType.None;
                break;
            case ItemType.Shield:
                _battleState.player.currentArmor = isEquiping ? item.currentCharges : 0;
                break;
        }
    }

    public void TakeOffItem(Item item)
    {
        if (item != null)
        {
            handleEquipItem(item, false);
            item.isEquipped = false;
        }
    }

    public List<Item> GetItemList()
    {
        return _items;
    }

}