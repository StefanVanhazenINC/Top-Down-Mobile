using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    [SerializeField] private ItemDefinition _itemDefinition;


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent( out CharacterInteract characterInteract)) 
        {
            characterInteract.AddItem(_itemDefinition);
            Destroy(gameObject);
        }
    }
}
