using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EnemyBuilder
{
    public List<EnemyDetails> availableEnemies = new List<EnemyDetails>()
    {
        new EnemyDetails()
        {
            type = EnemyType.SmallMonster,
            name = "Small Monster",
            minHp = 5, 
            maxHp = 10,
            actions = new List<Action>()
            {
                new Action()
                {
                    name = "Small attack",
                    type = ActionType.Damage,
                    target = ActionTarget.Enemy,
                    value = 3
                }
            }
        },
        new EnemyDetails()
        {
            type = EnemyType.MediumMonster,
            name = "Medium Monster",
            minHp = 15, 
            maxHp = 25,
            actions = new List<Action>()
            {
                new Action()
                {
                    name = "Medium attack",
                    type = ActionType.Damage,
                    target = ActionTarget.Enemy,
                    value = 8
                },
                new Action()
                {
                    name = "Heal",
                    type = ActionType.Heal,
                    target = ActionTarget.User,
                    value = 5
                }
            }
        }
    };

    public EnemyDetails GenerateEnemy(EnemyType enemyType)
    {
        return availableEnemies.Single(enemy => enemy.type == enemyType);
    }
}

