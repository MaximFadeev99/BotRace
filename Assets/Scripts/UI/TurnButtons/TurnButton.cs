using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class TurnButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    protected float MinValue;
    protected float MaxValue;

    private float _curentValue = 0f;

    protected float ValueChangeStep { get; } = 1f;

    public float CurrentValue 
    {
        get 
        {
            return _curentValue;
        }

        protected set 
        {
            _curentValue = Mathf.Clamp(value, MinValue, MaxValue);
        }
    }

    public abstract void OnPointerDown(PointerEventData eventData);

    public abstract void OnPointerUp(PointerEventData eventData);
}
