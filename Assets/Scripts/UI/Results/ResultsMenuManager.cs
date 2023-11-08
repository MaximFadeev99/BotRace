using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultsMenuManager : MonoBehaviour
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _replayButton;

    private void OnEnable()
    {
        _exitButton.onClick.AddListener(LoadStartScreen);
        _replayButton.onClick.AddListener(ReloadLevel);
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(LoadStartScreen);
        _replayButton.onClick.RemoveListener(ReloadLevel);
    }

    private void LoadStartScreen() 
    {
        //Time.timeScale = 1.0f;
        SceneManager.LoadScene("StartGameMenu");
    }

    private void ReloadLevel() 
    {
        Scene currentScene = SceneManager.GetActiveScene();

        //Time.timeScale = 1.0f;
        SceneManager.LoadScene(currentScene.name);
    }
}
