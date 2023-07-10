using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Action", menuName = "ScriptableObject/Action")]
public class Action: ScriptableObject
{
    public int id;
    public string actionName;
    public ActionType type;
    public ActionTarget target;
    public int value;
}
