using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryProvider 
{
    private List<IInventoryItem> _allItems = new List<IInventoryItem>();
    private int _maximumAlowedItemCount;
    private ItemTypes _allowedItem;

    public int InventoryItemCount => _allItems.Count;
    public bool isInventoryFull
    {
        get
        {
            if (_maximumAlowedItemCount < 0) 
                return false;

            if (InventoryItemCount >= _maximumAlowedItemCount)
            {
                for (int i = 0; i < _allItems.Count; i++)
                {
                    if (!_allItems[i].StackIsFull())
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
           
            
        }
    }

    public InventoryProvider(int maximumAlowedItemCount = -1, ItemTypes allowedItem = ItemTypes.Any) 
    {
        _maximumAlowedItemCount = maximumAlowedItemCount;
        _allowedItem = allowedItem;
    }
    public bool AddInventoryItem(IInventoryItem item)
    {
        if (!_allItems.Contains(item))
        {
            if (ContainsToIdNotFull(item, out IInventoryItem findItem))
            {
                Debug.Log(findItem.StackIsFull());
                if (!findItem.TryAddToStack()) 
                {
                    _allItems.Add(item);
                }
            }
            else 
            {
                _allItems.Add(item);
            }
            return true;
        }
        return false;
    }
    public bool ContainsToId(IInventoryItem item, out IInventoryItem findItem)
    {
        findItem = null;
        for (int i = 0; i < _allItems.Count; i++)
        {
            if (_allItems[i].IdItem == item.IdItem) 
            {
                findItem= _allItems[i];
                return true;
            }
        }
        return false;
    }
    public bool ContainsToIdNotFull(IInventoryItem item, out IInventoryItem findItem)
    {
        findItem = null;
        for (int i = 0; i < _allItems.Count; i++)
        {
            if (_allItems[i].IdItem == item.IdItem && !_allItems[i].StackIsFull())
            {
                findItem = _allItems[i];
                return true;
            }
        }
        return false;
    }
    public bool RemoveInventoryItem(IInventoryItem item) 
    {
        if (ContainsToId(item, out IInventoryItem findItem))
        {
            if (!findItem.TryRemoveStack())
            {
                return _allItems.Remove(item);
            }
            else
            {
                return true;
            }
        }
        else 
        {
            Debug.Log(5);
            return false;
        }
    }
    public IInventoryItem GetInventoryItem(int index) 
    {
        return _allItems[index];
    }
    public bool CanAddInventory(IInventoryItem item) 
    {
        if (_allowedItem == ItemTypes.Any) return true;
        return (item as ItemDefinition).ItemType == _allowedItem;
    }
    public bool CanRemoveInventoryItem(IInventoryItem item)
    {
        return true;
    }
    public bool IsFullInventory() 
    {
        if (_allItems.Count >= _maximumAlowedItemCount)
        {
            for (int i = 0; i < _allItems.Count; i++)
            {
                if (!_allItems[i].StackIsFull()) 
                {
                    return false;
                }
            }
            return true;
            //проверить на €чейку стака
        }
        else 
        {
            return false;
        }
    }

 
}
