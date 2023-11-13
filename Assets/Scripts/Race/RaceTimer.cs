using System;
using TMPro;
using UnityEngine;

public class RaceTimer
{
    private readonly TextMeshProUGUI _screenTimer;
    private readonly float _speedUpRate;

    private bool _isActive = false;

    public RaceTimer(TextMeshProUGUI screenTimer, float speedUpRate)
    {
        _screenTimer = screenTimer;
        _speedUpRate = speedUpRate;
    }

    public float CurrentTime { get; private set; }
    public string CurrentOutputTime { get; private set; }

    public void Tick() 
    {
        if (_isActive) 
        {
            CurrentTime += Time.deltaTime * _speedUpRate;
            TimeSpan currentTime = TimeSpan.FromSeconds(CurrentTime);

            CurrentOutputTime = $"{currentTime.Minutes}:";

            if (currentTime.Seconds.ToString().Length == 1)
                CurrentOutputTime += $"0{currentTime.Seconds}";
            else 
                CurrentOutputTime += currentTime.Seconds;

            _screenTimer.text = CurrentOutputTime;
        }
    }

    public void Activate() => 
        _isActive = true;

    public void Deactivate() => 
        _isActive = false;
}