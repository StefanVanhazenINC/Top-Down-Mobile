using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private HealthSystem _healthSystemPlayer;
    [SerializeField] private StatsConfig _playerStatsConfig;
    [SerializeField] private InventoryStorage _playerInventory;
    [SerializeField] private ItemLibrary _itemLibrary;
    //JsonLoader после лоадера загурузить инвентарь
    public override void InstallBindings()
    {
        BindInputSystem();
        BindStatsSystem();
        BindInventaryInJSON();
    }

    public void BindInputSystem() 
    {
        Container.Bind<TopDownInput>().AsSingle();
    }
    private void BindStatsSystem() 
    {
        Container.Bind<HealthSystem>().FromComponentOn(_healthSystemPlayer.gameObject).AsSingle();
        Container.Bind<StatsConfig>().FromScriptableObject(_playerStatsConfig).AsSingle();
    }
    private void BindInventaryInJSON()
    {
        //JsonLoader после лоадера загурузить инвентарь
        Container.BindInterfacesAndSelfTo<InventoryStorage>().FromComponentOn(_healthSystemPlayer.gameObject).AsSingle();
        Container.Bind<ItemLibrary>().FromScriptableObject(_itemLibrary).AsSingle().NonLazy();

    }
}
