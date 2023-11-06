using UnityEngine;

[System.Serializable]
public class DirectionChanger : IDirectionChanger
{
    [SerializeField] private float _yRotationSpeed;
    [SerializeField] private float _zRotationSpeed;
    [SerializeField] private float _maxZRotation;

    private Transform _transform;
    private float _currentZRotation = 0;
    private float _previousYRotation = 0;

    public void Initialize(Transform veichleTransform)
    {
        _transform = veichleTransform; 
        _currentZRotation = _transform.rotation.eulerAngles.z;
    }

    public void ChangeDirection(float newYRotation) 
    {
        SetYRotation(newYRotation);
        AdjustZRotation();
    }

    public void ResetZRotation()
    {
        _currentZRotation = Mathf.Lerp(_currentZRotation, 0, _zRotationSpeed * Time.deltaTime);
        _transform.rotation = Quaternion.Euler(_transform.rotation.eulerAngles.x,
            _transform.rotation.eulerAngles.y, _currentZRotation);
    }

    private void SetYRotation(float newYRotation) 
    {
        _previousYRotation = _transform.rotation.eulerAngles.y;
        _transform.rotation = Quaternion.Euler(_transform.rotation.eulerAngles.x,
            _transform.rotation.eulerAngles.y + newYRotation * _yRotationSpeed * Time.deltaTime, _transform.rotation.eulerAngles.z);

        _transform.rotation = Quaternion.Euler(_transform.rotation.eulerAngles.x,
           _transform.rotation.eulerAngles.y + newYRotation * Time.deltaTime, _transform.rotation.eulerAngles.z);
    }

    private void AdjustZRotation() 
    {
        if (_transform.rotation.eulerAngles.y - _previousYRotation < 0)
            _currentZRotation = Mathf.Lerp(_currentZRotation, _maxZRotation, _zRotationSpeed * Time.deltaTime);
        else
            _currentZRotation = Mathf.Lerp(_currentZRotation, -_maxZRotation, _zRotationSpeed * Time.deltaTime);

        _transform.rotation = Quaternion.Euler(_transform.rotation.eulerAngles.x,
                _transform.rotation.eulerAngles.y, _currentZRotation);
    }
}
