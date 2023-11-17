using Agava.WebUtility;
using UnityEngine;

public class FocusObserver : MonoBehaviour
{
    private void OnEnable()
    {
        //Application.focusChanged += OnFocusChanged; //два способа изменения звука и остановки времени выполянются последовательно,
                                                        //в результате звук и время всегда на нуля
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;
    }

    private void OnDisable()
    {
        //Application.focusChanged -= OnFocusChanged;
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;
    }

    private void OnFocusChanged(bool isInBackground) 
    {
        MuteAudio(isInBackground);
        PauseGame(isInBackground);
    }

    private void OnInBackgroundChange(bool isInBackground) 
    {
        MuteAudio(isInBackground);
        PauseGame(isInBackground);
    }

    private void MuteAudio(bool isMuting) 
    {
        AudioListener.pause = isMuting;
        AudioListener.volume = isMuting ? 0f : 1f;
    }

    private void PauseGame(bool isPausing) =>
        Time.timeScale = isPausing ? 0f : 1f;
}