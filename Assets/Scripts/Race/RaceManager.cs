using Agava.YandexGames;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    private const int resultsColumnCount = 4;

    [SerializeField] private GameObject _resultsMenu;
    [SerializeField] private ContentFiller _contentFiller;
    [SerializeField] private float _trackAverageTime;
    [SerializeField] private List<Hover> _participants;
    [SerializeField] private FinishLine _finishLine;
    [SerializeField] private Leaderboard _leaderboard;
    [SerializeField] private CountDownHandler _countDownHandler;
    [SerializeField] private TextMeshProUGUI _screenTimer;

    //[SerializeField] private LayerMask _layerMask;

    private ScoreCalculator _scoreCalculator;
    private RaceTimer _raceTimer;
    private string[,] _results;
    private int _vacantRow = 0;
    
    //private string[,] _results = new string[3, 4] { {"1", "Bob", "1.59", "1000" },
    //{"2", "Hui", "2.59", "900" },
    //{"3", "Her", "3.59", "800" },};


    private void Awake()
    {
        //_contentFiller.DrawResults(_results);
        //Debug.Log(_layerMask);
        _results = new string[_participants.Count, resultsColumnCount];
        _scoreCalculator = new(_trackAverageTime);
        _raceTimer = new(_screenTimer);
    }

    private void OnEnable()
    {
        _finishLine.Crossed += OnFinishLineCrossed;
    }

    private void OnDisable()
    {
        _finishLine.Crossed -= OnFinishLineCrossed;
    }

    private void Start()
    {
        _countDownHandler.StartRace(_participants, _raceTimer);
        //_raceTimer.Activate();
    }

    private void Update()
    {
        _raceTimer.Tick();
    }

    private void OnFinishLineCrossed(Hover hover) 
    {
        float score = _scoreCalculator.CalculateScore(_raceTimer.CurrentTime, _vacantRow + 1);
        _results[_vacantRow, 0] = (_vacantRow + 1).ToString();
        _results[_vacantRow, 1] = hover.gameObject.name;
        _results[_vacantRow, 2] = _raceTimer.CurrentOutputTime;
        _results[_vacantRow, 3] = Mathf.RoundToInt(score).ToString();

        if (hover.gameObject.TryGetComponent(out UserInputHandler _)) 
        {
            _leaderboard.SetPlayerScore(_results[_vacantRow, 3]);
            FinishRace();
        }

        _vacantRow++;
    }

    private void FinishRace() 
    {
        _raceTimer.Deactivate();
        //_resultsMenu.SetActive(true);
        _resultsMenu.SetActive(true);
        _contentFiller.DrawResults(_results);
        Time.timeScale = 0f;
    }   
}
