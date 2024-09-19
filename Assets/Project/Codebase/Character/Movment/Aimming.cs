using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Aimming : MonoBehaviour
{
    [SerializeField] private Transform _bodyRotation;
    [SerializeField] private float _rotationSpeed = 10;

    private InputHandler _input;
    private Vector2 _aimPosition;
    private float  _thresholdAimValue = 0.5f;

    [Inject]
    private void Construct() 
    {
        _input = GetComponent<InputHandler>();
    }

    private void OnEnable()
    {
        _input.OnMoveEvent.AddListener(Aim);
    }
    private void OnDisable()
    {
        _input.OnMoveEvent.RemoveListener(Aim);
    }
    private void Update()
    {
        if (_aimPosition.magnitude > _thresholdAimValue)
        {
            ProccesAim(_aimPosition);
        }
    }

    private void Aim(Vector2 value) 
    {
        _aimPosition = value;
       
    }

    private void ProccesAim(Vector2 value) 
    {

        Vector3 directioMouseLook = new Vector3(_bodyRotation.transform.localPosition.x - value.x, 0, _bodyRotation.transform.localPosition.z - value.y);

        float angle = Mathf.Atan2(directioMouseLook.x, directioMouseLook.z) * Mathf.Rad2Deg;
        angle += 180;
        Quaternion lerpAngle = Quaternion.Euler(0, angle, 0);
        _bodyRotation.localRotation = lerpAngle;
    }
}
