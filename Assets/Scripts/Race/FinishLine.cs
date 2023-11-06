using System;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public Action<Hover> Crossed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Hover hover))
            Crossed?.Invoke(hover);
    }
}
