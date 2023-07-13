using System.Collections;
using System.Collections.Generic;
using UnityEditor.Graphs;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryTile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public Item item;
    public ItemType _slotType = ItemType.None;
    protected InventoryManager _inventoryManager;
    private Vector2 _startingPosition;
    public void FillTile(Item item, InventoryManager inventoryManager)
    {
        this.item = Instantiate(item);
        _inventoryManager = inventoryManager;
        GetComponent<Image>().sprite = item.itemGraphic;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startingPosition = transform.position;
        GetComponent<Image>().raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<Image>().raycastTarget = true;
        var targetInventoryTile = eventData.pointerCurrentRaycast.gameObject.GetComponent<InventoryTile>();
        if (targetInventoryTile != null && targetInventoryTile._slotType != ItemType.None)
        {
            var isItemBeingEquiped = targetInventoryTile._slotType == item.itemType;
            if (isItemBeingEquiped)
            {
                _inventoryManager.TakeOffItem(targetInventoryTile.item);
                _inventoryManager.EquipItem(item);
                _slotType = targetInventoryTile._slotType;
            }
            if(isItemBeingEquiped || targetInventoryTile._slotType == ItemType.Backpack)
            {
                transform.position = targetInventoryTile.transform.position;
                targetInventoryTile.transform.position = _startingPosition;
            }

            return;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.dragging)
        {
            return;
        }

        if (_slotType == ItemType.None)
        {
            if (_inventoryManager.CanAddItem(item))
            {
                _inventoryManager.AddItem(this);
                _slotType = ItemType.Backpack;
            }
            else
            {
                Debug.LogWarning(item.name + " not added");
            }
        }
        else if(_inventoryManager.UseItem(item))
        {
            DestroyItem();
        };
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Showing tooltip for" + item.name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Hiding tooltip for" + item.name);
    }
    
}
