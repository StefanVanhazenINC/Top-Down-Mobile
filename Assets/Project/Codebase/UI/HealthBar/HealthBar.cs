using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem _healthSystem;
    [SerializeField] private ProgressBar _progressBar;

    private int _maxValueHealth;
    private int _valueHealth;

    [Inject]
    public void Construct(HealthSystem healthSystem) 
    {
        _progressBar = GetComponent<ProgressBar>();
        _healthSystem = healthSystem;
        _valueHealth = _healthSystem.Health;
        _maxValueHealth = _healthSystem.MaxHealth;
    }
   
    private void OnEnable()
    {
        _healthSystem.OnChangeHeath.AddListener(ChangeHealthBar);
        _healthSystem.OnChangeMaxHeath.AddListener(ChangeMaxValue);
    }
    private void OnDisable()
    {
        _healthSystem.OnChangeHeath.RemoveListener(ChangeHealthBar);
        _healthSystem.OnChangeMaxHeath.RemoveListener(ChangeMaxValue);
    }

    private void ChangeMaxValue(int value) 
    {
        _maxValueHealth = value;
        ChangeHealthBar(_valueHealth);
    }
    private void ChangeHealthBar(int value) 
    {
        _valueHealth = value;
        float t_precent = (float)_valueHealth / (float)_maxValueHealth;
        _progressBar.SetAmount(t_precent);
    }
}
