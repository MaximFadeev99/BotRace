using System;
using System.Collections.Generic;
using UnityEngine;

public class ControlPointManager : MonoBehaviour
{
    [SerializeField ]private List<ControlPoint> _controlPoints;

    public Action<ControlPoint, BotDriver> ControlPointReached;

    private void OnEnable() =>
        ControlPointReached += OnControlPointReached;

    private void OnDisable() =>
        ControlPointReached -= OnControlPointReached;

    private void OnControlPointReached(ControlPoint reachedControlPoint, BotDriver botDriver) 
    {
        int newPointIndex = _controlPoints.IndexOf(reachedControlPoint) + 1;

        if (newPointIndex > _controlPoints.Count - 1) 
            botDriver.SetControlPoint(reachedControlPoint);

        botDriver.SetControlPoint (_controlPoints[newPointIndex]);
    }

    public ControlPoint GetFirstControlPoint() 
    {
        return _controlPoints[0];
    }
}