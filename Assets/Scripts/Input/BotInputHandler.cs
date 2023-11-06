using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotInputHandler : MonoBehaviour, IInputHandler
{
    private BotDriver _botDriver;
    [SerializeField] private ControlPointManager _controlPointManager;
    [SerializeField] private Transform _raycastPoint;
    //[SerializeField] private LayerMask _layerMask;

    private Transform _veichleTransform;   
    private Renderer _renderer;
    private float _directionInput;
    private bool _isRightTurnBlock = false;
    private bool _isLeftTurnBlock = false;

    public BotDriver BotDriver => _botDriver;

    private void Awake()
    {
        _veichleTransform = transform;
        _renderer = GetComponent<Renderer>();
        //_botDriver = GetComponent<BotDriver>();
        _botDriver = new(_veichleTransform, _renderer, _controlPointManager, _raycastPoint);
        //_layerMask = 0;
    }

    public float InquireInput()
    {
        _directionInput = _botDriver.GetDirection();

        if (_isRightTurnBlock)
            _directionInput = Mathf.Clamp(_directionInput, -1f, 0f);

        if (_isLeftTurnBlock)
            _directionInput = Mathf.Clamp01(_directionInput);

        return _directionInput;
    }

    public void BlockLeftTurn()
    {
        _isLeftTurnBlock = true;
    }

    public void BlockRightTurn()
    {
        _isRightTurnBlock = true;
    }

    public void RemoveTurnBlocks()
    {
        _isRightTurnBlock = false;
        _isLeftTurnBlock = false;
    }

    //public void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;

    //    Gizmos.DrawLine(_veichleTransform.position, new Vector3
    //        (_veichleTransform.position.x, _veichleTransform.position.y, _veichleTransform.position.z + 50f));
    //}
}
