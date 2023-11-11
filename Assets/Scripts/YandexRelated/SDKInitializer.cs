using UnityEngine;
using Agava.YandexGames;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Source.Yandex 
{
    public sealed class SDKInitializer : MonoBehaviour
    {
        private void Awake()
        {
            YandexGamesSdk.CallbackLogging = true;
        }

        private IEnumerator Start() 
        {
            yield return YandexGamesSdk.Initialize(OnInitialized);
        
        }

        private void OnInitialized()
        {
            SceneManager.LoadScene("StartGameMenu"); //создать статический класс со всеми названиями сцен
        }
    }
}

