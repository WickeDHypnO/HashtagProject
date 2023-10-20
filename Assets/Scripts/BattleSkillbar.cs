using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSkillbar : MonoBehaviour
{
    [SerializeField] 
    BattleAction _actionPrefab;
    [SerializeField]
    InventoryManager _inventoryManager;
    [SerializeField]
    TooltipUi _tooltipUi;
    List<BattleAction> actions = new List<BattleAction>();

    public void AddAction(Item item)
    {
        foreach(var action in item.actions) 
        {
            var prefab = Instantiate(_actionPrefab);
            prefab.Generate(action, item, _tooltipUi, _inventoryManager, this);
            prefab.transform.SetParent(transform, false);
            actions.Add(prefab);
        }

    }

    public void RemoveAction(Item item)
    {
        var relatedActions = actions.FindAll(x => x.item == item);
        foreach(var action in relatedActions)
        {
            actions.Remove(action);
            Destroy(action.gameObject);
        }

    }
}
