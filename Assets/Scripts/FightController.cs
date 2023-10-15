using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightController : MonoBehaviour
{
    public static System.Action OnTurnEnd;
    public static System.Action OnPlayerTurn;
    public PlayerController player;
    [SerializeField]
    public List<EnemyController> enemies;
    private Queue<EntityController> turnQueue;
    [SerializeField]
    private ActionsManager _actionsManager;

    private void OnEnable()
    {
        var enemies = new List<Character>();
        _actionsManager = FindObjectOfType<ActionsManager>();
        var enemy = FindObjectOfType<EnemyBuilder>().GenerateRandomEnemy();
        enemies.Add(enemy);
        //var enemy2 = FindObjectOfType<EnemyBuilder>().GenerateRandomEnemy();
        //enemies.Add(enemy2);
        player = FindObjectOfType<PlayerController>();
        InitializeFight(enemies);
    }

    public void InitializeFight(List<Character> enemyData)
    {
        turnQueue = new Queue<EntityController>();
        player.InitilizePlayer();
        turnQueue.Enqueue(player);
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemyData.Count > i)
            {
                enemies[i].InitializeEnemy(enemyData[i], _actionsManager);
                turnQueue.Enqueue(enemies[i]);
            }
            else
            {
                enemies[i].gameObject.SetActive(false);
            }
        }
        NextTurn();
    }

    [ContextMenu("Next turn")]
    public void NextTurn()
    {
        if (turnQueue.Count > 1)
        {
            var currentTurnEntity = turnQueue.Dequeue();
            if (currentTurnEntity is PlayerController)
            {
                PlayerTurn();
            }
            else
            {
                EnemyTurn(currentTurnEntity as EnemyController);
                if (turnQueue.Count == 1)
                {
                    EndFight(true);
                }
            }
            turnQueue.Enqueue(currentTurnEntity);

        }
        else
        {
            EndFight(true);
        }
    }

    private void EndFight(bool won)
    {
        if (won)
        {
            FindObjectOfType<MapUI>().ClearTile();
        }
        Debug.Log("deadge");
        return;
        //TODO: Fight ended, allow traversal to next room or end game if lost fight
    }

    private void PlayerTurn()
    {
        if (OnPlayerTurn != null)
        {
            OnPlayerTurn();
        }
        player.OnAttackEnded += NextTurn;
    }

    private void EnemyTurn(EnemyController enemy)
    {
        if (enemy.IsDead())
        {
            enemy.RemoveEnemy();
            return;
        }
        enemy.OnAttackEnded += NextTurn;

        enemy.EvaluateOptions();

        if (player.currentData.currentHp <= 0)
        {
            EndFight(false);
        }
    }
}
