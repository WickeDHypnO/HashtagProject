using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : EntityController
{
    ActionsManager _actionsManager;
    [SerializeField]
    GameObject _hpBar;
    public void InitializeEnemy(Character enemyData, ActionsManager actionsManager)
    {
        currentData = Instantiate(enemyData);
        _actionsManager = actionsManager;
        GetComponent<SpriteRenderer>().sprite = enemyData.image;
    }

    public void RemoveEnemy()
    {
        currentData = null;
        GetComponent<SpriteRenderer>().sprite = null;
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
        _actionsManager.ExecuteAction(currentData.GetRandomAction(), currentData, null, currentData.element);
        Attack();
    }

    private void FixedUpdate()
    {
        if (currentData)
        {
            _hpBar.transform.localScale = new Vector2((float)currentData.currentHp / (float)currentData.maxHp, _hpBar.transform.localScale.y);
        }
    }
}
