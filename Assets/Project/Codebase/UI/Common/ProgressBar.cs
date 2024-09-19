using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image _sliderBar;

    [Inject]
    public void Construct()
    {
        _sliderBar.fillAmount = 1;
    }

    public void SetAmount(float value) 
    {
        _sliderBar.fillAmount = value;
    }
}
