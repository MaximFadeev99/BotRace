using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDownHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countDownField;
    [SerializeField] private AudioClip _beep;
    [SerializeField] private AudioClip _finalBeep;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private IEnumerator StartCountDown(List<Hover> participants, RaceTimer raceTimer)
    {
        var waitTime = new WaitForSeconds(0.8f);
        Time.timeScale = 1f;
        _countDownField.gameObject.SetActive(true);
        _countDownField.text = "3";
        _audioSource.clip = _beep;
        _audioSource.Play();
        yield return waitTime;
        _countDownField.text = "2";
        _audioSource.Play();
        yield return waitTime;
        _countDownField.text = "1";
        _audioSource.Play();
        yield return waitTime;
        _countDownField.text = "GO!";
        _audioSource.clip = _finalBeep;
        _audioSource.Play();

        foreach (Hover participant in participants)
            participant.StartRacing();

        raceTimer.Activate();
        yield return waitTime;
        _countDownField.gameObject.SetActive(false);
    }

    public void StartRace(List<Hover> participants, RaceTimer raceTimer) => 
        StartCoroutine(StartCountDown(participants, raceTimer));
}
