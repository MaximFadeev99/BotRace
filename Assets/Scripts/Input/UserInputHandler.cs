using UnityEngine;

internal class UserInputHandler: InputHandler
{
    [SerializeField] private RightTurnButton _rightTurnButton;
    [SerializeField] private LeftTurnButton _leftTurnButton;   

    protected override float GetInput() 
    {
        if (_rightTurnButton.CurrentValue != 0f)
            return _rightTurnButton.CurrentValue;
        else if (_leftTurnButton.CurrentValue != 0f)
            return _leftTurnButton.CurrentValue;
        else 
            return Input.GetAxis("Horizontal") * 1f;
    }
}