using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeftTurnButton : TurnButton
{
    private void Awake()
    {
        MinValue = -1f;
        MaxValue = 0f;
        CurrentValue = 0f;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        CurrentValue = Mathf.MoveTowards(CurrentValue, MinValue, ValueChangeStep);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        CurrentValue = Mathf.MoveTowards(CurrentValue, MaxValue, ValueChangeStep);
    }
}
