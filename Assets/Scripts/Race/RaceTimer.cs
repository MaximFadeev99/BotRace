using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RaceTimer
{
    private TextMeshProUGUI _screenTimer;    
    private bool _isActive = false;

    public float CurrentTime { get; private set; }
    public string CurrentOutputTime { get; private set; }

    public RaceTimer(TextMeshProUGUI screenTimer)
    {
        _screenTimer = screenTimer;
    }

    public void Tick() 
    {
        if (_isActive) 
        {
            CurrentTime += Time.deltaTime * 1.5f;
            //Debug.Log(CurrentTime);
            TimeSpan currentTime = TimeSpan.FromSeconds(CurrentTime);

            //if (currentTime.Minutes.ToString().Length == 1)
            //{
            //    outputTime = $"0{currentTime.Minutes}:";
            //}
            //else 
            //{
            //    outputTime = $"{currentTime.Minutes}:";
            //}

            CurrentOutputTime = $"{currentTime.Minutes}:";

            if (currentTime.Seconds.ToString().Length == 1)
            {
                CurrentOutputTime += $"0{currentTime.Seconds}";
            }
            else 
            {
                CurrentOutputTime += currentTime.Seconds;
            }

            _screenTimer.text = CurrentOutputTime;
        }
    }

    public void Activate() => _isActive = true;
    public void Deactivate() => _isActive = false;

}
