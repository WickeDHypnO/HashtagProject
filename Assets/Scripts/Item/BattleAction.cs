using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleAction : MonoBehaviour, IPointerClickHandler
{
    private BattleState _battleState;
    private ActionsManager _actionsManager;
    private Action _action;
    public Item item;
    public void OnPointerClick(PointerEventData eventData)
    {
        _actionsManager.ExecutePlayerAction(_action, _battleState.player, _battleState.enemies[0], item.elementType);
        FindObjectOfType<FightController>().NextTurn();
    }

    public void Generate(Action action, Item item, ActionsManager actionsManager, BattleState battleState)
    {
        _actionsManager = actionsManager;
        _battleState = battleState;
        this.item = item;
        GetComponent<Image>().sprite = item.itemGraphic;
        _action = action;
    }
}
