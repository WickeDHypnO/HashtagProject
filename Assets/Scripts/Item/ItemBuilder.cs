using System.Collections.Generic;
using System.Linq;

public class ItemBuilder
{
    public List<Item> availableItems = new List<Item>()
    {
        new Item()
        {
            id = ItemId.SmallSword,
            name = "Small Sword",
            size = 2,
            maxCharges = 5,
            itemType = ItemType.Weapon,
            actions = new List<Action>
            {
                new Action()
                {
                    name = "Attack",
                    target = ActionTarget.Enemy,
                    type = ActionType.Damage,
                    value = 5
                }
            }
        },
        new Item()
        {
            id = ItemId.SmallPotion,
            name = "Small Potion",
            size = 1,
            maxCharges = 1,
            itemType = ItemType.Consumable,
            actions = new List<Action>
            {
                new Action()
                {
                    name = "Heal",
                    target = ActionTarget.User,
                    type = ActionType.Heal,
                    value = 10
                }
            }
        },
        new Item()
        {
            id = ItemId.BigSword,
            name = "Big Sword",
            size = 4,
            maxCharges = 3,
            itemType = ItemType.Weapon,
            actions = new List<Action>
            {
                new Action()
                {
                    name = "Attack All",
                    target = ActionTarget.AllEnemies,
                    type = ActionType.Damage,
                    value = 7
                },
                new Action()
                {
                    name = "Attack",
                    target = ActionTarget.Enemy,
                    type = ActionType.Damage,
                    value = 20
                }
            }
        }
    };

    public Item GenerateItem(ItemId itemId)
    {
        return availableItems.Single(item => item.id == itemId);
    }
}
