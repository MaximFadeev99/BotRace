using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Agava.YandexGames;
using UnityEngine.Audio;

public class MainMenuManager : MonoBehaviour
{
    private const string StandardSnapshotName = "Standard";
    private const string MuteSnapshotName = "Mute";

    [SerializeField] private Button _playButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private AudioMixer _audioMixer;

    private AudioMixerSnapshot _muteSnapshot;
    private AudioMixerSnapshot _standardSnapshot;
    private bool _isSoundMute;

    private void Awake()
    {
        YandexGamesSdk.GameReady();
        _standardSnapshot = _audioMixer.FindSnapshot(StandardSnapshotName);
        _muteSnapshot = _audioMixer.FindSnapshot(MuteSnapshotName);
    }

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

    private void LoadGame() =>
        SceneManager.LoadScene(SceneNames.Level1);

    private void ShowSettingsMenu() =>
         _settingsMenu.SetActive(!_settingsMenu.activeSelf);

    private void ExitGame() =>
        Application.Quit();

    public void MuteVolume() 
    {
        float transitionTime = 0.2f;
        
        if (_isSoundMute)
        {
            _standardSnapshot.TransitionTo(transitionTime);
            _isSoundMute = false;
        }
        else 
        {
            _muteSnapshot.TransitionTo(transitionTime);
            _isSoundMute = true;
        }
    }
}