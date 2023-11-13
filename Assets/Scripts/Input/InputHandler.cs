using UnityEngine;

public abstract class InputHandler : MonoBehaviour, IInputHandler
{
    private float _directionInput;
    private bool _isRightTurnBlock = false;
    private bool _isLeftTurnBlock = false;

    protected abstract float GetInput();
    
    public float InquireInput()
    {
        _directionInput = GetInput();

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
}