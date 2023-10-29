using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class TransformLevitator : ILevitator
{
    [SerializeField] private float _initialAltitutde;
    [SerializeField] private float _upDownLevitationRange;
    [SerializeField] private float _upDownLevitationTime;

    private Transform _veichleTransform;
    private Tween _idleLevitationTween;

    public void Initialize(Transform veichleTransform)
    {
        _veichleTransform = veichleTransform;
        _veichleTransform.position = new Vector3(_veichleTransform.position.x, _initialAltitutde, _veichleTransform.position.z);
        _idleLevitationTween = _veichleTransform.DOMoveY(_veichleTransform.position.y - _upDownLevitationRange, _upDownLevitationTime).
            SetLoops(-1, LoopType.Yoyo);
        _idleLevitationTween.Pause();
    }

    public void StartLevitation() 
    {
        _idleLevitationTween.Play();
    }
    

    public void StopLevitation() 
    {
        _idleLevitationTween.Pause();
    }
}
