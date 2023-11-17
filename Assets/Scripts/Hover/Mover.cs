using UnityEngine;

[System.Serializable]
public class Mover : IMover
{
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _xRotation;
    [SerializeField] private float _xRotationLerpRate;

    private Transform _transform;   
    private float _currentXRotation;
    private float _initialYPosition;
    private float _initialMaxSpeed;

    public float CurrentSpeed { get; private set; } = 0f;

    private void AdjustForwardRotation()
    {
        _currentXRotation = Mathf.Lerp(_currentXRotation, _xRotation, _xRotationLerpRate);
        _transform.rotation = Quaternion.Euler(_currentXRotation,
            _transform.rotation.eulerAngles.y, _transform.rotation.eulerAngles.z);
    }

    public void Initialize(Transform vehicleTransform)
    {
        _transform = vehicleTransform;
        _initialYPosition = _transform.position.y;
        _initialMaxSpeed = _maxSpeed;
    }

    public void PushForward()
    {
        if (CurrentSpeed != _maxSpeed)
            CurrentSpeed = Mathf.MoveTowards(CurrentSpeed, _maxSpeed, _acceleration);

        if (_currentXRotation < _xRotation)
            AdjustForwardRotation();

        _transform.position = new Vector3(_transform.position.x, _initialYPosition, _transform.position.z) 
            + (CurrentSpeed * Time.deltaTime * _transform.forward);
    }    

    public void ChangeMaxSpeed(float changeRate) 
    {
        _maxSpeed *= changeRate;
        CurrentSpeed = _maxSpeed;
    }

    public void ResetMaxSpeed() =>
        _maxSpeed = _initialMaxSpeed;

    public void DecreaseCurrentSpeed(float reductionRate) =>
        CurrentSpeed *= reductionRate;
}