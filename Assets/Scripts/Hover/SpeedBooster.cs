using UnityEngine;

public class SpeedBooster : MonoBehaviour 
{
    [SerializeField] private ParticleSystem _speedBoostEffect;
    [SerializeField] private float _speedBoostRate;
    [SerializeField] private float _speedBoostTime;   

    private readonly Timer _speedBoostTimer = new();

    private Mover _mover;
    private bool _isSpeedBoostActive;

    private void DeactivateSpeedBoost()
    {
        _isSpeedBoostActive = false;
        _speedBoostEffect.Stop();
        _mover.ResetMaxSpeed();
        _speedBoostTimer.TimeIsUp -= DeactivateSpeedBoost;
    }

    private void Update()
    {
        if (_speedBoostTimer.IsActive)
            _speedBoostTimer.Tick();
    }

    public void Intialize(Mover mover) =>
        _mover = mover;

    public void ActivateSpeedBoost()
    {
        if (_isSpeedBoostActive)
        {
            _speedBoostTimer.Start(_speedBoostTime);
            return;
        }

        _isSpeedBoostActive = true;
        _speedBoostEffect.Play();
        _speedBoostTimer.Start(_speedBoostTime);
        _speedBoostTimer.TimeIsUp += DeactivateSpeedBoost;
        _mover.ChangeMaxSpeed(_speedBoostRate);
    }   
}
