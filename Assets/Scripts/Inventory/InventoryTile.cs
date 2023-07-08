using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryTile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    private Item _item;
    private InventoryManager _inventoryManager;
    private Vector2 _startingPosition;
    public void FillTile(Item item, InventoryManager inventoryManager)
    {
        _item = Instantiate(item);
        _inventoryManager = inventoryManager;
        GetComponent<Image>().sprite = item.itemGraphic;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startingPosition = transform.position;
        GetComponent<Image>().raycastTarget = false;
        Debug.Log("start");

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<Image>().raycastTarget = true;
        var targetInventoryTile = eventData.pointerCurrentRaycast.gameObject.GetComponent<InventoryTile>();
        if (targetInventoryTile != null)
        {
            transform.position = targetInventoryTile.transform.position;
            targetInventoryTile.transform.position = _startingPosition;
            if(targetInventoryTile.GetComponent<EquipmentInventoryTile>())
            {
                _inventoryManager.EquipItem(_item);
            }
            return;
        }

        Debug.Log("koniec");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.dragging)
        {
            return;
        }

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
