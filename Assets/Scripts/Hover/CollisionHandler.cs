using UnityEngine;
using DG.Tweening;

public class CollisionHandler
{
    private readonly Engine _engine;
    private readonly Transform _veichleTransform;
    private readonly InvincibilityHandler _invincibilityHandler;
    private readonly IInputHandler _inputHandler;

    public CollisionHandler (Hover hover) 
    {
        _veichleTransform = hover.Transform;
        _inputHandler = hover.InputHandler;
        _engine = hover.Engine;
        _invincibilityHandler = hover.InvincibilityHandler;
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

    public void OnCollisionEnded() =>
        _inputHandler.RemoveTurnBlocks();

    private void CorrectTrajectory(bool isCollidingOnRight)
    {
        float raycastDistance = 5f;
        float correctionDuration = 0.2f;
        float correctionAngle;

        Physics.Raycast(_veichleTransform.position, Vector3.down, out RaycastHit track, raycastDistance);
        correctionAngle = CalculateCorrectionAngle(track, isCollidingOnRight);
        _veichleTransform.DORotate(new Vector3(_veichleTransform.rotation.eulerAngles.x, 
            _veichleTransform.rotation.eulerAngles.y + correctionAngle,
            _veichleTransform.rotation.eulerAngles.z), correctionDuration);
    }

    private float CalculateCorrectionAngle(RaycastHit track, bool isCollidingOnRight) 
    {
        Transform trackTransform = track.transform;
        Vector3 rotationAxis =  Vector3.zero;
        float correctionAngle = 0;

        if (track.collider.TryGetComponent(out Straight _))
        {
            correctionAngle = Vector3.SignedAngle(_veichleTransform.forward, trackTransform.forward, Vector3.up);
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

            correctionAngle = Vector3.SignedAngle(_veichleTransform.forward, rotationAxis, Vector3.up);
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

            correctionAngle = Vector3.SignedAngle(_veichleTransform.forward, rotationAxis, Vector3.up);
        }

        return correctionAngle;
    }
}