using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleAction : MonoBehaviour, IPointerClickHandler
{
    private FightController _fightController;
    private ActionsManager _actionsManager;
    private Action _action;
    public Item item;
    public void OnPointerClick(PointerEventData eventData)
    {
        _actionsManager.ExecutePlayerAction(_action, FindObjectOfType<FightController>().player.currentData, FindObjectOfType<FightController>().enemies[0].currentData, item.elementType);
        FindObjectOfType<FightController>().NextTurn();
    }

    public void Generate(Action action, Item item, ActionsManager actionsManager, FightController fightController)
    {
        _actionsManager = actionsManager;
        _fightController = fightController;
        this.item = item;
        GetComponent<Image>().sprite = item.itemGraphic;
        _action = action;
    }
}
