using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class RaceManager : MonoBehaviour
{
    private const string HoverVolume = nameof(HoverVolume);
    private const int HoverMuteValue = -80;
    private const int HoverUnmuteValue = -13;
    private const int ResultsColumnCount = 4;
    private const int PlaceColumn = 0;
    private const int NameColumn = 1;
    private const int TimeColumn = 2;
    private const int ScoreColumn = 3;

    [SerializeField] private List<Hover> _participants;
    [SerializeField] private GameObject _resultsMenu;
    [SerializeField] private ContentFiller _contentFiller;
    [SerializeField] private FinishLine _finishLine;
    [SerializeField] private Leaderboard _leaderboard;
    [SerializeField] private CountDownHandler _countDownHandler;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private TextMeshProUGUI _screenTimer;
    [SerializeField] private float _screenTimerSpeedUpRate;
    [SerializeField] private float _trackAverageTime;
    [SerializeField] private int _trackLengthLevel;
    [SerializeField] private int _trackDifficultyLevel;

    private ScoreCalculator _scoreCalculator;
    private RaceTimer _raceTimer;
    private string[,] _results;
    private int _vacantRow = 0;

    private void Awake()
    {
        CheckDataAbsence();

        _results = new string[_participants.Count, ResultsColumnCount];
        _scoreCalculator = new(_trackAverageTime, _trackLengthLevel, _trackDifficultyLevel);
        _raceTimer = new(_screenTimer, _screenTimerSpeedUpRate);

        GameStopper.TryUnpauseGame();
    }

    private void OnEnable() =>
        _finishLine.Crossed += OnFinishLineCrossed;

    private void OnDisable() =>
        _finishLine.Crossed -= OnFinishLineCrossed;

    private void Start()
    {
        _audioMixer.SetFloat(HoverVolume, HoverUnmuteValue);
        _countDownHandler.StartRace(_participants, _raceTimer);
    }

    private void Update() =>
        _raceTimer.Tick();


    private void CheckDataAbsence()
    {
        if (_finishLine == null)
            throw new ArgumentNullException(nameof(_finishLine),
                "Assign a finish line manually through the serialized field or use Initialize method");

        if (_trackAverageTime <= 0f)
            throw new ArgumentOutOfRangeException(nameof(_finishLine),
                "Assign a track time manually through the serialized field or use Initialize method");

        if (_trackLengthLevel <= 0)
            _trackLengthLevel = CustomLevelConfiguration.TrackLength;

        if (_trackDifficultyLevel <= 0)
            _trackDifficultyLevel = CustomLevelConfiguration.DifficultyLevel;
    }

    private void OnFinishLineCrossed(Hover hover)
    {
        float score = _scoreCalculator.CalculateScore(_raceTimer.CurrentTime, _vacantRow + 1);
        _results[_vacantRow, PlaceColumn] = (_vacantRow + 1).ToString();
        _results[_vacantRow, NameColumn] = hover.gameObject.name;
        _results[_vacantRow, TimeColumn] = _raceTimer.CurrentOutputTime;
        _results[_vacantRow, ScoreColumn] = Mathf.RoundToInt(score).ToString();

        if (hover.gameObject.TryGetComponent(out UserInputHandler _))
        {
            _leaderboard.SetPlayerScore(_results[_vacantRow, ScoreColumn]);
            hover.Stopped += FinishRace;
            _finishLine.StartFireworks();
        }

        _vacantRow++;
        hover.EndRacing();
    }

    private void FinishRace(Hover player)
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        _raceTimer.Deactivate();
        _resultsMenu.SetActive(true);
        _contentFiller.DrawResults(_results);
        _audioMixer.SetFloat(HoverVolume, HoverMuteValue);
        Time.timeScale = 0f;
        SaveSystem.LevelAccomplished(currentSceneName);
        player.Stopped -= FinishRace;
    }

    public void Initialize(FinishLine finishLine, List<Hover> participants, float trackAverageTime,
        int trackLengthLevel, int trackDifficultyLevel)
    {
        _finishLine = finishLine;
        _trackAverageTime = trackAverageTime;
        _participants = participants;
        _trackLengthLevel = trackLengthLevel;
        _trackDifficultyLevel = trackDifficultyLevel;
    }
}