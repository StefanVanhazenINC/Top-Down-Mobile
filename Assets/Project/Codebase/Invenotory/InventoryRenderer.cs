using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryRenderer : MonoBehaviour
{
    [SerializeField] private Transform _viewParent;
    [SerializeField] private ItemView[] _allView;
    private IInventoryManager _inventory;

    private ItemView _selectedView;

    private bool _haveListeners;

    public Action<IInventoryItem> OnSelectItem;
    private Dictionary<IInventoryItem, ItemView> _items = new Dictionary<IInventoryItem, ItemView>();
    void OnDisable()
    {
        if (_inventory != null && _haveListeners)
        {
            _inventory.OnRebuilt -= ReRenderAllItems;
            _inventory.OnItemAdded -= HandleItemAdded;
            _inventory.OnItemRemoved -= HandleItemRemoved;
            _haveListeners = false;
            ClearViewItems();
        }
    }
    void OnEnable()
    {
        if (_inventory != null && !_haveListeners)
        {
            SetViewItems();
            _inventory.OnRebuilt += ReRenderAllItems;
            _inventory.OnItemAdded += HandleItemAdded;
            _inventory.OnItemRemoved += HandleItemRemoved;
            _haveListeners = true;

            ReRenderAllItems();
        }
    }
    public void SetInvetory(IInventoryManager inventoryManager)
    {
        OnDisable();
        _inventory = inventoryManager ?? throw new ArgumentNullException(nameof(inventoryManager));
        OnEnable();
        
    }
    public void SetViewItems() 
    {
        for (int i = 0; i < _allView.Length; i++)
        {
            _allView[i].OnSelectItemView = HandleSelectItem;
            _allView[i].OnUseItem = HandleUseItem;
            _allView[i].OnDropItem = HandleDropItem;
            _allView[i].ClearView();

        }
    }
    public void ClearViewItems()
    {
        for (int i = 0; i < _allView.Length; i++)
        {
            _allView[i].OnSelectItemView = null;
            _allView[i].OnUseItem = null;
            _allView[i].OnDropItem = null;
            _allView[i].ClearView();

        }
    }
    public void ReRenderAllItems()
    {
        // Clear all items
        foreach (ItemView View in _items.Values)
        {
            View.ClearView();
        }
        _items.Clear();

        // Add all items
        foreach (var item in _inventory.AllItems)
        {
           HandleItemAdded(item);
        }
    }
    private void HandleItemAdded(IInventoryItem item)
    {
        ItemView view = TakeView();
        Debug.Log(item.Name +" - "+ item.IdItem);
        view.SetView(item);
        _items.Add(item, view);
    }
    private void HandleItemRemoved(IInventoryItem item)
    {
        if (_items.ContainsKey(item))
        {
            ItemView view = _items[item];
            _items.Remove(item);
            view.ClearView();
        }
    }
    public void HandleSelectItem(ItemView view) 
    {
        IInventoryItem key = _items.FirstOrDefault(x => x.Value == view).Key;
        _selectedView = view;
        OnSelectItem?.Invoke(key);
        _inventory.SelectItem(key);
    }
    public void HandleUseItem(ItemView view) 
    {
        Debug.Log("use");
        _inventory.UseItem(SelectItemToView(view)); 
    }
    public void HandleDropItem(ItemView view)
    {
        _inventory.TryRemove(SelectItemToView(view));
    }
    public ItemView TakeView() 
    {
        for (int i = 0; i < _allView.Length; i++)
        {
            if (_allView[i].IsClear)
            {
                return _allView[i];
            }
        }
        Debug.LogError("Нету свободных префабов ячеек");
        return null;
    }

    private IInventoryItem SelectItemToView(ItemView view) 
    {
        IInventoryItem key = _items.FirstOrDefault(x => x.Value == view).Key;
        return key;
    }
}
