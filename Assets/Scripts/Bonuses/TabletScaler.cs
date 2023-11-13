using DG.Tweening;
using UnityEngine;

public class TabletScaler: MonoBehaviour
{
    private void Awake()
    {
        float endScale = 0.65f;
        float scaleTime = 1f;

        transform.DOScale(endScale, scaleTime).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }
}