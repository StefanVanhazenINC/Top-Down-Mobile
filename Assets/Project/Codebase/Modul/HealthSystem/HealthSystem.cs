using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour, IDamagable
{
    [SerializeField] private int _maxHealth;

    public UnityEvent<int> OnChangeHeath;
    private int _health;
    private int Health 
    {
        get => _health;
        set 
        {
            _health = value;
            OnChangeHeath?.Invoke(value);
        }
    }

    private void Awake()
    {
        Construct();
    }
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
}
