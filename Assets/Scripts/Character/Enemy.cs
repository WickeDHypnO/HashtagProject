using System;
using System.Collections.Generic;

public class Enemy: Character
{
    public EnemyDetails details;

    public Enemy()
    {
        speed = 2;
        actionsPerTurn = 1;
        LoadEnemyData();
    }
    public void LoadEnemyData()
    {
        //TODO: Dependency Injection
        EnemyBuilder builder = new EnemyBuilder();

        details = builder.GenerateEnemy(randomizeEnemyType());
        currentHp = new Random().Next(details.minHp, details.maxHp);
    }

    private EnemyType randomizeEnemyType()
    {
        Array values = Enum.GetValues(typeof(EnemyType));
        Random random = new Random();
        return (EnemyType)values.GetValue(random.Next(values.Length));
    }
}