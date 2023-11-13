using System.Collections;
using UnityEngine;

[System.Serializable]
public class VeichleShaker
{
    [SerializeField] private AudioSource _collisionAudioSource;
    [SerializeField] private AnimationCurve _amplitudeCurve;
    [SerializeField] private float _duration;
    [SerializeField] private float _amplitudeRange;

    private Transform _veichleTransform;
    private Vector3 _shakeAngles = new();
    private float _shakeTimer;

    private void PlayCollisionSound() 
    {
        if (_collisionAudioSource != null)
            _collisionAudioSource.Play();
    }

    public void Intialize(Transform veichleTransform) => 
        _veichleTransform = veichleTransform;

    public IEnumerator Shake()
    {
        PlayCollisionSound();
        _shakeTimer = 1f;

        while (_shakeTimer > 0f)
        {
            _shakeTimer -= Time.deltaTime / _duration;
            float currentOffset = Random.Range(-_amplitudeRange, _amplitudeRange);
            _shakeAngles.x += currentOffset;
            _shakeAngles.y += currentOffset;
            _shakeAngles *= _amplitudeCurve.Evaluate(Mathf.Clamp01(1 - _shakeTimer));
            _veichleTransform.position += _shakeAngles;
            yield return null;
        }
    }
}