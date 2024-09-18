using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IMovable
{
    public UnityEvent<Vector3> OnVelocityChange { get; set; }
    public void Move();
    public void SetValueInput(Vector2 inputValue);


}
