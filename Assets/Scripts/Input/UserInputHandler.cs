using UnityEngine;

internal class UserInputHandler: MonoBehaviour, IInputHandler
{
    private float _directionInput;
    private bool _isRightTurnBlock= false;
    private bool _isLeftTurnBlock= false;

    public float InquireInput()
    {
        _directionInput = Input.GetAxis("Horizontal");

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
