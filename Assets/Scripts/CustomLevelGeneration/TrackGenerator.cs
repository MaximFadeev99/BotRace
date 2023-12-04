using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TrackGenerator : MonoBehaviour
{
    private const string Straight = nameof(Straight);
    private const string RightTurn = nameof(RightTurn);
    private const string LeftTurn = nameof(LeftTurn);
    private const int StraightIndex = 0;
    private const int RightTurnIndex = 1;
    private const int LeftTurnIndex = 2;
    private const int EverythingAllowedIndex = -1;
    
    [SerializeField] private GameObject _start;
    [SerializeField] private GameObject _finish;
    [SerializeField] private List<GameObject> _trackParts;

    private readonly List<GameObject> _placedTrackParts = new();

    private float _previousYRotation;
    private string _previousPartType;     

    private void PlaceStartLine() 
    {      
        GameObject nextPart = Instantiate(_start, Vector3.zero, Quaternion.identity);

        _placedTrackParts.Add(nextPart);
        _previousPartType = Straight;
        _previousYRotation = nextPart.transform.rotation.eulerAngles.y;
        nextPart = Instantiate(_trackParts[StraightIndex]);
        nextPart.transform.position = CalculateNextPosition(nextPart, Straight);
        _placedTrackParts.Add(nextPart);
    }

    private void PlaceFinishLine() 
    {
        GameObject finishLine = Instantiate(_finish);
        Vector3 nextPosition = CalculateNextPosition(finishLine, Straight);
        Quaternion nextRotation = CalculateNextRotation(Straight, StraightIndex);

        finishLine.transform.SetPositionAndRotation(nextPosition, nextRotation);
        _placedTrackParts.Add(finishLine);
    }

    private void PlaceNextTrackPart()
    {
        int nextPartIndex = GetRandomIndex(out string nextPartType);
        GameObject nextPart = Instantiate(_trackParts[nextPartIndex]);
        Vector3 nextPosition = CalculateNextPosition(nextPart, nextPartType);
        Quaternion nextRotation = CalculateNextRotation(nextPartType, nextPartIndex);

        nextPart.transform.SetPositionAndRotation(nextPosition, nextRotation);
        nextPart.isStatic = true;
        _placedTrackParts.Add(nextPart);
        _previousPartType = nextPartType;
        _previousYRotation = nextPart.transform.rotation.eulerAngles.y;
    }   

    private Vector3 CalculateNextPosition(GameObject nextTrackPart, string nextPartType) 
    {
        GameObject previousTrackPart = _placedTrackParts[^1];
        Renderer previousPartRenderer = previousTrackPart.GetComponent<Renderer>();
        Renderer nextPartRenderer = nextTrackPart.GetComponent<Renderer>();
        Vector3 offset = Vector3.zero;

        switch (_previousPartType) 
        {
            case Straight:

                switch (_previousYRotation) 
                {
                    case 0f:

                        if (nextPartType == Straight)
                            offset = new Vector3(0f, 0f, previousPartRenderer.bounds.extents.z) + 
                                new Vector3(0f, 0f, nextPartRenderer.bounds.extents.z);
                        else if (nextPartType == RightTurn)
                            offset = new Vector3(0f, 0f, previousPartRenderer.bounds.extents.z) +
                                new Vector3(nextPartRenderer.bounds.extents.x / 2, 0f, nextPartRenderer.bounds.extents.z);
                        else if (nextPartType == LeftTurn)
                            offset = new Vector3(0f, 0f, previousPartRenderer.bounds.extents.z) + 
                                new Vector3(-nextPartRenderer.bounds.extents.x / 2, 0f, nextPartRenderer.bounds.extents.z);

                        break;

                    case 270:
                        
                        if (nextPartType == Straight)
                            offset = new Vector3(-previousPartRenderer.bounds.extents.z * 2f, 0f, 0f) + 
                                new Vector3(-nextPartRenderer.bounds.extents.z, 0f, 0f);
                        else if (nextPartType == RightTurn)
                            offset = new Vector3(-previousPartRenderer.bounds.extents.z * 2f, 0f, 0f) + 
                                new Vector3(-nextPartRenderer.bounds.extents.z, 0f, nextPartRenderer.bounds.extents.x / 2f);
                        break;

                    case 90:

                        if (nextPartType == Straight)
                            offset = new Vector3(previousPartRenderer.bounds.extents.z * 2f, 0f, 0f) + 
                                new Vector3(nextPartRenderer.bounds.extents.z, 0f, 0f);
                        if (nextPartType == LeftTurn)
                            offset = new Vector3(previousPartRenderer.bounds.extents.z * 2f, 0f, 0f) + 
                                new Vector3(nextPartRenderer.bounds.extents.z, 0f, nextPartRenderer.bounds.extents.x / 2f);

                        break;               
                }

                break;

            case RightTurn:

                switch (nextPartType) 
                {
                    case Straight:

                        if (_previousYRotation == 270f)
                        {
                            offset = new Vector3(previousPartRenderer.bounds.extents.x, 0f, 0f) + 
                                new Vector3(nextPartRenderer.bounds.extents.z, 0f, nextPartRenderer.bounds.extents.x) +
                                new Vector3(-0.5f, -0.3f, 0f); 
                        }
                        else if (_previousYRotation == 180f)
                        {
                            offset = new Vector3(0f, 0f, previousPartRenderer.bounds.extents.z) + 
                                new Vector3(-nextPartRenderer.bounds.extents.x, 0f, nextPartRenderer.bounds.extents.z) +
                                new Vector3(0.5f, -0.3f, 0f); 
                        }

                        break;

                    case RightTurn:

                        offset = new Vector3(0f, 0f, previousPartRenderer.bounds.extents.x) + 
                            new Vector3(0f, 0f, nextPartRenderer.bounds.extents.x) +
                            new Vector3(0f, 0f, -0.4f);

                        break;

                    case LeftTurn:

                        if (_previousYRotation == 270f)
                        {
                            offset = new Vector3(previousPartRenderer.bounds.extents.x, 0f, 0f) + 
                                new Vector3(nextPartRenderer.bounds.extents.z, 0f, nextPartRenderer.bounds.extents.x) +
                                new Vector3(0f, 0f, -0.4f);
                        }
                        else if (_previousYRotation == 180f)
                        {
                            offset = new Vector3(-previousPartRenderer.bounds.extents.z, 0f, previousPartRenderer.bounds.extents.x) + 
                                new Vector3(0f, 0f, nextPartRenderer.bounds.extents.x) +
                                new Vector3(0f, 0f, -0.4f);
                        }

                        break;                              
                }

                break;

            case LeftTurn:

                switch (nextPartType) 
                {
                    case Straight:

                        if (_previousYRotation == 0f)
                        {
                            offset = new Vector3(-previousPartRenderer.bounds.extents.x, 0f, 0f) + 
                                new Vector3(-nextPartRenderer.bounds.extents.z, 0f, nextPartRenderer.bounds.extents.x) +
                                new Vector3(0f, -0.3f, 0f); 
                        }
                        else if (_previousYRotation == 90f)
                        {
                            offset = new Vector3(0f, 0f, previousPartRenderer.bounds.extents.z) + 
                                new Vector3(nextPartRenderer.bounds.extents.x, 0f, nextPartRenderer.bounds.extents.z) +
                                new Vector3(0.5f, -0.3f, 0f); 
                        }

                        break;

                    case RightTurn:


                        if (_previousYRotation == 0f)
                        {
                            offset = new Vector3(-previousPartRenderer.bounds.extents.x, 0f, 0f) + 
                                new Vector3(-nextPartRenderer.bounds.extents.z, 0f, nextPartRenderer.bounds.extents.x);
                        }
                        else if (_previousYRotation == 90f)
                        {
                            offset = new Vector3(previousPartRenderer.bounds.extents.z, 0f, previousPartRenderer.bounds.extents.x) + 
                                new Vector3(0f, 0f, nextPartRenderer.bounds.extents.x) +
                                new Vector3(0f, 0f, -0.4f);
                        }

                        break;

                    case LeftTurn:

                        offset = new Vector3(0f, 0f, previousPartRenderer.bounds.extents.x) + 
                            new Vector3(0f, 0f, nextPartRenderer.bounds.extents.x) +
                            new Vector3(0f, 0f, -0.4f);

                        break;
                }

                break;     
        }

        return previousTrackPart.transform.position + offset;
    }

    private Quaternion CalculateNextRotation(string nextPartType, int nextPartTypeIndex) 
    {
        Quaternion originalRotaion = Quaternion.Euler(new Vector3
            (0f, _trackParts[nextPartTypeIndex].transform.rotation.eulerAngles.y, 0f));

        switch (_previousPartType) 
        {
            case Straight:

                if (nextPartType == Straight && _previousYRotation == 270f)
                    return Quaternion.Euler(new Vector3(0f, -90f, 0f));
                else if (nextPartType == Straight && _previousYRotation == 90f)
                    return Quaternion.Euler(new Vector3(0f, 90f, 0f));
                else if (nextPartType == RightTurn && _previousYRotation == 270f)
                    return Quaternion.Euler(new Vector3(0f, -180f, 0f));
                else if (nextPartType == LeftTurn && _previousYRotation == 90f)
                    return Quaternion.Euler(new Vector3(0f, 90f, 0f));
               
                break;
            
            case RightTurn:

                if ((nextPartType == Straight || nextPartType == LeftTurn) && _previousYRotation == 270f)
                    return Quaternion.Euler(new Vector3(0f, 90f, 0f));

                break;
            
            case LeftTurn:
                
                if(nextPartType == Straight && _previousYRotation == 0f)
                    return Quaternion.Euler(new Vector3(0f, -90f, 0f));
                else if(nextPartType == RightTurn && _previousYRotation == 0f)
                    return Quaternion.Euler(new Vector3(0f, -180f, 0f));

                break;           
        }
                          
        return originalRotaion;
    }

    private int GetRandomIndex(out string nextPartType) 
    {
        int restrictedIndex = DefineUncompatibleIndex();
        int randomIndex;

        do 
            randomIndex = Random.Range(0, _trackParts.Count);
        while (restrictedIndex == randomIndex);

        nextPartType = randomIndex switch
        {
            StraightIndex => Straight,
            RightTurnIndex => RightTurn,
            LeftTurnIndex => LeftTurn,
            _ => throw new ArgumentOutOfRangeException(nameof(randomIndex), "Random range is incorrect"),
        };

        return randomIndex;
    }

    private int DefineUncompatibleIndex() 
    {
        switch (_previousPartType) 
        {
            case Straight: 

                if (_previousYRotation == 270f)
                    return LeftTurnIndex;
                else if (_previousYRotation == 90f)
                    return RightTurnIndex;

                break;

            case RightTurn:

                if (_previousYRotation == 270f)
                    return RightTurnIndex;

                break;

            case LeftTurn:

                if(_previousYRotation == 0f)
                    return LeftTurnIndex;

                break;      
        }

        return EverythingAllowedIndex;
    }

    public void GenerateTrack(int trackPartCount)
    {
        PlaceStartLine();

        for (int i = 0; i < trackPartCount; i++)
            PlaceNextTrackPart();

        PlaceFinishLine();
    }

    public IReadOnlyList<GameObject> GetGeneratedTrackParts() 
    {
        if (_placedTrackParts.Count <= 0)
            throw new Exception("The track  hasn't been generated yet. Call GenerateTrack method first");

        return _placedTrackParts;  
    }
}