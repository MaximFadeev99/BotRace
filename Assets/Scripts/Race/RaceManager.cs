using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RaceManager : MonoBehaviour
{
    private const int resultsColumnCount = 4;

    [SerializeField] private GameObject _resultsMenu;
    [SerializeField] private ContentFiller _contentFiller;
    [SerializeField] private float _trackAverageTime;
    [SerializeField] private List<Hover> _participants;
    [SerializeField] private FinishLine _finishLine;
    [SerializeField] private TextMeshProUGUI _screenTimer;
    [SerializeField] private TextMeshProUGUI _startCountDownField;

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
        StartCoroutine(StartCountDown());
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
        _vacantRow++;

        if (hover.gameObject.TryGetComponent(out UserInputHandler _))
            FinishRace();
    }

    private void FinishRace() 
    {
        _raceTimer.Deactivate();
        //_resultsMenu.SetActive(true);
        _resultsMenu.SetActive(true);
        _contentFiller.DrawResults(_results);
        Time.timeScale = 0f;
    }

    private IEnumerator StartCountDown() 
    {
        var waitTime = new WaitForSeconds(0.8f);
        Time.timeScale = 1f;
        _startCountDownField.gameObject.SetActive(true);
        _startCountDownField.text = "3";
        yield return waitTime;
        _startCountDownField.text = "2";
        yield return waitTime;
        _startCountDownField.text = "1";
        yield return waitTime;
        _startCountDownField.text = "GO!";

        foreach (Hover participant in _participants)
            participant.StartRacing();

        _raceTimer.Activate();
        yield return waitTime;
        _startCountDownField.gameObject.SetActive(false);
    }
}
