using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ConditionData 
{
    private StatsController _playerStats;
    private HealthSystem _palyerHealth;
    public StatsController PlayerStats { get => _playerStats;  }
    public HealthSystem PlayerHealth { get => _palyerHealth;  }
    public ConditionData(StatsController playerStats, HealthSystem palyerHealth)
    {
        _playerStats = playerStats;
        _palyerHealth = palyerHealth;
    }

    
}
