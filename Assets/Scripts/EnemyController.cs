using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : EntityController
{
    public void InitializeEnemy(Character enemyData)
    {
        currentData = enemyData;
        GetComponent<SpriteRenderer>().sprite = enemyData.image;
    }

    public void EnemyAttackEnd()
    {
        if (OnAttackEnded != null)
        {
            OnAttackEnded();
        }
        OnAttackEnded = null;
    }

    public void EvaluateOptions()
    {
        //TODO: Make some AI to decide what to do, attack for now
        Attack();
    }
}
