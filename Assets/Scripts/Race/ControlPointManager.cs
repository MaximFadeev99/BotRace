using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControlPointManager : MonoBehaviour
{
    [SerializeField ]private List<ControlPoint> _controlPoints;

    public Action<ControlPoint, BotDriver> ControlPointReached;

    public void OnEnable()
    {
        ControlPointReached += OnControlPointReached;
    }

    public void OnDisable()
    {
        ControlPointReached -= OnControlPointReached;
    }

    public void OnControlPointReached(ControlPoint reachedControlPoint, BotDriver botDriver) 
    {
        int newPointIndex = _controlPoints.IndexOf(reachedControlPoint) + 1;

        if (newPointIndex > _controlPoints.Count - 1) 
        {
            botDriver.SetControlPoint(reachedControlPoint);
        }

        botDriver.SetControlPoint (_controlPoints[newPointIndex]);
    }

    public ControlPoint GetFirstControlPoint() 
    {
        return _controlPoints[0];
    }
}
