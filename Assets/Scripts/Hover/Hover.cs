using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Hover : MonoBehaviour
{
    private Engine _engine;
    private Transform _transform;
    private IInputHandler _inputHandler;
    private CollisionHandler _collisionHandler;

    private float _correctionAngle;
    private Transform _currentTrack;

    //класс получает сигнал от RaceManager и говорит двигателю двигать машину вперед

    private void Awake()
    {
        _transform = transform;
        _engine = GetComponent<Engine>();
        _inputHandler = GetComponent<IInputHandler>();
        _collisionHandler = new(_transform, _inputHandler);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.TryGetComponent(out RightTrackBarrier rightTrackBarrier))
        //{
        //    _inputHandler.BlockRightTurn();
        //    _correctionAngle = CalculateCorrectionAngle();
        //    _transform.DORotate(new Vector3(_transform.rotation.eulerAngles.x, _transform.rotation.eulerAngles.y + _correctionAngle,
        //    _transform.rotation.eulerAngles.z), 0.2f);
        //}

        //if (other.TryGetComponent(out LeftTrackBarrier leftTrackBarrier))
        //{
        //    _inputHandler.BlockLeftTurn();
        //    _correctionAngle = CalculateCorrectionAngle();
        //}

        _collisionHandler.AnalyzeCollision(other);
    }


    private void OnTriggerExit(Collider other)
    {
        //_inputHandler.RemoveTurnBlocks();
        _collisionHandler.OnCollisionEnded();
    }

    //private float CalculateCorrectionAngle() 
    //{
    //    float raycastDistance = 10f;
    //    Physics.Raycast(_transform.position, Vector3.down, out RaycastHit track, raycastDistance);
    //    _currentTrack = track.transform;
    //    return -Vector3.Angle(_transform.forward, _currentTrack.forward);
    //}
}
