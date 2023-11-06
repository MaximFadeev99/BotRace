using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPoint : MonoBehaviour
{
    [SerializeField] private ControlPointManager _controlPointManager;

    private void OnTriggerExit (Collider other)
    {
        if (other.TryGetComponent(out BotInputHandler botInputHandler)) 
        {
            _controlPointManager.ControlPointReached?.Invoke(this, botInputHandler.BotDriver);
        }
    }
}
