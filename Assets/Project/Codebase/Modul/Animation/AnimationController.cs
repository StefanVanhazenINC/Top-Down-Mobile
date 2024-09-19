using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using Zenject;

public class AnimationController : MonoBehaviour
{
    private const string VelocityString = "Velocity";

    [SerializeField] private Animator _animator;
    private IMovable _movable;

    [Inject]
    public void Construct() 
    {
        _movable = GetComponent<IMovable>();
        _movable.OnVelocityChange.AddListener(SetFloatVelocity);
    }
   
    private void SetFloatVelocity(Vector3 value) 
    {
        _animator.SetFloat(VelocityString, value.magnitude);
    }


}
