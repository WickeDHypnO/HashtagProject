using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleAction : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Action _action;
    private TooltipUi _tooltipUi;
    private InventoryManager _inventoryManager;
    private BattleSkillbar _battleSkillbar;
    public Item item;
    public void OnPointerClick(PointerEventData eventData)
    {
        if(_inventoryManager.UseItem(item, _action))
        {
            _battleSkillbar.RemoveAction(item);
            FindObjectsByType<InventoryTile>(FindObjectsSortMode.None).Where(x => x.item == item).First().DestroyItem();
        }
        _tooltipUi.SetTooltipText(@$"{_action.actionName} - {_action.type.ToString()} {_action.value} {_action.target} 
Charges: {item.currentCharges} / {item.maxCharges}");
        //_actionsManager.ExecutePlayerAction(_action, FindFirstObjectByType<FightController>().player.currentData, FindFirstObjectByType<FightController>().enemies[0].currentData, item.elementType);
        //FindFirstObjectByType<FightController>().NextTurn();
    }

    public void Generate(Action action, Item item, TooltipUi tooltipUi, InventoryManager inventoryManager, BattleSkillbar battleSkillbar)
    {
        _tooltipUi = tooltipUi;
        _inventoryManager = inventoryManager;
        _battleSkillbar = battleSkillbar;
        this.item = item;
        GetComponent<Image>().sprite = item.itemGraphic;
        _action = action;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _tooltipUi.SetTooltipText(@$"{_action.actionName} - {_action.type.ToString()} {_action.value} {_action.target} 
Charges: {item.currentCharges} / {item.maxCharges}");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _tooltipUi.SetTooltipText("");
    }

}
