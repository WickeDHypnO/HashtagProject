using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryTile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Item _item;
    private InventoryManager _inventoryManager;

    public void FillTile(Item item, InventoryManager inventoryManager)
    {
        _item = Instantiate(item);
        _inventoryManager = inventoryManager;
        GetComponent<Image>().sprite = item.itemGraphic;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(_inventoryManager.UseItem(_item))
        {
            Destroy(gameObject);
        };
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Showing tooltip for" + _item.name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Hiding tooltip for" + _item.name);
    }
    
}
