using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Engine : MonoBehaviour
{
    [SerializeField] private TransformLevitator _levitator;
    [SerializeField] private TransformMover _mover;
    [SerializeField] private TransformDirectionChanger _directionChanger;
    [SerializeField] private bool _isMovingForward;

    private Transform _veichleTransform;
    private IInputHandler _inputHandler;
    private float _currentDirectionInput;
    private float _previousYRotation;

    private void Awake()
    {
        _veichleTransform = transform;
        _levitator.Initialize(_veichleTransform);
        _mover.Initialize(_veichleTransform);
        _directionChanger.Initialize(_veichleTransform);
        _inputHandler = GetComponent<IInputHandler>();
        _previousYRotation = _veichleTransform.rotation.eulerAngles.y;
    }

    private void Update()
    {
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
}
