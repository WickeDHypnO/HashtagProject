using System.Collections;
using System.Collections.Generic;
using UnityEditor.Graphs;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryTile : MonoBehaviour, IPointerClickHandler, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Item item;
    public ItemType _slotType = ItemType.None;
    public Sprite emptyTileSprite;
    private Image _tileContent;
    protected InventoryManager _inventoryManager;
    private Vector2 _startingPosition;
    private TooltipUi _tooltipUi;

    private void Awake()
    {
        _tileContent = transform.GetChild(0).GetComponent<Image>();
        _tooltipUi = FindFirstObjectByType<TooltipUi>();
    }

    public void FillTile(Item item, InventoryManager inventoryManager)
    {
        this.item = item;
        _inventoryManager = inventoryManager;
        _tileContent.color = new Color(255, 255, 255, 255);
        _tileContent.sprite = item.itemGraphic;

    }

    private void clearTile()
    {
        this.item = null;
        _tileContent.color = new Color(255, 255, 255, 0);
        _tileContent.sprite = null;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startingPosition = _tileContent.transform.localPosition;
        _tileContent.raycastTarget = false;
        var canvas = _tileContent.gameObject.AddComponent<Canvas>();
        canvas.overrideSorting = true;
        canvas.sortingOrder = 999;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _tileContent.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _tileContent.raycastTarget = true;
        Destroy(_tileContent.GetComponent<Canvas>());

        var target = eventData.pointerCurrentRaycast.gameObject;

        if (target == null || target.GetComponentInParent<InventoryTile>() == null || target.GetComponentInParent<InventoryTile>()._slotType == ItemType.None)
        {
            _tileContent.transform.localPosition = _startingPosition;
            return;
        }
        var targetInventoryTile = target.GetComponentInParent<InventoryTile>(); 
        var isItemBeingEquiped = targetInventoryTile._slotType == item.itemType && !item.isEquipped;

        if (isItemBeingEquiped)
        {
            _inventoryManager.TakeOffItem(targetInventoryTile.item);
            _inventoryManager.EquipItem(item);
            //_slotType = targetInventoryTile._slotType;
            var temp = targetInventoryTile.item;
            targetInventoryTile.FillTile(item, _inventoryManager);
            if (temp)
            {
                FillTile(temp, _inventoryManager);
            }
            else
            {
                clearTile();
            }
        }
        //if (isItemBeingEquiped || targetInventoryTile._slotType == ItemType.Backpack)
        //{

        //    //_tileContent.transform.position = targetInventoryTile._tileContent.transform.position;
        //    //targetInventoryTile._tileContent.transform.position = _startingPosition;
        //}
        if(targetInventoryTile._slotType == ItemType.Backpack)
        {
            _inventoryManager.TakeOffItem(item);
            targetInventoryTile.FillTile(item, _inventoryManager);
            clearTile();
        }
        _tileContent.transform.localPosition = _startingPosition;

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
        //else if(_inventoryManager.UseItem(item))
        //{
        //    DestroyItem();
        //};
    }

    public void DestroyItem()
    {
        _inventoryManager.TakeOffItem(item);
        clearTile();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item)
        {
            _tooltipUi.SetTooltipText(@$"{item.itemName} 
Charges: {item.currentCharges} / {item.maxCharges} 
Actions: {item.actions.Count}");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _tooltipUi.SetTooltipText("");
    }

}
