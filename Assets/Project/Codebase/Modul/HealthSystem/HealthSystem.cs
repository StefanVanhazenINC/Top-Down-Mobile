using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class HealthSystem : MonoBehaviour, IDamagable
{
    [SerializeField] private int _maxHealth;

    private int _health;

    [HideInInspector]public UnityEvent<int> OnChangeHeath;
    [HideInInspector] public UnityEvent<int> OnChangeMaxHeath;
    public int Health 
    {
        get => _health;
        set 
        {
            _health = value;
            OnChangeHeath?.Invoke(_health);
        }
    }
    public int MaxHealth 
    { 
        get => _maxHealth;
        set 
        {
            _maxHealth = value;
            OnChangeMaxHeath?.Invoke(_maxHealth);
        }
    }

    [Inject]
    public void Construct() 
    {
        SettingHealth();
    }
    private void SettingHealth() 
    {
        Health = _maxHealth;
    }
    public void Death()
    {
        gameObject.SetActive(false);
    }
    public void TakeDamage(int value)
    {
        if (value>0 && Health>0)
        {
            Health -= value;
        }
        if (Health <= 0) 
        {
            Death();
        }
    }

    [ContextMenu("Test")]
    public void Test ()
    {
        TakeDamage(10);
    }
}
