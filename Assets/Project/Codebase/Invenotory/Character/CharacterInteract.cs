using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CharacterInteract : MonoBehaviour
{
    private InventoryStorage _playerInventory;
    [SerializeField] private StatsController _playerStats;
    [SerializeField] private HealthSystem _palyerHealth;

    private bool _ready = false;
    private void OnEnable()
    {
        if (_ready) 
        {
            _playerInventory.Inventory.OnUseItem = UseItem;
        }

    }
    private void OnDisable()
    {
        if (_ready)
        {
            _playerInventory.Inventory.OnUseItem = null;
        }

    }
    [Inject]
    private void Construct(InventoryStorage playerInventory) 
    {
        _playerInventory = playerInventory;
        _ready = true;
        OnEnable();

    }
    public void UseItem(IInventoryItem itemDefinition) 
    {
        ConditionData conditionData = new ConditionData(_playerStats, _palyerHealth);
        itemDefinition.ConditionItem.UseCondition(conditionData);
        _playerInventory.Inventory.TryRemoveToId(itemDefinition);

    }
    public void AddItem(ItemDefinition itemDefinition) 
    {
        _playerInventory.Inventory.TryAdd(itemDefinition);
    } 
}
