using DG.Tweening;
using System;
using UnityEngine;

public class Ball : Obstacle
{
    private Transform _transform;
    private Rigidbody _rigidbody;
    private ConstantForce _constantForce;
    private Vector3 _pinnedDownScale = Vector3.one; 

    public Action TouchedGround;
    private Vector3 _currentScale;
    private float _yVelocity;

    private void Awake()
    {
        _transform = transform;
        _constantForce = GetComponent<ConstantForce>();
        _rigidbody = GetComponent<Rigidbody>();
        _currentScale = _transform.localScale;
    }

    private void OnCollisionEnter(Collision collision)
    {
        TouchedGround?.Invoke();
        _transform.DOScale(_pinnedDownScale, 0.1f);
        //_transform.localScale = _pinnedDownScale;
    }

    private void OnCollisionExit(Collision collision)
    {
        _transform.DOScale(_currentScale, 0.3f);
        //_transform.localScale = new Vector3 (0.8f, 0.8f, 0.8f);
    }

    public void Initiate(float newYVelocity, Vector3 newPinnedDownSclale) 
    {
        _pinnedDownScale = newPinnedDownSclale;
        _yVelocity = newYVelocity;
    }

    public void ActivateGravity() 
    {
        _rigidbody.useGravity = true;
        _constantForce.force = new Vector3(0f, _yVelocity, 0f);
    }
}
