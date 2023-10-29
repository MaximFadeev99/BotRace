using DG.Tweening;
using UnityEngine;

[System.Serializable]
public class TransformMover : IMover
{
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _xRotation;
    [SerializeField] private float _xRotationLerpRate;

    private Transform _transform;
    private Vector3 _correctedPosition;
    private float _currentSpeed = 0;
    private float _currentXRotation;
    private float _initialYPosition;

    public void Initialize(Transform veichleTransform)
    {
        _transform = veichleTransform;
        _initialYPosition = _transform.position.y;
    }

    public void PushForward()
    {
        if (_currentSpeed < _maxSpeed)
            _currentSpeed = Mathf.MoveTowards(_currentSpeed, _maxSpeed, _acceleration);

        if (_currentXRotation < _xRotation)
            AdjustForwardRotation();

        _correctedPosition = _transform.position;

        if (_correctedPosition.y < _initialYPosition)
            _correctedPosition = new Vector3(_correctedPosition.x, _initialYPosition, _correctedPosition.z);

        _transform.DOMove(_correctedPosition + (_currentSpeed * Time.deltaTime * _transform.forward), Time.deltaTime);
    }

    private void AdjustForwardRotation()
    {
        _currentXRotation = Mathf.Lerp(_currentXRotation, _xRotation, _xRotationLerpRate);
        _transform.rotation = Quaternion.Euler(_currentXRotation,
            _transform.rotation.eulerAngles.y, _transform.rotation.eulerAngles.z);
    }  
}
