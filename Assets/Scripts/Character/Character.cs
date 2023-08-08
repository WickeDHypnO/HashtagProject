using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObject/Character")]
public class Character : ScriptableObject
{
    public int id;
    public string characterName;
    private int _currentHp;
    public Sprite image;

    public int currentHp { 
        get
        {
            return _currentHp;

        } 
        set
        {
            _currentHp = value;
            if(isPlayer)
            {
                battleUi.SetPlayerHP(_currentHp, maxHp);
            } 
            else
            {
                //TODO: Create prefab for enemies and add reference to slider above enemy head

            }
            Debug.Log(characterName + " HP is now: " + _currentHp);
        }
    }
    public int maxHp;
    private int _currentArmor;
    public int currentArmor
    {
        get { return _currentArmor; }
        set
        {
            _currentArmor = value;
            if (isPlayer)
            {
                battleUi.SetPlayerArmor(_currentArmor, maxArmor);
            }
            else
            {
                //TODO: Create prefab for enemies and add reference to slider above enemy head
            }
            Debug.Log(characterName + " Armor is now: " + _currentArmor);

        }
    }
    public int maxArmor;
    public int actionsPerTurn = 1;
    public int speed;
    public bool isPlayer;
    public ElementType element;
    private BattleUI battleUi;
    public List<Action> actions;
    void Awake()
    {
        battleUi = FindObjectOfType<BattleUI>();
        currentHp = maxHp;
    }

    public Action GetRandomAction()
    {
        return actions[Random.Range(0, actions.Count)];
    }
} 
