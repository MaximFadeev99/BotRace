using UnityEngine;

internal class UserInputHandler: InputHandler
{
    [SerializeField] private RightTurnButton _rightTurnButton;
    [SerializeField] private LeftTurnButton _leftTurnButton;

    private bool _isMobilePlatform;

    private void Awake()
    {
        _isMobilePlatform = Application.isMobilePlatform;
        _rightTurnButton.gameObject.SetActive(_isMobilePlatform);
        _leftTurnButton.gameObject.SetActive(_isMobilePlatform);
    }

    protected override float GetInput() 
    {
        if (_isMobilePlatform)
        {
            if (_rightTurnButton.CurrentValue != 0f)
                return _rightTurnButton.CurrentValue;
            else if (_leftTurnButton.CurrentValue != 0f)
                return _leftTurnButton.CurrentValue;

            return 0f;
        }
        else 
        {
            return Input.GetAxis("Horizontal");           
        }
    }
}