using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleGenerator : MonoBehaviour
{
    private const string SpawnPointName = "Obstacle";
    private const int HammersIndex = 0;
    private const int JumpingBallsIndex = 1;
    private const int PressIndex = 2;
    private const int SwordArchIndex = 3;
    private const int WallIndex = 4;

    [SerializeField] private List<GameObject> _obstaclePrefabs;

    private readonly List<int> _bannedObstaclePlaces = new();

  
    private int GetRandomObstacleIndex() 
    {
        return Random.Range(0, _obstaclePrefabs.Count); 
    }

    private int GetRandomObstaclePlace() 
    {
        int nextObstaclePlace;

        do 
        {
            nextObstaclePlace = Random.Range(1, 4);
        }
        while(_bannedObstaclePlaces.Contains(nextObstaclePlace));

        _bannedObstaclePlaces.Add(nextObstaclePlace);
        return nextObstaclePlace;
    }

    private Vector3 CalculateOffset(int obstacleIndex) 
    {
        switch (obstacleIndex) 
        {
            case HammersIndex:
                return new Vector3(0f, -2.59f, 0f);

            case JumpingBallsIndex:
                return new Vector3(1f, 0f, 0f);

            case PressIndex:
                return new Vector3(0f, -2f, 0f);

            case SwordArchIndex:
                return new Vector3(0f, 18.3f, 0f);

            case WallIndex:
                return new Vector3(0f, 13.7f, 0f);

        }
        
        return Vector3.zero;
    }

    public void GenerateObstacles(IReadOnlyList<GameObject> trackParts, int countPerTrackPart)
    {
        for (int i = 2; i < trackParts.Count - 2; i++) 
        {        
            for (int j = 0; j < countPerTrackPart; j++)
            {
                int nextObstacleIndex = GetRandomObstacleIndex();
                int placementIndex = GetRandomObstaclePlace();
                Transform obstaclePlace = trackParts[i].transform.Find(SpawnPointName + placementIndex);
                Vector3 obstaclePosition = obstaclePlace.position + CalculateOffset(nextObstacleIndex);

                Instantiate(_obstaclePrefabs[nextObstacleIndex], obstaclePosition, obstaclePlace.rotation);
            }

            _bannedObstaclePlaces.Clear();
        }      
    }
}