using UnityEngine;
using DG.Tweening;
using System.Collections;

[System.Serializable]
public class Levitator
{
    [SerializeField] private float _initialAltitutde;
    [SerializeField] private float _upDownLevitationRange;
    [SerializeField] private float _upDownLevitationTime;
    [SerializeField] private bool _isAltitudeCorrectionRequired = true;

    private Transform _vehicleTransform;
    private Tween _idleLevitationTween;
    private float _yPosition;
    
    public bool IsAltitudeCorrectionRequired => _isAltitudeCorrectionRequired;

    public float YPosition => _yPosition;

    public void Initialize(Transform vehicleTransform)
    {
        _vehicleTransform = vehicleTransform;
        _vehicleTransform.position = new Vector3
            (_vehicleTransform.position.x, _initialAltitutde, _vehicleTransform.position.z);
        _idleLevitationTween = _vehicleTransform.DOMoveY
            (_vehicleTransform.position.y - _upDownLevitationRange, _upDownLevitationTime).SetLoops(-1, LoopType.Yoyo);
        _idleLevitationTween.SetEase(Ease.Linear).Pause();
    }

    public IEnumerator CorrectAltitude()
    {
        float raycastDistance = 10f;
        float initialAltitude = _vehicleTransform.position.y;
        var waitTime = new WaitForSeconds(5f);

        while (_isAltitudeCorrectionRequired) 
        {
            Physics.Raycast(_vehicleTransform.position, Vector3.down, out RaycastHit track, raycastDistance);
            float currentAltitude = _vehicleTransform.position.y - track.transform.position.y;

            if (currentAltitude > initialAltitude) 
            {
                _yPosition -= currentAltitude - initialAltitude;
            }

            yield return waitTime;
        }
    }

    public void VerifyAltitude() 
    {
        _vehicleTransform.position = new Vector3
            (_vehicleTransform.position.x, _yPosition, _vehicleTransform.position.z);   
    }

    public void StartLevitation() =>
        _idleLevitationTween.Play();

    public void StopLevitation() 
    {
        _idleLevitationTween.Pause();
        _yPosition = _vehicleTransform.position.y;
    }

    public void TurnOffAltitudeCorrection() =>
        _isAltitudeCorrectionRequired = false;
}