using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryItem
{
    public string Name  { get; set; }
    public Sprite Icon { get; }
    public string IdItem { get; }
    public int ValueInStack { get; set; }
    public int MaxValueInStack { get; set; }
    public ConditionItem ConditionItem { get;  }
    public abstract bool StackIsFull();


    public abstract void AddedToStack();

    public abstract bool TryAddToStack();

    public abstract bool TryRemoveStack();
    public abstract void RemoveStack();

    public abstract ItemDefinition CreateInstance();

    public abstract ItemDefinition CreateInstance(int valueStack);
   
}
