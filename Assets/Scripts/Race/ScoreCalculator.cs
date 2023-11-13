using UnityEngine;

public class ScoreCalculator
{
    private readonly float _basicScore = 1000f;
    private readonly float _placeCorrectionRate = 0.1f;
    private readonly float _averageTime;

    public ScoreCalculator(float trackAverageTime)=>
        _averageTime = trackAverageTime;
    
    public float CalculateScore(float actualTime, int place) 
    {
        float timeCorrection = actualTime / _averageTime;
        float placeCorrection = 1f;

        if (place != 1)
            placeCorrection -= _placeCorrectionRate * (place - 1);

        float score = _basicScore / timeCorrection * placeCorrection;
        return Mathf.Clamp(score, 0f, score);
    }
}