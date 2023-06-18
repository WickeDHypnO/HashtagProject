using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public ItemId id;
    public string name;
    public int size;
    public int maxCharges;
    public int usedCharges;
    public ItemType itemType;
    public List<Action> actions;

    public bool Use()
    {
        ++usedCharges;
        return maxCharges != usedCharges;
    }
}
