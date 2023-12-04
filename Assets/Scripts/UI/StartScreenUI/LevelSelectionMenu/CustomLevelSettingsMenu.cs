using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomLevelSettingsMenu : MonoBehaviour
{
    [SerializeField] private Slider _trackLengthSlider;
    [SerializeField] private Slider _trackDifficultySlider;

    private void Awake()
    {
        _trackLengthSlider.value = CustomLevelConfiguration.TrackLength;
        _trackDifficultySlider.value = CustomLevelConfiguration.DifficultyLevel;
    }

    private void OnEnable()
    {
        _trackLengthSlider.onValueChanged.AddListener(SetTrackLength);
        _trackDifficultySlider.onValueChanged.AddListener(SetTrackDifficulty);
    }

    private void OnDisable()
    {
        _trackLengthSlider.onValueChanged.RemoveListener(SetTrackLength);
        _trackDifficultySlider.onValueChanged.RemoveListener(SetTrackDifficulty);
    }

    private void SetTrackLength(float newValue) =>
        CustomLevelConfiguration.TrackLength = Mathf.RoundToInt(newValue);

    private void SetTrackDifficulty(float newValue) => 
        CustomLevelConfiguration.DifficultyLevel = Mathf.RoundToInt(newValue);

    public void LaunchCustomLevel() =>
        SceneManager.LoadScene(SceneNames.CustomLevel);
}