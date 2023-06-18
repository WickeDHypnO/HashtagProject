using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class InventoryManagerTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void InventoryManager_AddItems_AddsNewItem()
    {
        InventoryManager manager = new InventoryManager();
        ItemBuilder builder = new ItemBuilder();

        var item = builder.GenerateItem(ItemId.SmallSword);
        var wasAddedSuccesfully = manager.AddItem(item);

        Assert.True(wasAddedSuccesfully);
        Assert.Contains(item, manager.GetItemList());
        Assert.AreEqual(manager.currentSize, item.size);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [Test]
    public void InventoryManager_AddItems_DoesntAddItemsWhenSizeExceeded()
    {
        InventoryManager manager = new InventoryManager();
        manager.maxSize = 2;
        ItemBuilder builder = new ItemBuilder();

        var item = builder.GenerateItem(ItemId.BigSword);
        var wasAddedSuccesfully = manager.AddItem(item);

        Assert.False(wasAddedSuccesfully);
        Assert.IsEmpty(manager.GetItemList());
    }
}
