using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[System.Serializable]
public class BotDriver 
{
    //добавить ошибки в логику Бота, чтобы контролировать сложность 
    private float _speedRotation = 0.2f;

    private Transform _controlPoint;
    private LayerMask _layerMask = 768;
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

    public BotDriver(Transform veichleTransform, Renderer renderer, Transform controlPoint, Transform raycastPoint)
    {
        _veichleTransform = veichleTransform;
        _renderer = renderer;
        _controlPoint = controlPoint;
        _boxcastPoint = raycastPoint;
        _boxcastSize = new Vector3(_renderer.bounds.extents.x, 2f, 0f);

    }

    public float GetDirection() 
    {
        bool isObstacleSpotted;

        //убрать dotResult после отладки

        if (_isManeuvering == false) 
        {
            _boxcastDirection = _veichleTransform.forward;
            _boxcastOrientation = _veichleTransform.rotation;
        }

        isObstacleSpotted = Physics.BoxCast(_boxcastPoint.position, _boxcastSize, _boxcastDirection, out _newObstacle,
            _boxcastOrientation, 200f, _layerMask, QueryTriggerInteraction.Collide);
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

                if (renderer.bounds.center.x > _controlPoint.position.x)
                {
                    _maneuverSide = -1f;
                }
                else
                {
                    _maneuverSide = 1f;
                }              
            }

            Debug.Log("Avoiding an obstacle");
            return CalculateGradualInput(_maneuverSide);

        }

        
        

        if (Vector3.Dot(_controlPoint.forward, _veichleTransform.forward) < 0.995f && _isManeuvering == false) 
        {
            //Debug.Log(dotResult);
            _correctionAngle = Vector3.SignedAngle(_veichleTransform.forward, _controlPoint.forward, Vector3.up);
            //Debug.Log(_correctionAngle);
            Debug.Log("Aligning with control point's forward");
            return CalculateGradualInput(_correctionAngle);
        }

        Debug.Log("I am going straight right");
        return CalculateGradualInput();
    }

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

    //public void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;

    //    Gizmos.DrawWireCube(_raycastPoint.position, _boxcastSize);
    //    Gizmos.DrawCube(_newObstacle.point, _boxcastSize);
    //    Gizmos.DrawCube(_currentObstacle.point, _boxcastSize);
    //}

}
