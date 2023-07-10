using System.Collections.Generic;
using UnityEngine;

public class EnemyDetails: ScriptableObject
{
    public int id;
    public EnemyType type;
    public string EnemyName;
    public int maxHp;
    public int minHp;
    public List<Action> actions;
}
