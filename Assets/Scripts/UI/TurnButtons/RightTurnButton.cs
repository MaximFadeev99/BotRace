using UnityEngine;
using UnityEngine.EventSystems;

public class RightTurnButton : TurnButton
{
    private void Awake()
    {
        MinValue = 0f;
        MaxValue = 1f;
        CurrentValue = 0f;
    }

    public override void OnPointerDown(PointerEventData eventData) =>
        CurrentValue = Mathf.MoveTowards(CurrentValue, MaxValue, ValueChangeStep);

    public override void OnPointerUp(PointerEventData eventData) =>
        CurrentValue = Mathf.MoveTowards(CurrentValue, MinValue, ValueChangeStep);
}