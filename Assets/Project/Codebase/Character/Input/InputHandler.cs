using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputHandler : MonoBehaviour
{
    #region Events
    /// <summary>
    /// ������� ������� ����� ��������� � ���������� ���������� ���������� 
    /// �������� ��������� ���������� ������, ��� ������ ��������� ������ 
    /// ��� ����� ������ ������ (��������, ������������) ����� ���������� �� ���������� ���������� ���������� � ������ ��������� ������ �� ������
    /// </summary>
 
    [HideInInspector] public UnityEvent<Vector2> OnMoveEvent;
    #endregion
}
