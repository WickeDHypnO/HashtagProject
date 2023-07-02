using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObject/Item")]
public class Item: ScriptableObject
{
    public ItemId id;
    public string itemName;
    public int size;
    public int maxCharges;
    public ItemType itemType;
    public List<Action> actions;
    public Sprite itemGraphic;

    public bool Use()
    {
        maxCharges--;
        return maxCharges == 0;
    }
}
