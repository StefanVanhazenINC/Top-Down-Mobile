using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryStorage : MonoBehaviour
{
    [SerializeField] private InventoryRenderer _inventoryRenderer;
    [SerializeField] private int _maximumAlowedItemCount;
    [SerializeField] private ItemDefinition[] _baseItem = null;//сюда будет считка из JSON
    [SerializeField] private ItemTypes _allowedItem = ItemTypes.Any;

    private InventoryManager _inventory;
    private InventoryProvider _inventoryProvider;

    public InventoryManager Inventory { get => _inventory; }

    private void Awake() 
    {
        Construct();
    }
    private void Construct() 
    {
        _inventoryProvider = new InventoryProvider( _maximumAlowedItemCount, _allowedItem);
        _inventory = new InventoryManager(_inventoryProvider);
        SetDefaultItem();
        _inventoryRenderer.SetInvetory(_inventory);

        _inventory.onItemRemovedFailed += (item) =>
        {
            OnItemRemoveFailde(item);
        };

        _inventory.onItemAddedFailed += (item) =>
        {
            OnItemAddedFailde(item);
        };
    }
    private void SetDefaultItem() 
    {
        for (int i = 0; i < _baseItem.Length; i++)
        {
            _inventory.TryAdd(_baseItem[i].CreateInstance());
        }
    }

    
    private void OnItemRemoveFailde(IInventoryItem item)
    {
        Debug.Log($"You can't remove {(item as ItemDefinition).Name} there!");
    }

    private void OnItemAddedFailde(IInventoryItem item)
    {
        Debug.Log($"You can't put {(item as ItemDefinition).Name} there!");
    }
   
}
