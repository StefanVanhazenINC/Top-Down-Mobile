using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class StatsPanel : MonoBehaviour
{
    [SerializeField] private StatsConfig _statsConfig;
    [SerializeField] private StatUIContainer _prefabStatContainer;
    [SerializeField] private Transform _parentTransform;
    private StatUIContainer[] _statsContainer;

    [Inject]
    public void Consruct(StatsConfig statsConfig)
    {
        _statsConfig = statsConfig;
        SetStatsContainer();
    }
    public void SetStatsContainer() 
    {
        int valueStats = _statsConfig.ValueStats;
        _statsContainer = new StatUIContainer[valueStats];
        for (int i = 0; i < _statsContainer.Length; i++) 
        {
            StatUIContainer t_container = Instantiate(_prefabStatContainer, _parentTransform);
            t_container.SetContainer(_statsConfig.Stats[i]);
            _statsContainer[i] = t_container;
        }
    }
    private void OnEnable()
    {
        for (int i = 0; i < _statsContainer.Length; i++)
        {
            _statsConfig.Stats[i].OnStatChange.AddListener(_statsContainer[i].SetValue);
        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < _statsContainer.Length; i++)
        {
            _statsConfig.Stats[i].OnStatChange.RemoveListener(_statsContainer[i].SetValue);
        }
    }
}
