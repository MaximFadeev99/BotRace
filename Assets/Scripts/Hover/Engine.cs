using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(IInputHandler))]
[RequireComponent(typeof(SpeedBooster))]
public class Engine : MonoBehaviour
{
    [SerializeField] private Levitator _levitator;
    [SerializeField] private Mover _mover;
    [SerializeField] private DirectionChanger _directionChanger;
    [SerializeField] private vehicleShaker _vehicleShaker;
    [SerializeField] private AudioSource _audioSource;   
    [SerializeField] private float _speedLoseRate;

    private Transform _vehicleTransform;
    private SpeedBooster _speedBooster;
    private IInputHandler _inputHandler;
    private float _currentDirectionInput;
    private bool _isMovingForward = false;

    public Action SpeedNullified;

    private void Awake()
    {
        _vehicleTransform = transform;
        _levitator.Initialize(_vehicleTransform);
        _mover.Initialize(_vehicleTransform);
        _directionChanger.Initialize(_vehicleTransform);
        _vehicleShaker.Intialize(_vehicleTransform);
        _inputHandler = GetComponent<IInputHandler>();
        _speedBooster = GetComponent<SpeedBooster>();
        _speedBooster.Intialize(_mover);
    }

    private void Start() =>
        _levitator.StartLevitation();

    private void Update()
    {
        if (_isMovingForward) 
        {
            _currentDirectionInput = _inputHandler.InquireInput();

            if (_currentDirectionInput != 0f) 
                _directionChanger.ChangeDirection(_currentDirectionInput);
            else
                _directionChanger.ResetZRotation();

            _mover.PushForward();
        }
    }

    public void StartMovement() 
    {
        _levitator.StopLevitation();
        _isMovingForward = true;

        if (_audioSource != null)
            _audioSource.Play();
    }

    public void ApplySpeedPenalty() 
    {
        _mover.DecreaseCurrentSpeed(_speedLoseRate);
        StartCoroutine(_vehicleShaker.Shake());
    }

    public void ApplySpeedBonus() =>
        _speedBooster.ActivateSpeedBoost();

    public IEnumerator GraduallyDecreaseSpeed() 
    {
        var waitTime = new WaitForSecondsRealtime(0.1f);
        yield return waitTime;

        while (_mover.CurrentSpeed > 2f) 
        {
            _mover.ChangeMaxSpeed(0.9f);
            yield return waitTime;
        }

        _mover.DecreaseCurrentSpeed(0f);
        _isMovingForward = false;
        SpeedNullified?.Invoke();
    }   
}