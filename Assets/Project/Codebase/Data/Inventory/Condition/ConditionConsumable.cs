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
    [SerializeField] private float _value;
    [SerializeField] private bool _effectToHeal;
    [SerializeField] private ConsumableType _consumableType;
    [SerializeField] private StatsType _statType;
    public override void UseCondition()
    {
        if (!_effectToHeal)
        {
            Debug.Log((_consumableType == ConsumableType.Buff ? "Повышена характеристика" : "Понижена характеристика") + " " + _statType + " " + _value);
        }
        else 
        {
            Debug.Log((_consumableType == ConsumableType.Buff ? "Лечение" : "Отравлдение") + " "  + _value);
        }
    }
}
