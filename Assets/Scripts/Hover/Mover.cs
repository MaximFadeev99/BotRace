using UnityEngine;

[System.Serializable]
public class Mover
{
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _xRotation;
    [SerializeField] private float _xRotationLerpRate;

    private Transform _vehicleTransform;
    private float _currentXRotation;
    private float _initialMaxSpeed;

    public float CurrentSpeed { get; private set; } = 0f;

    private void AdjustForwardRotation()
    {
        _currentXRotation = Mathf.Lerp(_currentXRotation, _xRotation, _xRotationLerpRate);
        _vehicleTransform.rotation = Quaternion.Euler(_currentXRotation,
            _vehicleTransform.rotation.eulerAngles.y, _vehicleTransform.rotation.eulerAngles.z);
    }

    public void Initialize(Transform vehicleTransform)
    {
        _vehicleTransform = vehicleTransform;
        _initialMaxSpeed = _maxSpeed;
    }

    public void PushForward()
    {
        if (CurrentSpeed != _maxSpeed)
            CurrentSpeed = Mathf.MoveTowards(CurrentSpeed, _maxSpeed, _acceleration);

        if (_currentXRotation < _xRotation)
            AdjustForwardRotation();

        _vehicleTransform.position += (CurrentSpeed * Time.deltaTime * _vehicleTransform.forward);
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