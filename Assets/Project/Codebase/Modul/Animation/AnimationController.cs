using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private const string VelocityString = "Velocity";

    [SerializeField] private Animator _animator;
    private IMovable _movable;

    public void Construct() 
    {
        _movable = GetComponent<IMovable>();
        _movable.OnVelocityChange.AddListener(SetFloatVelocity);
    }
    private void Start()
    {
        Construct();
    }
    private void SetFloatVelocity(Vector3 value) 
    {
        _animator.SetFloat(VelocityString, value.magnitude);
    }


}
