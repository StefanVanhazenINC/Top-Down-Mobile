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
        AllItems = new IInventoryItem[0];
    }

    public Action<IInventoryItem> OnItemAdded { get ; set ; }
    public Action<IInventoryItem> OnItemAddedFailed { get; set ; }
    public Action<IInventoryItem> OnItemRemoved { get; set; }
    public Action<IInventoryItem> OnItemRemovedFailed { get; set; }
    public Action<IInventoryItem> OnSelecterItem { get ; set ; }
    public Action<IInventoryItem> OnUseItem { get ; set ; }
    public Action OnRebuilt { get; set; }

    public IInventoryItem[] AllItems { get; private set; }

    public bool IsFull 
    {
        get
        {
            return _provider.isInventoryFull;
        }
    }

    public void Dispose()
    {
        _provider = null;
        AllItems = null;
    }
    public void Rebuild()
    {
        Rebuild(false);
    }
    private void Rebuild(bool silent)
    {
        AllItems = new IInventoryItem[_provider.InventoryItemCount];
        for (var i = 0; i < _provider.InventoryItemCount; i++)
        {
            AllItems[i] = _provider.GetInventoryItem(i);
        }
        if (!silent) 
        {
            OnRebuilt?.Invoke();
        }
    }

    public bool Contains(IInventoryItem item) 
    {
        Debug.Log(AllItems);
        return AllItems.Contains(item);
    
    }
    public bool ContainsToId(IInventoryItem item, out IInventoryItem findItem)
    {
        findItem = null;
        for (int i = 0; i < AllItems.Length; i++)
        {
            if (AllItems[i].IdItem == item.IdItem)
            {
                findItem = AllItems[i];
                return true;
            }
        }
        return false;
    }

    public void SelectItem(IInventoryItem item) 
    {
        Debug.Log(item);
        OnSelecterItem?.Invoke(item);
    }
   
    public bool TryAdd(IInventoryItem item)
    {

        if (!CanAdd(item) || IsFull )
        {

            OnItemAddedFailed?.Invoke(item);
            return false;
        }
        if (!_provider.AddInventoryItem(item)) 
        {
            OnItemAddedFailed?.Invoke(item);
            return false;
        }

        OnItemAdded?.Invoke(item);
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
            OnItemRemovedFailed?.Invoke(item);
            return false;
        }
        if (!_provider.RemoveInventoryItem(item)) 
        {
            OnItemRemovedFailed?.Invoke(item);
            return false;
        }
        OnItemRemoved?.Invoke(item);
        Rebuild(true);
        return true;
    }
    public bool TryRemoveToId(IInventoryItem item, int value = 1)
    {

        if (!CanRemoveId(item, out IInventoryItem findItem))
        {
            OnItemRemovedFailed?.Invoke(item);
            return false;
        }
        if (!_provider.RemoveInventoryItem(findItem))
        {
            OnItemRemovedFailed?.Invoke(item);
            return false;
        }

        OnItemRemoved?.Invoke(findItem);
        Rebuild(false);
        return true;
    }
    public void Clear()
    {
        foreach (var item in AllItems)
        {
            TryRemove(item);
        }
    }

    public void UseItem(IInventoryItem item)
    {
        if (item.ConditionItem == null)
        {
            Debug.Log("Предмет не имеет активного действия");
        }
        else 
        {
            OnUseItem?.Invoke(item);
        }
    }
}
