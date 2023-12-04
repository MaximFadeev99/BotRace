using Lean.Localization;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingMenuManager : MonoBehaviour
{
    private const string MasterVolume = nameof(MasterVolume);
    private const int MuteValue = -80;
    private const int UnmuteValue = 0;

    [SerializeField] private Toggle _english;
    [SerializeField] private Toggle _russian;
    [SerializeField] private Toggle _turkish;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Image _soundImage;
    [SerializeField] private List<Sprite> _soundSprites;
    [SerializeField] private Button _closeButton;

    private bool _isSoundMute;

    public Action<bool> Closed; 

    private void OnEnable() =>
        SelectLanguageToggle();

    private void SelectLanguageToggle() 
    {
        string currentLanguage = LeanLocalization.GetFirstCurrentLanguage();
        Toggle targetToggle = currentLanguage switch
        {
            AvailableLanguages.English => _english,
            AvailableLanguages.Russian => _russian,
            AvailableLanguages.Turkish => _turkish,
            _ => throw new NotImplementedException
            ("The language currently set by LeanLocalization is not implemented"),
        };

        targetToggle.isOn = true;     
    }

    public void MuteVolume()
    {
        if (_isSoundMute)
        {
            _audioMixer.SetFloat(MasterVolume, UnmuteValue);
            _soundImage.sprite = _soundSprites[1];
            _isSoundMute = !_isSoundMute;
        }
        else
        {
            _audioMixer.SetFloat(MasterVolume, MuteValue);
            _soundImage.sprite = _soundSprites[0];
            _isSoundMute = !_isSoundMute;
        }
    }

    public void Close() 
    {
        Closed?.Invoke(true);
        gameObject.SetActive(false);
    }
}