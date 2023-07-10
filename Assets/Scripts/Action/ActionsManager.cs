using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActionsManager : MonoBehaviour
{
    [SerializeField]
    BattleState _battleState;
    [SerializeField]
    InventoryManager _inventoryManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExecutePlayerAction(Action action, Character user, Character target = null, ElementType itemElement = ElementType.None)
    {
        ExecuteAction(action, user, target, itemElement);
        _battleState.OnPlayerActionFinished();
    }

    public void ExecuteAction(Action action, Character user, Character target = null, ElementType usedElement = ElementType.None)
    {
       var targets = getTargets(action.target, user, target);
       foreach(var character in targets)
        {
            Debug.Log($"{user.characterName} used {action.name} on {character.name}");
            handleActionType(action, character, usedElement);
        }
    }

    private void handleActionType(Action action, Character target, ElementType usedElement)
    {
        switch (action.type)
        {
            case ActionType.Heal:
                target.currentHp += action.value;
                break;
            case ActionType.Damage:
                var damageAfterResistances = Mathf.RoundToInt(checkResistances(target.element, usedElement) * action.value);
                dealDamage(damageAfterResistances, target);
                break;
        }
    }

    private float checkResistances(ElementType targetElement, ElementType usedElement)
    {
        var usedElementStats = ElementalConsts.ElementalEmpower.FirstOrDefault(x => x.Type == usedElement);
        if(usedElementStats == null)
        {
            return 1;
        }
        if(usedElementStats.Weakness == targetElement) 
        {
            return 1.5f;
        }
        if(usedElementStats.Empowers == targetElement)
        {
            return 0.5f;
        }
        return 1;
    }

    private void dealDamage(int damageAfterResistances, Character target)
    {
        if(target.armor > 0)
        {
            target.armor -= damageAfterResistances;
            if(target.isPlayer)
            {
                //Make check if armor is from consumable, make some calculations
                _inventoryManager.ReduceCharges(ItemType.Shield, damageAfterResistances);
            }
            if (target.armor < 0)
            {
                target.currentHp -= Mathf.Abs(target.armor);
                target.armor = 0;
            }
        }
        else
        {
            target.currentHp -= damageAfterResistances;
        }
    }
    private List<Character> getTargets(ActionTarget actionTarget, Character user, Character target = null)
    {
        var targets = new List<Character>();
        switch (actionTarget)
        {
            case ActionTarget.Self:
                targets.Add(user.isPlayer ? _battleState.player : user);
                break;
            case ActionTarget.Target:
                targets.Add(user.isPlayer ? target : _battleState.player);
                break;
            case ActionTarget.AllTargets:
                if(user.isPlayer)
                {
                    targets.AddRange(_battleState.enemies);
                } 
                else
                {
                    targets.Add(_battleState.player);
                }
                break;
            case ActionTarget.Everyone:
                targets.AddRange(_battleState.enemies);
                targets.Add(_battleState.player);
                break;
        }
        return targets;
    }
}
