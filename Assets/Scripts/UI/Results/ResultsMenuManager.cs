using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Agava.YandexGames;

public class ResultsMenuManager : MonoBehaviour
{
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _replayButton;
    [SerializeField] private Button _showLeadersButton;
    [SerializeField] private GameObject _failedAuthorizationWindow;
    [SerializeField] private Leaderboard _leaderboard;

    private void OnEnable()
    {
        _pauseButton.interactable = false;
        _exitButton.onClick.AddListener(LoadStartScreen);
        _replayButton.onClick.AddListener(ReloadLevel);
        _showLeadersButton.onClick.AddListener(TryAuthorize);
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(LoadStartScreen);
        _replayButton.onClick.RemoveListener(ReloadLevel);
        _showLeadersButton.onClick.RemoveListener(TryAuthorize);
    }

    private void LoadStartScreen() 
    {
        AdShower.Show();
        SceneManager.LoadScene(SceneNames.StartGameMenu);
    }

    private void ReloadLevel() 
    {
        AdShower.Show();
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    private void TryAuthorize() 
    {
        if (PlayerAccount.IsAuthorized == false)
            PlayerAccount.Authorize();

        if (PlayerAccount.IsAuthorized == false)
        {
            StartCoroutine(ShowFailedAuthorizationWindow());
        }
        else 
        {
            PlayerAccount.RequestPersonalProfileDataPermission();      
            _leaderboard.gameObject.SetActive(true);
        }
    }

    private IEnumerator ShowFailedAuthorizationWindow() 
    {
        var waitTime = new WaitForSeconds(3f);
        _failedAuthorizationWindow.SetActive(true);
        yield return waitTime;
        _failedAuthorizationWindow.SetActive(false);
    }
}