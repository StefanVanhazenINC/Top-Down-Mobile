using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Movment : MonoBehaviour, IMovable
{
    [SerializeField] private float _moveSpeed;
    private InputHandler _input;

    private Rigidbody _rb;
    private Vector2 _currentVelocity;
    private Vector2 _smoothMoveInput;
    private Vector2 _refSmoothMoveInput;

    public UnityEvent<Vector3> OnVelocityChange { get ; set ; }

    public Vector3 Velocity 
    {
        get 
        {
            return _rb.velocity;
        }
        set 
        {
            _rb.velocity = value;
            OnVelocityChange?.Invoke(_rb.velocity);
        }
    
    }

    /// <summary>
    /// модуль движение сам подписывется на управление и нужную ему команду 
    /// </summary>
    private void Construct()
    {
        OnVelocityChange = new UnityEvent<Vector3>();
        _rb = GetComponent<Rigidbody>();
        _input = GetComponent<InputHandler>();
    }

    private void Awake()
    {
        Construct();
    }
    private void OnEnable()
    {
        _input.OnMoveEvent.AddListener(SetValueInput);
    }
    private void OnDisable()
    {
        _input.OnMoveEvent.RemoveListener(SetValueInput);
    }
    private void FixedUpdate()
    {
        Move();
    }
    public  void Move() 
    {
        _smoothMoveInput = Vector2.SmoothDamp(_smoothMoveInput, _currentVelocity, ref _refSmoothMoveInput, 0.1f);
        Velocity = (transform.right * _smoothMoveInput.x + transform.forward * _smoothMoveInput.y) * _moveSpeed;
    }
    public void SetValueInput(Vector2 inputValue)
    {
        _currentVelocity = inputValue  ;
    }

}
