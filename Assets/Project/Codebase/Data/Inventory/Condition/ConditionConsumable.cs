using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ConsumableType
{
    Buff,
    Debuff
}

[CreateAssetMenu(fileName = "Consumable", menuName = "Configs/Inventory/Condition/new Condition Consumable")]
public class ConditionConsumable : ConditionItem
{
    [SerializeField] private int _value;
    [SerializeField] private bool _effectToHeal;
    [SerializeField] private ConsumableType _consumableType;
    [SerializeField] private StatsType _statType;
    public override void UseCondition(ConditionData data)
    {
        
        if (!_effectToHeal)
        {
            Debug.Log((_consumableType == ConsumableType.Buff ? "Повышена характеристика" : "Понижена характеристика") + " " + _statType + " " + _value);
            int value = _consumableType == ConsumableType.Buff ? 1 : -1;
            value *= _value;
            data.PlayerStats.ChangeStat(_statType, value);
        }
        else 
        {
            Debug.Log((_consumableType == ConsumableType.Buff ? "Лечение" : "Отравлдение") + " " + _value);
            if (_consumableType == ConsumableType.Buff)
            {
                data.PlayerHealth.TakeHeal(_value);
            }
            else if (_consumableType == ConsumableType.Debuff) 
            {
                data.PlayerHealth.TakeDamage(_value);
            }
        }
    }
}
