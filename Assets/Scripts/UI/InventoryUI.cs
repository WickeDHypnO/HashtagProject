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

    public void AddItem(InventoryTile inventoryTile)
    {
        var position = new Vector2(_firstItemPosition.x + (40 * _inventoryManager.GetItemList().Count), _firstItemPosition.y);
        inventoryTile.transform.position = position;
        inventoryTiles.Add(inventoryTile);
    }

}
