using Agava.WebUtility;
using UnityEngine;

public class FocusObserver : MonoBehaviour
{
    private void OnEnable()
    {
        //Application.focusChanged += OnInBackgroundChange;
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;
    }

    private void OnDisable()
    {
        //Application.focusChanged -= OnInBackgroundChange;
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;
    }

    private void OnInBackgroundChange(bool isInBackground) 
    {
        MuteAudio(isInBackground);
        PauseGame(isInBackground);
    }

    private void MuteAudio(bool value) 
    {
        AudioListener.pause = value;
        AudioListener.volume = value ? 0f : 1f;
    }

    private void PauseGame(bool value) 
    {
        if (Time.timeScale == 0f)
            return;

        Time.timeScale = value ? 0f : 1f;
    }
}