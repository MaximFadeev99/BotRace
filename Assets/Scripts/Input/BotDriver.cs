using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[System.Serializable]
public class BotDriver 
{
    private float _speedRotation = 0.2f;

    private ControlPointManager _controlPointManager;
    private ControlPoint _controlPoint;
    private Transform _controlPointTransform;
    private LayerMask _layerMask = 2560; //2560
    private Transform _veichleTransform;
    private Renderer _renderer;
    private Transform _boxcastPoint;

    private bool _isManeuvering = false;
    private float _correctionAngle;
    private Vector3 _boxcastDirection;
    private Vector3 _boxcastSize;
    private Quaternion _boxcastOrientation;
    private float _currentInput;
    private float _maneuverSide;
    private RaycastHit _newObstacle;
    private RaycastHit _currentObstacle;

    public BotDriver(Transform veichleTransform, Renderer renderer, ControlPointManager controlPointManager, Transform raycastPoint)
    {
        _veichleTransform = veichleTransform;
        _renderer = renderer;
        _controlPointManager = controlPointManager;
        _boxcastPoint = raycastPoint;
        _boxcastSize = new Vector3(_renderer.bounds.extents.x, 2f, 0f);
        _controlPoint = _controlPointManager.GetFirstControlPoint();
        _controlPointTransform = _controlPoint.transform;
    }

    public float GetDirection() 
    {
        //TryChangeControlPoint();
        //Debug.Log(_controlPoint.gameObject.name);

        if (_isManeuvering == false) 
        {
            _boxcastDirection = _veichleTransform.forward;
            _boxcastOrientation = _veichleTransform.rotation;
        }

        bool isObstacleSpotted = Physics.BoxCast(_boxcastPoint.position, _boxcastSize, _boxcastDirection, out _newObstacle,
            _boxcastOrientation, 150f, _layerMask, QueryTriggerInteraction.Collide);
        //Debug.Log(isObstacleSpotted);

        if (isObstacleSpotted == false)
        {
            _isManeuvering = false;
        }
        else 
        {
            if ((_currentObstacle.collider == null || _newObstacle.collider.gameObject != _currentObstacle.collider.gameObject) &&
            _newObstacle.collider.TryGetComponent(out Obstacle _))
            {
                _isManeuvering = true;
                _currentObstacle = _newObstacle;
                Renderer renderer = _currentObstacle.collider.GetComponent<Renderer>();

                if (renderer.bounds.center.x > _controlPointTransform.position.x)
                {
                    _maneuverSide = -1f;
                }
                else
                {
                    _maneuverSide = 1f;
                }              
            }

            //Debug.Log("Avoiding an obstacle");
            return CalculateGradualInput(_maneuverSide);

        }

        float dotResult = Vector3.Dot(_veichleTransform.forward, _controlPointTransform.forward);
        //Debug.Log(dotResult);

        if (dotResult < 0.995f && _isManeuvering == false) 
        {
            
            _correctionAngle = Vector3.SignedAngle(_veichleTransform.forward, _controlPointTransform.forward, Vector3.up);
            //Debug.Log(_correctionAngle);
            //Debug.Log("Aligning with control point's forward");
            return CalculateGradualInput(_correctionAngle);
        }

        //Debug.Log("I am going straight right");
        return CalculateGradualInput();
    }

    //private void TryChangeControlPoint() 
    //{
    //    if (_veichleTransform.position.z >= _controlPointTransform.position.z) 
    //    {
    //        _controlPoint = _controlPointManager.GiveNextPoint(_controlPoint);
    //        _controlPointTransform = _controlPoint.transform;
    //    }
    //}

    private float CalculateGradualInput(float correctionAngle = 0f) 
    {
        float maxInput;

        if (correctionAngle > 0)
        {
            maxInput = 1f;
        }
        else if (correctionAngle < 0)
        {
            maxInput = -1f;
        }
        else 
        {
            maxInput = 0f;
        }

        _currentInput = Mathf.MoveTowards(_currentInput, maxInput, _speedRotation);
        return _currentInput;
    }

    public void SetControlPoint(ControlPoint newControlPoint) 
    {
        _controlPoint = newControlPoint;
        _controlPointTransform = _controlPoint.transform;
    }

    //public void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;

    //    Gizmos.DrawWireCube(_raycastPoint.position, _boxcastSize);
    //    Gizmos.DrawCube(_newObstacle.point, _boxcastSize);
    //    Gizmos.DrawCube(_currentObstacle.point, _boxcastSize);
    //}

}
