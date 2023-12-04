using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrackGenerator))]
[RequireComponent(typeof(ObstacleGenerator))]
[RequireComponent(typeof(BonusGenerator))]
public class LevelBuilder : MonoBehaviour
{
    [SerializeField] private List<Hover> _vehicles;
    [SerializeField] private RaceManager _raceManager;
    [SerializeField] private ControlPointManager _controlPointManager;
    
    private TrackGenerator _trackGenerator;
    private ObstacleGenerator _obstacleGenerator;
    private BonusGenerator _bonusGenerator;
    private IReadOnlyList<GameObject> _trackParts;

    private void Awake()
    {
        _trackGenerator = GetComponent<TrackGenerator>();
        _obstacleGenerator = GetComponent<ObstacleGenerator>();
        _bonusGenerator = GetComponent<BonusGenerator>();       

        BuildLevel();
    }
   
    private void BuildLevel() 
    {       
        _trackGenerator.GenerateTrack(CustomLevelConfiguration.TrackPartCount);
        _trackParts = _trackGenerator.GetGeneratedTrackParts();
        _obstacleGenerator.GenerateObstacles(_trackParts, CustomLevelConfiguration.DifficultyLevel);
        _bonusGenerator.GenerateBonuses(_trackParts);

        FinishLine finishLine = _trackParts[^1].transform.Find("FinishLine").GetComponent<FinishLine>();
        _raceManager.Initialize(finishLine, _vehicles, CustomLevelConfiguration.TrackTime, 
            CustomLevelConfiguration.TrackLength, CustomLevelConfiguration.DifficultyLevel);

        ControlPointCollector controlPointCollector = new(_trackParts, _controlPointManager);
        controlPointCollector.ActivateControlPoints();

        PlaceVehicles();      
    }

    private void PlaceVehicles() 
    {
        const string Vehicle1 = nameof(Vehicle1);
        const string Vehicle2 = nameof(Vehicle2);
        const string Vehicle3 = nameof(Vehicle3);

        Transform startTransform = _trackParts[0].transform;
        Vector3 vehiclePosition1 = startTransform.Find(Vehicle1).position;
        Vector3 vehiclePosition2 = startTransform.Find(Vehicle2).position;
        Vector3 vehiclePosition3 = startTransform.Find(Vehicle3).position;
        List<Vector3> vacantPositions = new()
        {
            vehiclePosition1,
            vehiclePosition2,
            vehiclePosition3
        };

        foreach (Hover vehicle in _vehicles) 
        {
            Vector3 assignedPosition = vacantPositions[Random.Range(0, vacantPositions.Count)];
            vehicle.transform.position = assignedPosition;
            vacantPositions.Remove(assignedPosition);
        }
    }
}