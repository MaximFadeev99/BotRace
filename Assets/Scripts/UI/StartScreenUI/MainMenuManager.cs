using UnityEngine;
using UnityEngine.UI;
using Agava.YandexGames;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _tutorialButton;
    [SerializeField] private SettingMenuManager _settingsMenu;
    [SerializeField] private TutorialPanel _tutorialPanel;
    [SerializeField] private LevelSelectionMenu _levelSelectionMenu;

    private void Awake() 
    {
        YandexGamesSdk.GameReady();
        SaveSystem.Load();
    }

    private void OnEnable()
    {
        _settingsMenu.Closed += SetButtonsActive;
        _levelSelectionMenu.Closed += SetButtonsActive;
        _tutorialPanel.Closed += SetButtonsActive;
        _playButton.onClick.AddListener(ShowLevelSelectionMenu);
        _settingsButton.onClick.AddListener(ShowSettingsMenu);
        _tutorialButton.onClick.AddListener(ShowTutorialPanel);
    }

    private void OnDisable()
    {
        _settingsMenu.Closed -= SetButtonsActive;
        _levelSelectionMenu.Closed -= SetButtonsActive;
        _tutorialPanel.Closed -= SetButtonsActive;
        _playButton.onClick.RemoveListener(ShowLevelSelectionMenu);
        _settingsButton.onClick.RemoveListener(ShowSettingsMenu);
        _tutorialButton.onClick.RemoveListener(ShowTutorialPanel);
    }

    private void ShowLevelSelectionMenu() 
    {
        SaveSystem.Load();
        _levelSelectionMenu.gameObject.SetActive(!_levelSelectionMenu.gameObject.activeSelf);
        SetButtonsActive(false);
    }

    private void ShowSettingsMenu() 
    {
        _settingsMenu.gameObject.SetActive(!_settingsMenu.gameObject.activeSelf);
        SetButtonsActive(false); 
    }

    private void ShowTutorialPanel() 
    {
        _tutorialPanel.gameObject.SetActive(!_tutorialPanel.gameObject.activeSelf);
        SetButtonsActive(false);  
    }

    private void SetButtonsActive(bool isActive) 
    {
        _playButton.interactable = isActive;
        _settingsButton.interactable = isActive;
        _tutorialButton.interactable = isActive;
    }
}