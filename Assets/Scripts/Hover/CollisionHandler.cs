using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollisionHandler
{
    private Transform _veichleTransform;
    private IInputHandler _inputHandler;

    public CollisionHandler (Transform veichleTransform, IInputHandler inputHandler) 
    {
        _veichleTransform = veichleTransform;
        _inputHandler = inputHandler;
    }

    public void AnalyzeCollision(Collider other) 
    {
        if (other.TryGetComponent(out RightTrackBarrier _))  
            //поскольку сделал через SignedAngle деление на левый и правый барьер нужно убрать 
        {
            _inputHandler.BlockRightTurn();
            CorrectTrajectory(true);
            return;
        }

        if (other.TryGetComponent(out LeftTrackBarrier _))
        {
            _inputHandler.BlockLeftTurn();
            CorrectTrajectory(false);
            return;
        }
    }

    public void OnCollisionEnded()
    {
        _inputHandler.RemoveTurnBlocks();
    }

    private void CorrectTrajectory(bool isCollidingOnRight)
    {
        float raycastDistance = 5f;
        float correctionDuration = 0.2f;
        float correctionAngle;

        Physics.Raycast(_veichleTransform.position, Vector3.down, out RaycastHit track, raycastDistance);
        //correctionAngle = Vector3.Angle(_veichleTransform.forward, track.transform.forward);
        ////попробовать высчитать correctionAngle через Vector3.SignedAngle
        //correctionAngle = isCollisionOnRight ? -correctionAngle : correctionAngle;
        correctionAngle = CalculateCorrectionAngle(track, isCollidingOnRight);
        _veichleTransform.DORotate(new Vector3(_veichleTransform.rotation.eulerAngles.x, 
            _veichleTransform.rotation.eulerAngles.y + correctionAngle,
            _veichleTransform.rotation.eulerAngles.z), correctionDuration);
    }

    private float CalculateCorrectionAngle(RaycastHit track, bool isCollidingOnRight) 
    {
        float correctionAngle = 0;
        Vector3 rotationAxis =  Vector3.zero;
        Transform trackTransform = track.transform;

        if (track.collider.TryGetComponent(out Straight _))
        {
            correctionAngle = Vector3.SignedAngle(_veichleTransform.forward, trackTransform.forward, Vector3.up);
        }
        else if (track.collider.TryGetComponent(out LeftTurn _))
        {
            float slerpPorportion = 0.5f;

            if (isCollidingOnRight && track.point.z < trackTransform.position.z)
            {
                rotationAxis = Vector3.Slerp(-trackTransform.right, trackTransform.forward, slerpPorportion);
            }
            else if (isCollidingOnRight && track.point.z >= trackTransform.position.z)
            {
                rotationAxis = -trackTransform.right;
            }
            else if (isCollidingOnRight == false && track.point.z < trackTransform.position.z)
            {
                rotationAxis = trackTransform.forward;
            }
            else if (isCollidingOnRight == false && track.point.z >= trackTransform.position.z)
            {
                rotationAxis = Vector3.Slerp(-trackTransform.right, trackTransform.forward, slerpPorportion);
            }

            correctionAngle = Vector3.SignedAngle(_veichleTransform.forward, rotationAxis, Vector3.up);
            //Debug.Log(correctionAngle);
        }
        else if (track.collider.TryGetComponent(out RightTurn _))
        {
            float slerpPorportion1 = 0.6f;
            float slerpPorportion2 = 0.45f;

            if (isCollidingOnRight && track.point.z < trackTransform.position.z)
            {
                rotationAxis = trackTransform.right;
            }
            else if (isCollidingOnRight && track.point.z >= trackTransform.position.z)
            {
                rotationAxis = Vector3.Slerp(trackTransform.right, -trackTransform.forward, slerpPorportion2);
            }
            else if (isCollidingOnRight == false && track.point.z < trackTransform.position.z)
            {
                rotationAxis = Vector3.Slerp(trackTransform.right, -trackTransform.forward, slerpPorportion1);
            }
            else if (isCollidingOnRight == false && track.point.z >= trackTransform.position.z)
            {
                rotationAxis = -trackTransform.forward;
            }

            correctionAngle = Vector3.SignedAngle(_veichleTransform.forward, rotationAxis, Vector3.up);
        }

        return correctionAngle;
    }
}
