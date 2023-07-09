using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    InventoryManager _inventoryManager;
    [SerializeField]
    Vector2 _firstItemPosition;
    [SerializeField]
    InventoryTile _tilePlaceholder;
    public List<InventoryTile> inventoryTiles = new List<InventoryTile>();

    public bool AddItem(Item item)
    {
        if(_inventoryManager.CanAddItem(item))
        {
            var position = new Vector2(_firstItemPosition.x + (40 * _inventoryManager.GetItemList().Count), _firstItemPosition.y);
            var tile = Instantiate(_tilePlaceholder, position, Quaternion.identity, transform);
            tile.FillTile(item, _inventoryManager);
            inventoryTiles.Add(tile);
            return true;
        }
        return false;
    }

}
