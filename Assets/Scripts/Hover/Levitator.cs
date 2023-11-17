using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class Levitator : ILevitator
{
    [SerializeField] private float _initialAltitutde;
    [SerializeField] private float _upDownLevitationRange;
    [SerializeField] private float _upDownLevitationTime;

    private Transform _vehicleTransform;
    private Tween _idleLevitationTween;

    public void Initialize(Transform vehicleTransform)
    {
        _vehicleTransform = vehicleTransform;
        _vehicleTransform.position = new Vector3
            (_vehicleTransform.position.x, _initialAltitutde, _vehicleTransform.position.z);
        _idleLevitationTween = _vehicleTransform.DOMoveY
            (_vehicleTransform.position.y - _upDownLevitationRange, _upDownLevitationTime).SetLoops(-1, LoopType.Yoyo);
        _idleLevitationTween.SetEase(Ease.Linear).Pause();
    }

    public void StartLevitation() =>
        _idleLevitationTween.Play();

    public void StopLevitation() =>
        _idleLevitationTween.Pause();
}
