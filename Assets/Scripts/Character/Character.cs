using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public int id;
    public string characterName;
    public int currentHp;
    public int maxHp;
    public int actionsPerTurn = 1;
    public int speed;
} 
