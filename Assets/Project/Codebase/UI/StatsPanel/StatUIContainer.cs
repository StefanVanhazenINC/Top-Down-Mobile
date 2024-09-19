using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatUIContainer : MonoBehaviour
{
    [SerializeField] private TMP_Text _value;
    [SerializeField] private TMP_Text _nameStats;

    public void SetContainer(Stat stats) 
    {
        _nameStats.text = stats.Type.ToString();
        _value.text = stats.Value.ToString();
    }
    public void SetValue(int value)
    {
        _value.text = value.ToString();
    }
}
