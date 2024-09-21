using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryManager : IDisposable
{
    Action<IInventoryItem> OnItemAdded { get; set; }
    Action<IInventoryItem> OnSelecterItem{ get; set; }


    Action<IInventoryItem> OnItemAddedFailed { get; set; }


    Action<IInventoryItem> OnItemRemoved { get; set; }



    Action OnRebuilt { get; set; }


    IInventoryItem[] AllItems { get; }


    bool Contains(IInventoryItem item);

    bool IsFull { get; }


    bool CanAdd(IInventoryItem item);


    bool TryAdd(IInventoryItem item);

    bool CanRemove(IInventoryItem item);

    bool TryRemove(IInventoryItem item);

    void SelectItem(IInventoryItem item);

    void UseItem(IInventoryItem item);

    void Clear();

    void Rebuild();

}
