using UnityEngine;

public class InvincibilityHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem _invincibilityEffect;
    [SerializeField] private float _invincibilityTime;

    private readonly Timer _invincibilityTimer = new();

    public bool IsInvincible { get; private set; }

    private void Update()
    {
        if (_invincibilityTimer.IsActive)
            _invincibilityTimer.Tick();
    }

    private void DeactivateInvincibility()
    {
        IsInvincible = false;
        _invincibilityEffect.Stop();
        _invincibilityEffect.Clear();
        _invincibilityTimer.TimeIsUp -= DeactivateInvincibility;
    }

    public void ActivateInvincibility()
    {
        if (IsInvincible)
        {
            _invincibilityTimer.Start(_invincibilityTime);
            return;
        }

        IsInvincible = true;
        _invincibilityEffect.Play();
        _invincibilityTimer.Start(_invincibilityTime);
        _invincibilityTimer.TimeIsUp += DeactivateInvincibility;
    }
}