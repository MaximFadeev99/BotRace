using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Agava.YandexGames;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private SettingMenuManager _settingsMenu;

    private void Awake()
    {
        //YandexGamesSdk.GameReady();
    }

    private void OnEnable()
    {
        _settingsMenu.Closed += SetButtonsActive;
        _playButton.onClick.AddListener(LoadGame);
        _settingsButton.onClick.AddListener(ShowSettingsMenu);
        _exitButton.onClick.AddListener(ExitGame);   
    }

    private void OnDisable()
    {
        _settingsMenu.Closed -= SetButtonsActive;
        _playButton.onClick.RemoveListener(LoadGame);
        _settingsButton.onClick.RemoveListener(ShowSettingsMenu);
        _playButton.onClick.RemoveListener(ExitGame);
    }

    private void LoadGame() =>
        SceneManager.LoadScene(SceneNames.Level1);

    private void ShowSettingsMenu() 
    {
        _settingsMenu.gameObject.SetActive(!_settingsMenu.gameObject.activeSelf);
        SetButtonsActive(false); 
    }

    private void SetButtonsActive(bool isActive) 
    {
        _playButton.interactable = isActive;
        _settingsButton.interactable = isActive;
        _exitButton.interactable = isActive;
    }

    private void ExitGame() =>
        Application.Quit();
}