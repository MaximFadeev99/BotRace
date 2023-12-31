using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenuPanel;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private ConfirmationWindow _confirmationWindow;

    private void OnEnable()
    {
        _pauseButton.onClick.AddListener(OnPauseButtonClick);
        _confirmationWindow.NoButton.onClick.AddListener(() =>
        { _confirmationWindow.gameObject.SetActive(false); });
    }

    private void OnDisable()
    {
        _pauseButton.onClick.RemoveListener(OnPauseButtonClick);
        _confirmationWindow.NoButton.onClick.RemoveAllListeners();
    }

    private void OnPauseButtonClick() 
    {
        if (_pauseMenuPanel.activeSelf == false)
            PauseGame();
        else 
            ResumeGame();
    }

    private void ReloadScene() 
    {
        _confirmationWindow.YesButton.onClick.RemoveListener(ReloadScene);
        AdShower.Show();
        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.name);
    }

    private void LoadStartScreen() 
    {
        _confirmationWindow.YesButton.onClick.RemoveListener(LoadStartScreen);
        AdShower.Show();
        SceneManager.LoadScene(SceneNames.StartGameMenu);
    }

    private void ResumeGame() 
    {
        _pauseMenuPanel.SetActive(false);
        GameStopper.ResetManualPause();
        GameStopper.TryUnpauseGame();
    }

    public void PauseGame() 
    {
        _pauseMenuPanel.SetActive(true);
        GameStopper.PauseGame();
    }

    public void InquireReloadConfirmation()
    {
        _confirmationWindow.gameObject.SetActive(true);
        _confirmationWindow.YesButton.onClick.AddListener(ReloadScene);
    }

    public void InquireExitConfirmation()
    {
        _confirmationWindow.gameObject.SetActive(true);
        _confirmationWindow.YesButton.onClick.AddListener(LoadStartScreen);
    }
}