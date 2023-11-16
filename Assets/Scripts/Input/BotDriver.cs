using UnityEngine;

public class BotDriver 
{
    private readonly Transform _veichleTransform;
    private readonly ControlPointManager _controlPointManager;
    private readonly Renderer _renderer;
    private readonly Transform _boxcastPoint;
    private readonly LayerMask _layerMask = 2048;
    private readonly float _inputLerpRate = 0.2f;
    private readonly float _raycastDistance = 150f;
    private readonly float _dotResultThreshold = 0.995f;

    private ControlPoint _controlPoint;
    private Transform _controlPointTransform;
    private Vector3 _boxcastDirection;
    private Vector3 _boxcastSize;
    private Quaternion _boxcastOrientation;
    private RaycastHit _newObstacle;
    private RaycastHit _currentObstacle;

    private bool _isManeuvering = false;
    private float _correctionAngle;
    private float _currentInput;
    private float _maneuverSide;

    public BotDriver(Transform veichleTransform, Renderer renderer, ControlPointManager controlPointManager, Transform raycastPoint)
    {
        _veichleTransform = veichleTransform;
        _renderer = renderer;
        _controlPointManager = controlPointManager;
        _boxcastPoint = raycastPoint;
        _boxcastSize = new Vector3(_renderer.bounds.extents.x, _renderer.bounds.extents.y, 0f);
        _controlPoint = _controlPointManager.GetFirstControlPoint();
        _controlPointTransform = _controlPoint.transform;
    }

    public float GetDirection() 
    {
        if (_isManeuvering == false) 
        {
            _boxcastDirection = _veichleTransform.forward;
            _boxcastOrientation = _veichleTransform.rotation;
        }

        bool isObstacleSpotted = Physics.BoxCast(_boxcastPoint.position, _boxcastSize, _boxcastDirection, 
            out _newObstacle, _boxcastOrientation, _raycastDistance, _layerMask, QueryTriggerInteraction.Collide);

        if (isObstacleSpotted == false)
        {
            _isManeuvering = false;
        }
        else 
        {
            if ((_currentObstacle.collider == null || _newObstacle.collider.gameObject != _currentObstacle.collider.gameObject) 
                && _newObstacle.collider.TryGetComponent(out Obstacle _))
            {
                _isManeuvering = true;
                _currentObstacle = _newObstacle;
                Renderer renderer = _currentObstacle.collider.GetComponent<Renderer>();

                if (renderer.bounds.center.x > _controlPointTransform.position.x)
                    _maneuverSide = -1f;
                else
                    _maneuverSide = 1f;              
            }

            return CalculateGradualInput(_maneuverSide);
        }
      
        if (Vector3.Dot(_veichleTransform.forward, _controlPointTransform.forward) 
            < _dotResultThreshold && _isManeuvering == false) 
        {          
            _correctionAngle = Vector3.SignedAngle
                (_veichleTransform.forward, _controlPointTransform.forward, Vector3.up);
            return CalculateGradualInput(_correctionAngle);
        }

        return CalculateGradualInput();
    }

    private float CalculateGradualInput(float correctionAngle = 0f) 
    {
        float maxInput;

        if (correctionAngle > 0)
            maxInput = 1f;
        else if (correctionAngle < 0)
            maxInput = -1f;
        else 
            maxInput = 0f;

        _currentInput = Mathf.MoveTowards(_currentInput, maxInput, _inputLerpRate);
        return _currentInput;
    }

    public void SetControlPoint(ControlPoint newControlPoint) 
    {
        _controlPoint = newControlPoint;
        _controlPointTransform = _controlPoint.transform;
    }
}