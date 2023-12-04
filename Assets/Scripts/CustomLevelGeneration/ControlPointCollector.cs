using System.Collections.Generic;
using UnityEngine;

public class ControlPointCollector
{    
    private const string ControlPoint1 = nameof(ControlPoint1);
    private const string ControlPoint2 = nameof(ControlPoint2);

    private readonly List<ControlPoint> _controlPoints = new();
    private readonly ControlPointManager _controlPointManager;
    
    public ControlPointCollector (IReadOnlyList<GameObject> trackParts, ControlPointManager controlPointManager) 
    {
        for (int i = 1; i < trackParts.Count; i++) 
        {
            Transform transform = trackParts[i].transform;

            _controlPoints.Add(transform.Find(ControlPoint1).GetComponent<ControlPoint>());
            _controlPoints.Add(transform.Find(ControlPoint2).GetComponent<ControlPoint>());
        }

        _controlPointManager = controlPointManager;
    }

    public void ActivateControlPoints() 
    {
        foreach (ControlPoint controlPoint in _controlPoints) 
            controlPoint.SetControlPointManager(_controlPointManager);         

        _controlPointManager.Initialize(_controlPoints);
    }
}