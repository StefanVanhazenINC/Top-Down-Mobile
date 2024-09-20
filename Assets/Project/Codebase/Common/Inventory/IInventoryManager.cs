using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryManager : IDisposable
{
    Action<IInventoryItem> onItemAdded { get; set; }


    Action<IInventoryItem> onItemAddedFailed { get; set; }


    Action<IInventoryItem> onItemRemoved { get; set; }



    Action onRebuilt { get; set; }


    IInventoryItem[] allItems { get; }


    bool Contains(IInventoryItem item);

    bool isFull { get; }


    bool CanAdd(IInventoryItem item);


    bool TryAdd(IInventoryItem item);

    bool CanRemove(IInventoryItem item);

    bool TryRemove(IInventoryItem item);

    void Clear();

    void Rebuild();

}
