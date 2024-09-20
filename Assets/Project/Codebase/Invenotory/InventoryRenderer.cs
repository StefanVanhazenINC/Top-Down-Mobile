using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryRenderer : MonoBehaviour
{
    [SerializeField] private Transform _viewParent;
    [SerializeField] private ItemView[] _allView;
    private IInventoryManager _inventory;

    private bool _haveListeners;

    public Action<IInventoryItem> OnSelectItem;
    private Dictionary<IInventoryItem, ItemView> _items = new Dictionary<IInventoryItem, ItemView>();
    void OnDisable()
    {
        if (_inventory != null && _haveListeners)
        {
            _inventory.onRebuilt -= ReRenderAllItems;
            _inventory.onItemAdded -= HandleItemAdded;
            _inventory.onItemRemoved -= HandleItemRemoved;
            _haveListeners = false;
        }
    }
    void OnEnable()
    {
        if (_inventory != null && !_haveListeners)
        {

            _inventory.onRebuilt += ReRenderAllItems;
            _inventory.onItemAdded += HandleItemAdded;
            _inventory.onItemRemoved += HandleItemRemoved;
            _haveListeners = true;

            ReRenderAllItems();
        }
    }
    public void SetInvetory(IInventoryManager inventoryManager)
    {
        OnDisable();
        _inventory = inventoryManager ?? throw new ArgumentNullException(nameof(inventoryManager));
        SetViewItems();
        OnEnable();
        
    }
    public void SetViewItems() 
    {
        Debug.Log("SetView");
        for (int i = 0; i < _allView.Length; i++)
        {
            _allView[i].OnSelectItemView = SelectItem;
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
        foreach (var item in _inventory.allItems)
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
    public void SelectItem(ItemView view) 
    {
        IInventoryItem key = _items.FirstOrDefault(x => x.Value == view).Key;
        Debug.Log(key);
        OnSelectItem?.Invoke(key);
    }
    //находить предмет по ключу визула 
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
}
