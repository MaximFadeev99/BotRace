using UnityEngine;
using Agava.YandexGames;

public static class AdShower
{
    public static void Show() =>
        VideoAd.Show(OnOpenCallBack, null, OnCloseCallBack);

    private static void OnOpenCallBack() 
    {
        Time.timeScale = 0f;
        AudioListener.volume = 0f;
    }

    private static void OnCloseCallBack() 
    {
        Time.timeScale = 1f;
        AudioListener.volume = 1f;
    }
}