using UnityEngine;
using Agava.YandexGames;
using System.Collections;
using UnityEngine.SceneManagement;
using Lean.Localization;

namespace Source.Yandex 
{
    public sealed class SDKInitializer : MonoBehaviour
    {
        private void Awake() =>
            YandexGamesSdk.CallbackLogging = true;

        private IEnumerator Start()
        {
            yield return YandexGamesSdk.Initialize(OnInitialized);
        }

        private void OnInitialized() 
        {
            ChangeLanguage();
            SaveSystem.Load();
            SceneManager.LoadScene(SceneNames.StartGameMenu);       
        }

        private void ChangeLanguage() 
        {
            string languageCode = YandexGamesSdk.Environment.i18n.lang;
            string usedLanguage = languageCode switch
            {
                AvailableLanguages.RussianCode => AvailableLanguages.Russian,
                AvailableLanguages.EnglishCode => AvailableLanguages.English,
                AvailableLanguages.TurkishCode => AvailableLanguages.Turkish,
                _ => AvailableLanguages.Russian
            };

            LeanLocalization.SetCurrentLanguageAll(usedLanguage);
            Debug.Log(YandexGamesSdk.Environment.i18n.lang + " " +  usedLanguage);
        }
    }
}