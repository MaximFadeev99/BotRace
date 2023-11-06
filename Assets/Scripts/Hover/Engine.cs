using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Engine : MonoBehaviour
{
    [SerializeField] private Levitator _levitator;
    [SerializeField] private Mover _mover;
    [SerializeField] private DirectionChanger _directionChanger;
    [SerializeField] private bool _isMovingForward = false;
    [SerializeField] private float _speedBoostRate;
    [SerializeField] private float _speedBoostTime;
    [SerializeField] private float _speedLoseRate;

    private Transform _veichleTransform;
    private List<Timer> _timers = new(); //если будет только один таймер, убрать список
    private Timer _speedBoostTimer = new();
    //private Timer _invincibilityTimer = new();
    private IInputHandler _inputHandler;
    private float _currentDirectionInput;
    private float _previousYRotation;
    private bool _isSpeedBoostActive;

    private Vector3 _shakeAngles = new Vector3();
    [SerializeField] private AnimationCurve _amplitudeCurve;
    private float _shakeTimer = 1f;
    [SerializeField] private float _duration;
    [SerializeField] private float _amplitudeRange;

    private void Awake()
    {
        _veichleTransform = transform;
        _levitator.Initialize(_veichleTransform);
        _mover.Initialize(_veichleTransform);
        _directionChanger.Initialize(_veichleTransform);
        _inputHandler = GetComponent<IInputHandler>();
        _previousYRotation = _veichleTransform.rotation.eulerAngles.y;
        _timers.Add(_speedBoostTimer);
        //_timers.Add(_invincibilityTimer);
    }

    private void Update()
    {
        foreach (Timer timer in _timers) 
        {
            if (timer.IsActive)
                timer.Tick();
        }
        
        _currentDirectionInput = _inputHandler.InquireInput();

        if (_currentDirectionInput != 0)
            _directionChanger.ChangeDirection(_currentDirectionInput);

        if (_isMovingForward) 
        {
            _levitator.StopLevitation(); //убрать !!!
            _mover.PushForward();        
        }

        if (_previousYRotation == _veichleTransform.rotation.eulerAngles.y)
            _directionChanger.ResetZRotation();
    }

    private void LateUpdate()
    {
        _previousYRotation = _veichleTransform.rotation.eulerAngles.y;
    }

    public void StartMovement() 
    {
        _levitator.StopLevitation();
        _isMovingForward = true;
    }

    public void ResetDirection(float correctionAngle) 
    {
        _directionChanger.ChangeDirection(correctionAngle);

        //_veichleTransform.DORotate(new Vector3(_veichleTransform.rotation.eulerAngles.x, _veichleTransform.rotation.eulerAngles.y + correctionAngle,
        //    _veichleTransform.rotation.eulerAngles.z), 0.2f);
    }

    public void ActivateSpeedBoost() 
    {
        if (_isSpeedBoostActive) 
        { 
            _speedBoostTimer.Start(_speedBoostTime);
            return;
        }

        _isSpeedBoostActive = true;
        _speedBoostTimer.Start(_speedBoostTime);
        _speedBoostTimer.TimeIsUp += DeactivateSpeedBoost;
        _mover.IncreaseMaxSpeed(_speedBoostRate);
    }

    private void DeactivateSpeedBoost() 
    {
        _isSpeedBoostActive = false;
        _mover.ResetMaxSpeed();
        _speedBoostTimer.TimeIsUp -= DeactivateSpeedBoost;
    }

    public void ApplySpeedPenalty() 
    {
        _mover.DecreaseCurrentSpeed(_speedLoseRate);
        //StartCoroutine(ShowHitEffect());
    }

    private IEnumerator ShowHitEffect() 
    {
        _shakeTimer = 1f;
        
        while (_shakeTimer > 0f)
        {
            _shakeTimer -= Time.deltaTime / _duration;
            float currentOffset = Random.Range(-_amplitudeRange, _amplitudeRange);
            _shakeAngles.x += currentOffset;
            _shakeAngles.y += currentOffset;
            _shakeAngles *= _amplitudeCurve.Evaluate(Mathf.Clamp01(1 - _shakeTimer));
            _veichleTransform.position += _shakeAngles;
            yield return null;
        }
    }
}
