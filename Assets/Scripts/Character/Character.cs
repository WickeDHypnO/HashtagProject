using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObject/Character")]
public class Character : ScriptableObject
{
    public int id;
    public string characterName;
    private int _currentHp;
    public int currentHp { 
        get
        {
            return _currentHp;

        } 
        set
        {
            _currentHp = value;
            Debug.Log(characterName + " HP is now: " + _currentHp);
        }
    }
    public int maxHp;
    public int actionsPerTurn = 1;
    public int speed;
    public bool isPlayer;
    public List<Action> actions;
    void Awake()
    {
        _currentHp = maxHp;
    }

    public Action GetRandomAction()
    {
        return actions[Random.Range(0, actions.Count)];
    }
} 
