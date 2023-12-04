using Agava.YandexGames;

public static class AdShower
{
    public static void Show() =>
        InterstitialAd.Show(OnOpenCallBack, OnCloseCallBack);

    private static void OnOpenCallBack() =>
        GameStopper.PauseGame();

    private static void OnCloseCallBack(bool value) 
    {
        GameStopper.ResetManualPause();
        GameStopper.TryUnpauseGame();
    }
}