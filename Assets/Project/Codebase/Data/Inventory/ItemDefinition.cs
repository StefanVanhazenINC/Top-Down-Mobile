using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item",menuName = "Configs/Inventory/new Item")]
public class ItemDefinition : ScriptableObject, IInventoryItem
{
    [SerializeField] private string _idItem;
    [SerializeField] private Sprite _icon ;
    [SerializeField] private ItemTypes _itemType;
    [SerializeField] private int _maxValueInStack = 1;
    [SerializeField] private GameObject _prefabItem;
    [SerializeField] private ConditionItem _conditionItem;
    private int _valueInStack = 1;

    public string Name { get => name; set => name = value; }
    public Sprite Icon { get => _icon; }
    public string IdItem { get => _idItem; }
    public int ValueInStack { get => _valueInStack; set => _valueInStack = value; }
    public ItemTypes ItemType { get => _itemType; }
    public int MaxValueInStack { get => _maxValueInStack; set => _maxValueInStack= value; }
    public ConditionItem ConditionItem { get => _conditionItem; }
    public GameObject PrefabItem { get => _prefabItem; }

    public bool StackIsFull() 
    {
        if (_valueInStack >= _maxValueInStack)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }
    public void AddedToStack() 
    {
        _valueInStack += 1;
    }
    public bool TryAddToStack() 
    {
        if (!StackIsFull())
        {
            AddedToStack();
            return true;
        }
        else 
        {
            return false;
        }
    }

    public bool TryRemoveStack()
    {
        if (_valueInStack > 1)
        {
            RemoveStack();
            return true;
        }
        else
        {
            return false;
        }
    }
    public void RemoveStack()
    {
        _valueInStack -= 1;
    }
    public ItemDefinition CreateInstance()
    {
        ItemDefinition clone = ScriptableObject.Instantiate(this);
        clone.Name = this.name;
        clone.ValueInStack = this.ValueInStack;
        return clone;
    }

    public ItemDefinition CreateInstance(int valueStack)
    {

        ItemDefinition clone = ScriptableObject.Instantiate(this);
        clone.Name = this.name;
        clone._valueInStack = valueStack;
        return clone;
    }

   
}
