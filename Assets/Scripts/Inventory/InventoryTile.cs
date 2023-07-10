using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryTile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public Item item;
    protected InventoryManager _inventoryManager;
    private Vector2 _startingPosition;
    public void FillTile(Item item, InventoryManager inventoryManager)
    {
        this.item = Instantiate(item);
        _inventoryManager = inventoryManager;
        _inventoryManager.AddItem(this.item);
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
        if (targetInventoryTile != null)
        {
            var equippedItem = targetInventoryTile.GetComponent<EquipmentInventoryTile>();
            var isItemBeingEquiped = equippedItem && equippedItem.slotType == item.itemType;

            if (equippedItem == null || isItemBeingEquiped)
            {
                transform.position = targetInventoryTile.transform.position;
                targetInventoryTile.transform.position = _startingPosition;
            }
            if (isItemBeingEquiped)
            {
                _inventoryManager.TakeOffItem(equippedItem.item);
                _inventoryManager.EquipItem(item);
            }
            else
            {
                Debug.Log($"{item} cannot be equiped in this slot");
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

        if(_inventoryManager.UseItem(item))
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
