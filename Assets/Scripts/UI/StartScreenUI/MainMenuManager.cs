using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Agava.YandexGames;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private GameObject _settingsMenu;

    //private void Awake()  //раскомментировать перед сборкой, выдает ошибку при тестированиии в редакторе
    //{
    //    YandexGamesSdk.GameReady();
    //}

    private void OnEnable()
    {
        _playButton.onClick.AddListener(LoadGame);
        _settingsButton.onClick.AddListener(ShowSettingsMenu);
        _exitButton.onClick.AddListener(ExitGame);   
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(LoadGame);
        _settingsButton.onClick.RemoveListener(ShowSettingsMenu);
        _playButton.onClick.RemoveListener(ExitGame);
    }

    private void LoadGame() 
    {
        SceneManager.LoadScene("Level1");
    }

    private void ShowSettingsMenu() 
    {
         _settingsMenu.SetActive(!_settingsMenu.activeSelf);
    }

    private void ExitGame() 
    {
        Application.Quit();
    }


}
