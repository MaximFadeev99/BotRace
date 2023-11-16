using Lean.Localization;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingMenuManager : MonoBehaviour
{
    private const string StandardSnapshotName = "Standard";
    private const string MuteSnapshotName = "Mute";

    [SerializeField] private Toggle _english;
    [SerializeField] private Toggle _russian;
    [SerializeField] private Toggle _turkish;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Image _soundImage;
    [SerializeField] private List<Sprite> _soundSprites;
    [SerializeField] private Button _closeButton;

    private AudioMixerSnapshot _muteSnapshot;
    private AudioMixerSnapshot _standardSnapshot;
    private bool _isSoundMute;

    public Action<bool> Closed; 

    private void Awake()
    {
        _standardSnapshot = _audioMixer.FindSnapshot(StandardSnapshotName);
        _muteSnapshot = _audioMixer.FindSnapshot(MuteSnapshotName);
    }

    private void OnEnable() =>
        SelectLanguageButton();

    private void SelectLanguageButton() 
    {
        string currentLanguage = LeanLocalization.GetFirstCurrentLanguage();
        Toggle targetToggle = currentLanguage switch
        {
            AvailableLanguages.English => _english,
            AvailableLanguages.Russian => _russian,
            AvailableLanguages.Turkish => _turkish,
            _ => throw new NotImplementedException("The language currently set by LeanLocalization is not implemented"),
        };

        targetToggle.isOn = true;     
    }

    public void MuteVolume()
    {
        float transitionTime = 0.2f;

        if (_isSoundMute)
        {
            _standardSnapshot.TransitionTo(transitionTime);
            _soundImage.sprite = _soundSprites[1];
            _isSoundMute = false;
        }
        else
        {
            _muteSnapshot.TransitionTo(transitionTime);
            _soundImage.sprite = _soundSprites[0];
            _isSoundMute = true;
        }
    }

    public void Close() 
    {
        Closed?.Invoke(true);
        gameObject.SetActive(false);
    }
}