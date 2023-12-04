using UnityEngine;
using UnityEngine.UI;

public class LevelToggle : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    [SerializeField] private GameObject _inactiveBackground;

    private Toggle _toggle;

    public string SceneName => _sceneName;

    private void Awake() =>
        _toggle = GetComponent<Toggle>();

    private void OnEnable()
    {
        bool isLevelAccesable = SaveSystem.VerifyAccesstoLevel(_sceneName);

        SetActive(isLevelAccesable);
    }

    public void SetActive(bool isActive) 
    {
        _toggle.interactable = isActive;
        _inactiveBackground.SetActive(!isActive);
    }
}