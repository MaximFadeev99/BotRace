using Lean.Localization;
using System;
using UnityEngine;

[RequireComponent(typeof(LeanPhrase))]
public class LeanLocalizedGameObjectName : MonoBehaviour
{
    [SerializeField] private LeanLocalization _leanLocalization;

    private LeanPhrase _leanPhrase;

    private void Awake() =>
        _leanPhrase = GetComponent<LeanPhrase>();


    private void OnEnable() =>
        LeanLocalization.OnLanguageChanged += ChangeName;       


    private void OnDisable() =>
        LeanLocalization.OnLanguageChanged -= ChangeName;

    private void Start() =>
        ChangeName(LeanLocalization.GetFirstCurrentLanguage());

    private void ChangeName(string newLanguage) 
    {
        int entryIndex = newLanguage switch
        {
            AvailableLanguages.English => AvailableLanguages.EnglishEntryIndex,
            AvailableLanguages.Russian => AvailableLanguages.RussianEntryIndex,
            AvailableLanguages.Turkish => AvailableLanguages.TurkishEntryIndex,
            _ => throw new NotImplementedException("The language currently set by LeanLocalization is not implemented"),
        };
        
        LeanPhrase.Entry entry = _leanPhrase.Entries[entryIndex];
        gameObject.name = entry.Text;
    }
}