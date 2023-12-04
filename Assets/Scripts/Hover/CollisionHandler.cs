using UnityEngine;
using DG.Tweening;

public class CollisionHandler
{
    private readonly Engine _engine;
    private readonly Transform _vehicleTransform;
    private readonly InvincibilityHandler _invincibilityHandler;
    private readonly IInputHandler _inputHandler;

    public CollisionHandler (Hover hover) 
    {
        _vehicleTransform = hover.Transform;
        _inputHandler = hover.InputHandler;
        _engine = hover.Engine;
        _invincibilityHandler = hover.InvincibilityHandler;
    }
    
    private void CorrectTrajectory(bool isCollidingOnRight)
    {
        float raycastDistance = 10f; //5f
        float correctionDuration = 0.2f;
        float correctionAngle;

        if (Physics.Raycast(_vehicleTransform.position, Vector3.down, out RaycastHit track, raycastDistance)) 
        {
            correctionAngle = CalculateCorrectionAngle(track, isCollidingOnRight);
            _vehicleTransform.DORotate(new Vector3(_vehicleTransform.rotation.eulerAngles.x,
                _vehicleTransform.rotation.eulerAngles.y + correctionAngle,
                _vehicleTransform.rotation.eulerAngles.z), correctionDuration);
        }    
    }

    private float CalculateCorrectionAngle(RaycastHit track, bool isCollidingOnRight) 
    {
        Transform trackTransform = track.transform;
        Vector3 rotationAxis =  Vector3.zero;
        float correctionAngle = 0;

        if (track.collider.TryGetComponent(out Straight _))
        {
            correctionAngle = Vector3.SignedAngle(_vehicleTransform.forward, trackTransform.forward, Vector3.up);
        }
        else if (track.collider.TryGetComponent(out LeftTurn _))
        {
            float slerpPorportion1 = 0.45f;
            float slerpPorportion2 = 0.25f;

            if (isCollidingOnRight && track.point.z < trackTransform.position.z)
            {
                rotationAxis = Vector3.Slerp(-trackTransform.right, trackTransform.forward, slerpPorportion2);
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
                rotationAxis = Vector3.Slerp(-trackTransform.right, trackTransform.forward, slerpPorportion1);
            }

            correctionAngle = Vector3.SignedAngle(_vehicleTransform.forward, rotationAxis, Vector3.up);
        }
        else if (track.collider.TryGetComponent(out RightTurn _))
        {
            float slerpPorportion1 = 0.65f;
            float slerpPorportion2 = 0.25f;

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

            correctionAngle = Vector3.SignedAngle(_vehicleTransform.forward, rotationAxis, Vector3.up);
        }

        return correctionAngle;
    }

    public void AnalyzeCollision(Collider other)
    {
        if (other.TryGetComponent(out RightTrackBarrier _))
        {
            _inputHandler.BlockRightTurn();
            CorrectTrajectory(true);
        }
        else if (other.TryGetComponent(out LeftTrackBarrier _))
        {
            _inputHandler.BlockLeftTurn();
            CorrectTrajectory(false);
        }
        else if (other.TryGetComponent(out SpeedBonus _))
        {
            _engine.ApplySpeedBonus();
        }
        else if (other.TryGetComponent(out InvincibilityBonus _))
        {
            _invincibilityHandler.ActivateInvincibility();
        }
        else if (other.TryGetComponent(out Obstacle _) && _invincibilityHandler.IsInvincible == false)
        {
            _engine.ApplySpeedPenalty();
        }
    }

    public void OnCollisionEnded(Collider other)
    {
        if (other.TryGetComponent(out RightTrackBarrier _) || other.TryGetComponent(out LeftTrackBarrier _))
            _inputHandler.RemoveTurnBlocks();
    }

    public void OnCollisionStay(Collider other)
    {
        if (other.TryGetComponent(out RightTrackBarrier _))
            _inputHandler.BlockRightTurn();
        else if (other.TryGetComponent(out LeftTrackBarrier _))
            _inputHandler.BlockLeftTurn();
    }
}