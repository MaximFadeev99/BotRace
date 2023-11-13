using DG.Tweening;
using UnityEngine;

public class SwordArch : MonoBehaviour
{
    [SerializeField] private Transform _pivotTransform;
    [SerializeField] private float _rotationSpeed;

    private void Update() =>
        _pivotTransform.DORotate
        (_pivotTransform.rotation.eulerAngles + _rotationSpeed * Time.deltaTime * Vector3.forward, Time.deltaTime);
}