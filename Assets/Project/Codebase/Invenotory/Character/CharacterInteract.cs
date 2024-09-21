using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteract : MonoBehaviour
{
    [SerializeField] private InventoryStorage _playerInventory;
    [SerializeField] private StatsController _playerStats;
    [SerializeField] private HealthSystem _palyerHealth;
    private void OnEnable()
    {
        _playerInventory.Inventory.OnUseItem = UseItem;

    }
    private void OnDisable()
    {
        _playerInventory.Inventory.OnUseItem =null;

    }
   
    public void UseItem(IInventoryItem itemDefinition) 
    {
        ConditionData conditionData = new ConditionData(_playerStats, _palyerHealth);
        itemDefinition.ConditionItem.UseCondition(conditionData);
        _playerInventory.Inventory.TryRemove(itemDefinition);

    }
    public void AddItem(ItemDefinition itemDefinition) 
    {
        _playerInventory.Inventory.TryAdd(itemDefinition);
    } 
}
