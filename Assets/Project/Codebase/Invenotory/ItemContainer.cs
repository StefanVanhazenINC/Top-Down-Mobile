using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    [SerializeField] private ItemDefinition _itemDefinition;

    private void Start()
    {
        SetVisual();
    }

    private void SetVisual() 
    {
        if (_itemDefinition) 
        {
            Instantiate(_itemDefinition.PrefabItem, transform) ;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent( out CharacterInteract characterInteract)) 
        {
            characterInteract.AddItem(_itemDefinition);
            Destroy(gameObject);
        }
    }
}
