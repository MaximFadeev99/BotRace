using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletScaler: MonoBehaviour
{
    private Transform _transform;
    private float _initialScale;
    private float _endScale = 0.65f;
    private float _scaleTime = 1f;

    private void Awake()
    {
        _transform = transform;
        _transform.DOScale(_endScale, _scaleTime).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }
}
