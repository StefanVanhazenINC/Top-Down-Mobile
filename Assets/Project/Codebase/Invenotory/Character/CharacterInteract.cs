using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteract : MonoBehaviour
{
    [SerializeField] private InventoryStorage _store;

    public void AddItem(ItemDefinition itemDefinition) 
    {
        _store.Inventory.TryAdd(itemDefinition);
    } 
}
