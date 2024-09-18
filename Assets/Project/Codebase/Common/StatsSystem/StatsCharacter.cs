using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum StatsType
{
    Strenght,
    Dexterity,
    Wisdom
}

[System.Serializable]
public class Stat 
{
    [SerializeField] private int _value;
    [SerializeField] private StatsType _type;

    public UnityEvent<int> OnStateChange;

    public int Value 
    {
        get => _value;
        set 
        {
            _value = value;
            OnStateChange?.Invoke(value);
        }
    }

    public StatsType Type { get => _type; }
}

public class StatsCharacter : MonoBehaviour
{
    [SerializeField] private Stat[] _stats;

    public Stat[] Stats { get => _stats;  }

    public void ChangeStats(Stat statChange,int value) 
    {
        Stat t_stat = FindStat(statChange);
        t_stat.Value = value;
    }
    public Stat FindStat(Stat statChange) 
    {
        Stat t_stat = null;
        for (int i = 0; i < Stats.Length; i++)
        {
            if (Stats[i].Type == statChange.Type) 
            {
                t_stat = Stats[i];
                break;                 
            }
        }
        return t_stat;
    }
}
