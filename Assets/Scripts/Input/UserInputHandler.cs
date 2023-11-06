using UnityEngine;

internal class UserInputHandler: MonoBehaviour, IInputHandler
{
    [SerializeField] private RightTurnButton _rightTurnButton;
    [SerializeField] private LeftTurnButton _leftTurnButton;
    private float _directionInput;
    private bool _isRightTurnBlock= false;
    private bool _isLeftTurnBlock= false;

    public float InquireInput()
    {
        //_directionInput = Input.GetAxis("Horizontal");
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

    private float GetInput() 
    {
        if (_rightTurnButton.CurrentValue != 0f)
        {
            return _rightTurnButton.CurrentValue;
        }
        else if (_leftTurnButton.CurrentValue != 0f)
        {
            return _leftTurnButton.CurrentValue;
        }
        else 
        {
            return Input.GetAxis("Horizontal") * 1f;
        }
    }
}
