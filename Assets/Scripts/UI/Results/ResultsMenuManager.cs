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
    [SerializeField] private GameObject _authorizationWarning;

    private void OnEnable()
    {
        _pauseButton.interactable = false;
        _exitButton.onClick.AddListener(LoadStartScreen);
        _replayButton.onClick.AddListener(ReloadLevel);
        _showLeadersButton.onClick.AddListener(TryAuthorize);
        StartCoroutine(ShowPanel(_authorizationWarning, 10f));
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
            StartCoroutine(ShowPanel(_failedAuthorizationWindow, 3f));
        }
        else 
        {
            PlayerAccount.RequestPersonalProfileDataPermission();      
            _leaderboard.gameObject.SetActive(true);
        }
    }

    private IEnumerator ShowPanel(GameObject panel, float showTime) 
    {
        var waitTime = new WaitForSecondsRealtime(showTime);
        panel.SetActive(true);
        yield return waitTime;
        panel.SetActive(false);
    }
}