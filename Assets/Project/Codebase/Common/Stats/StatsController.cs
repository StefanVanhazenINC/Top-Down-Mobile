using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StatsController : MonoBehaviour
{
    [SerializeField] private StatsConfig _statsConfig;

    [Inject]
    public void Consruct(StatsConfig statsConfig) 
    {
        _statsConfig = statsConfig;
    }
}
