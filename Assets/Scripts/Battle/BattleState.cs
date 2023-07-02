using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState : MonoBehaviour
{
    [SerializeField]
    EnemyBuilder _enemyBuilder;
    [SerializeField]
    ActionsManager _actionsManager;
    public Character player;
    public List<Character> enemies = new List<Character>();
    public List<Character> actionQueue = new List<Character>();
    // Start is called before the first frame update
    void Start()
    {
        player = Instantiate(player);
        LoadBattle();
    }

    public void LoadBattle()
    {
        enemies.Add(_enemyBuilder.GenerateRandomEnemy());
        actionQueue.Add(player);
        actionQueue.AddRange(enemies);
    }

    public void OnPlayerActionFinished()
    {
        foreach(Character enemy in enemies) 
        {
            _actionsManager.ExecuteAction(enemy.GetRandomAction(), enemy);
        }
    }
}
