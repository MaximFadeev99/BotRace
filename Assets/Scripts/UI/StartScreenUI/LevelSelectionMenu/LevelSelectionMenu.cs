using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionMenu : MonoBehaviour
{
    [SerializeField] private ToggleGroup _levelToggles;
    [SerializeField] private Toggle _customLevelToggle;
    [SerializeField] private CustomLevelSettingsMenu _customLevelSettingsMenu;

    public Action<bool> Closed;

    public void LaunchLevel() 
    {
        Toggle activeToggle = _levelToggles.GetFirstActiveToggle();

        if (activeToggle == null)
            return;

        if (activeToggle == _customLevelToggle)
        {
            _customLevelSettingsMenu.gameObject.SetActive(true);
        }
        else 
        {
            string sceneName = activeToggle.GetComponent<LevelToggle>().SceneName;
            SceneManager.LoadScene(sceneName);
        }       
    }

    public void OnCloseButtonClick() 
    {
        Closed?.Invoke(true);
        gameObject.SetActive(false);
    }
}