using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private HealthSystem _healthSystemPlayer;
    [SerializeField] private StatsConfig _playerStatsConfig;


    public override void InstallBindings()
    {
        BindInputSystem();
        BindStatsSystem();
    }

    public void BindInputSystem() 
    {
        Container.Bind<TopDownInput>().AsSingle();
    }
    private void BindStatsSystem() 
    {
        Container.BindInterfacesAndSelfTo<HealthSystem>().FromComponentOn(_healthSystemPlayer.gameObject).AsSingle();
        Container.BindInterfacesAndSelfTo<StatsConfig>().FromScriptableObject(_playerStatsConfig).AsSingle();
    }
}
