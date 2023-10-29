using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SwordArch : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private float _rotationSpeed;

    private Tween _rotationTween;

    //private void Awake()
    //{
    //    //_rotationTween = _transform.DOLocalRotate(_rotationSpeed, 1f, RotateMode.LocalAxisAdd);
    //}

    private void Start()
    {
        
    }

    private void Update()
    {
        //_transform.localRotation = Quaternion.Euler(_transform.localRotation.eulerAngles.x,
        //   _transform.localRotation.eulerAngles.y + +_rotationSpeed * Time.deltaTime,
        //   _transform.localRotation.eulerAngles.z );

        //_transform.Rotate(0f, 0f, _rotationSpeed * Time.deltaTime, Space.World);

        //_transform.rotation = Quaternion.Euler(0f, 0f, _rotationSpeed * Time.deltaTime);

        _transform.DORotate(_transform.rotation.eulerAngles + Vector3.forward * _rotationSpeed * Time.deltaTime, Time.deltaTime);
    }
}
