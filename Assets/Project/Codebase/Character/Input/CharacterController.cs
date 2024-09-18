using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : InputHandler
{
    private Vector2 _moveInput;

    private TopDownInput _topDownInput;
    private InputAction _movment;

    private void Awake()
    {
        Constructor();
    }
    private void OnEnable()
    {
        EnableInput();
    }
    private void OnDestroy()
    {
        DisableInput();
    }
    private void OnDisable()
    {
        _movment.Disable();
    }
    private void Constructor() 
    {
        _topDownInput = new TopDownInput();
        _movment = _topDownInput.Player.Move;
    }
    private void EnableInput() 
    {
        _movment.Enable();
        _movment.performed += OnMove;
        _movment.started += OnMove;
        _movment.canceled += OnMove;
    }
    private void DisableInput() 
    {
        _movment.Disable();
        _movment.performed -= OnMove;
        _movment.started -= OnMove;
        _movment.canceled -= OnMove;
    }
    private void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = _movment.ReadValue<Vector2>().normalized ;
        OnMoveEvent?.Invoke(_moveInput);

    }
}
