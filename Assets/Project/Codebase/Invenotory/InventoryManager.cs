using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : IInventoryManager
{
    private InventoryProvider _provider;

    public InventoryManager(InventoryProvider provider)
    {
        _provider = provider;
        allItems = new IInventoryItem[0];
    }

    public Action<IInventoryItem> onItemAdded { get ; set ; }
    public Action<IInventoryItem> onItemAddedFailed { get; set ; }
    public Action<IInventoryItem> onItemRemoved { get; set; }
    public Action<IInventoryItem> onItemRemovedFailed { get; set; }
    public Action onRebuilt { get; set; }

    public IInventoryItem[] allItems { get; private set; }

    public bool isFull 
    {
        get
        {
            return _provider.isInventoryFull;
        }
    }
    public void Dispose()
    {
        _provider = null;
        allItems = null;
    }
    public void Rebuild()
    {
        Rebuild(false);
    }
    private void Rebuild(bool silent)
    {
        allItems = new IInventoryItem[_provider.InventoryItemCount];
        for (var i = 0; i < _provider.InventoryItemCount; i++)
        {
            allItems[i] = _provider.GetInventoryItem(i);
        }
        if (!silent) 
        {
            onRebuilt?.Invoke();
        }
    }

    public bool Contains(IInventoryItem item) 
    {
        Debug.Log(allItems);
        return allItems.Contains(item);
    
    }
    public bool ContainsToId(IInventoryItem item, out IInventoryItem findItem)
    {
        findItem = null;
        for (int i = 0; i < allItems.Length; i++)
        {
            if (allItems[i].IdItem == item.IdItem)
            {
                findItem = allItems[i];
                return true;
            }
        }
        return false;
    }


   
    public bool TryAdd(IInventoryItem item)
    {

        if (!CanAdd(item) || isFull )
        {

            onItemAddedFailed?.Invoke(item);
            return false;
        }
        if (!_provider.AddInventoryItem(item)) 
        {
            onItemAddedFailed?.Invoke(item);
            return false;
        }

        onItemAdded?.Invoke(item);
        Rebuild(false);
        return true;
    }
    public bool CanAdd(IInventoryItem item)
    {
        if (!Contains(item)) 
        {
            return true;
        }
        return false;
    }


    public bool CanRemove(IInventoryItem item) => Contains(item) && _provider.CanRemoveInventoryItem(item);
    public bool CanRemoveId(IInventoryItem item, out IInventoryItem findItem) => ContainsToId(item, out findItem) && _provider.CanRemoveInventoryItem(item);
    public bool TryRemove(IInventoryItem item)
    {
        if (!CanRemove(item)) 
        {
            onItemRemovedFailed?.Invoke(item);
            return false;
        }
        if (!_provider.RemoveInventoryItem(item)) 
        {
            onItemRemovedFailed?.Invoke(item);
            return false;
        }
        onItemRemoved?.Invoke(item);
        Rebuild(true);
        return true;
    }
    public bool TryRemoveToId(IInventoryItem item, int value = 1)
    {

        if (!CanRemoveId(item, out IInventoryItem findItem))
        {
            onItemRemovedFailed?.Invoke(item);
            return false;
        }
        if (!_provider.RemoveInventoryItem(findItem))
        {
            onItemRemovedFailed?.Invoke(item);
            return false;
        }

        onItemRemoved?.Invoke(findItem);
        Rebuild(false);
        return true;
    }
    public void Clear()
    {
        foreach (var item in allItems)
        {
            TryRemove(item);
        }
    }

}
