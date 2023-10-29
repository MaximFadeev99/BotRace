using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;

    private Transform _cameraTransform;
    private float _initialYPosition;
    private float _nextXPosition;
    private float _nextZPosition;
    private float _initialXDistance;
    private float _initialZDistance;
    

    private void Awake()
    {
        _cameraTransform = transform;
        _initialYPosition = _cameraTransform.position.y;
        _nextXPosition = _cameraTransform.position.x;
        _nextZPosition = _cameraTransform.position.z;
        _initialXDistance = Mathf.Abs(Mathf.Abs(_targetTransform.position.x) - Mathf.Abs(_cameraTransform.position.x));
        _initialZDistance = Mathf.Abs(Mathf.Abs(_targetTransform.position.z) - Mathf.Abs(_cameraTransform.position.z));
    }

    private void LateUpdate()
    {
        if (Mathf.Abs(Mathf.Abs(_targetTransform.position.z) - Mathf.Abs(_cameraTransform.position.z)) 
            != _initialZDistance)
        {
            _nextZPosition = _targetTransform.position.z - _initialZDistance;
        }

        if (Mathf.Abs(Mathf.Abs(_targetTransform.position.x) - Mathf.Abs(_cameraTransform.position.x))
            != _initialXDistance)
        {
            _nextXPosition = _targetTransform.position.x - _initialXDistance;
        }

        _cameraTransform.position = new Vector3(_nextXPosition, _initialYPosition, _nextZPosition);
    }
}