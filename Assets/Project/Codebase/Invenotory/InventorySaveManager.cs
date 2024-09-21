using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InventorySaveManager : MonoBehaviour
{
    [SerializeField] private InventoryStorage _playerInventoryStorage;
    private ItemLibrary _itemLibrary;
    [Inject]
    private void Construct(ItemLibrary itemLibrary, InventoryStorage playerInventoryStorage) 
    {
        _itemLibrary = itemLibrary;
        _playerInventoryStorage = playerInventoryStorage;
        LoadSaveData();
    }
    public void LoadSaveData() 
    {
       
        _playerInventoryStorage.BaseItem = _itemLibrary.LoadInventory() ;
        _playerInventoryStorage.SetDefaultItem();
    }


    private void OnApplicationQuit()
    {
        _itemLibrary.SaveData(_playerInventoryStorage.Inventory);
    }
}
