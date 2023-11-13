using DG.Tweening;
using System;
using UnityEngine;

[RequireComponent(typeof(ConstantForce))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class Ball : Obstacle
{
    private readonly float _shrinkTime = 0.1f;
    private readonly float _bounceBackTime = 0.3f;
    
    private Transform _transform;
    private Rigidbody _rigidbody;
    private ConstantForce _constantForce;
    private AudioSource _audioSource;
    private Vector3 _pinnedDownScale = Vector3.one; 
    private Vector3 _currentScale;
    private float _yVelocity;

    public Action TouchedGround;

    private void Awake()
    {
        _transform = transform;
        _constantForce = GetComponent<ConstantForce>();
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        _currentScale = _transform.localScale;
    }

    private void OnCollisionEnter()
    {
        TouchedGround?.Invoke();
        _transform.DOScale(_pinnedDownScale, _shrinkTime);
        _audioSource.Play();
    }

    private void OnCollisionExit() => 
        _transform.DOScale(_currentScale, _bounceBackTime);

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