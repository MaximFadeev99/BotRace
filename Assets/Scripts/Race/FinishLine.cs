using System;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private GameObject _finishFireworksHolder;
    
    public Action<Hover> Crossed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Hover hover))
            Crossed?.Invoke(hover);
    }

    public void StartFireworks() =>
       _finishFireworksHolder.gameObject.SetActive(true);
}