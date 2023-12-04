using Agava.WebUtility;
using UnityEngine;

public class GameStopper: MonoBehaviour
{
    private static bool _isGamePausedManually = false;
    
    private void OnEnable()
    {
        Application.focusChanged += OnInBackgroundChangeAplication;
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChangeWeb;
    }

    private void OnDisable()
    {
        Application.focusChanged -= OnInBackgroundChangeAplication;
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChangeWeb;
    }

    private void OnInBackgroundChangeWeb(bool isBackground)
    {      
        if (_isGamePausedManually == false) 
        {
            MuteAudio(isBackground);
            StopTime(isBackground);
        }       
    }


    private void OnInBackgroundChangeAplication(bool inApp)
    {
        if (_isGamePausedManually == false) 
        {
            MuteAudio(!inApp);
            StopTime(!inApp);
        }              
    }

    private static void MuteAudio(bool value) =>
        AudioListener.pause = value;

    private static void StopTime(bool value) =>
        Time.timeScale = value ? 0 : 1;

    public static void PauseGame() 
    {
        MuteAudio(true);
        StopTime(true);
        _isGamePausedManually = true;
    }

    public static void TryUnpauseGame() 
    {
        if (_isGamePausedManually == false)
        {
            MuteAudio(false);
            StopTime(false);
        }
    }

    public static void ResetManualPause() =>
        _isGamePausedManually = false;
}