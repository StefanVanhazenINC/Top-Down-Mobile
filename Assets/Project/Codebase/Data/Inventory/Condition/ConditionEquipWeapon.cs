using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Equip Weapon", menuName = "Configs/Inventory/Condition/new Condition Equip")]
public class ConditionEquipWeapon : ConditionItem
{
    [SerializeField] private GameObject _weapon;
    public override void UseCondition(ConditionData data)
    {
        Debug.Log("Экиперовать предмет" + _weapon);
    }
}
