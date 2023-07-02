using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsManager : MonoBehaviour
{
    [SerializeField]
    BattleState _battleState;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExecutePlayerAction(Action action, Character user, Character target = null)
    {
        ExecuteAction(action, user, target);
        _battleState.OnPlayerActionFinished();
    }

    public void ExecuteAction(Action action, Character user, Character target = null)
    {
       var targets = getTargets(action.target, user, target);
       foreach(var character in targets)
        {
            Debug.Log($"{user.characterName} used {action.name} on {character.name}");
            handleActionType(action, character);
        }
    }

    private void handleActionType(Action action, Character target)
    {
        switch (action.type)
        {
            case ActionType.Heal:
                target.currentHp += action.value;
                break;
            case ActionType.Damage:
                target.currentHp -= action.value;
                break;
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
