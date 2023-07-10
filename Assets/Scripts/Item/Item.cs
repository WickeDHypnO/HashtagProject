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
    public int currentCharges;
    public ItemType itemType;
    public bool isEquipped;
    public ElementType elementType;
    public List<Action> actions;
    public Sprite itemGraphic;

    private void Awake()
    {
        currentCharges = maxCharges;  
    }

    public bool Use()
    {
        currentCharges--;
        return currentCharges == 0;
    }

    public bool UseManyCharges(int amount)
    {
        currentCharges -= amount;
        return currentCharges <= 0;
    }

    
}
