using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using TMPro;
using UnityEngine.UI;

public class ItemView : MonoBehaviour, IPointerClickHandler, IPointerExitHandler
{
    [SerializeField] private TMP_Text _titleItem;
    [SerializeField] private TMP_Text _amountItem;
    [SerializeField] private Image _icon;
    [SerializeField] private GameObject _buttonMenu; 
    private bool _isClear = true;

    public bool IsClear { get => _isClear;  }
    public Action<ItemView> OnSelectItemView;

    public void SetView(IInventoryItem item) 
    {
        _icon.sprite = item.Icon;
        gameObject.SetActive(true);
        _amountItem.text = item.ValueInStack.ToString();
        _titleItem.text = item.Name;
        _isClear = false;
    }
    public void ClearView()
    {
        _isClear = true;
        gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)//активация под меню
    {
        OnSelectItemView?.Invoke(this);
        _buttonMenu.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)//выключение под меню
    {
        _buttonMenu.SetActive(false);
        _buttonMenu.SetActive(false);
    }
}
