using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class BotInputHandler : InputHandler
{
    [SerializeField] private ControlPointManager _controlPointManager;
    [SerializeField] private Transform _raycastPoint;

    private Transform _vehicleTransform;   
    private Renderer _renderer;

    public BotDriver BotDriver { get; private set; }

    private void Awake()
    {
        _vehicleTransform = transform;
        _renderer = GetComponent<Renderer>();
        BotDriver = new(_vehicleTransform, _renderer, _controlPointManager, _raycastPoint);
    }

    protected override float GetInput()
    {
        return BotDriver.GetDirection();
    }
}