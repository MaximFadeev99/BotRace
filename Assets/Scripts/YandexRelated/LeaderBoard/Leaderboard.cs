using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;
using LeaderboardNS;
using System;
using Lean.Localization;

public class Leaderboard : MonoBehaviour
{
    private const string LeaderboardName = "BotRaceLB";

    [SerializeField] private LeaderboardView _leaderboardView;

    private readonly List<LeaderboardPlayer> _leaderboardPlayers = new();

    private string _anonymousName = AvailableLanguages.AnonymousRu;
    private int _playerScore;

    private void OnEnable()
    {
        if (PlayerAccount.IsAuthorized)
        {
            TrySetPlayer();
            Fill();
        }
        else 
        {
            gameObject.SetActive(false);
        }     
    }

    private void TrySetPlayer() 
    {
        Agava.YandexGames.Leaderboard.GetPlayerEntry(LeaderboardName, _ => 
        {
            if (_.score < _playerScore)
                Agava.YandexGames.Leaderboard.SetScore(LeaderboardName, _playerScore);        
        });
    }

    private void Fill() 
    {
        _leaderboardPlayers.Clear();
        SetAnonymousTranslation();

        Agava.YandexGames.Leaderboard.GetEntries(LeaderboardName, result =>
        {
            for (var i = 0; i < result.entries.Length; i++)
            {
                int rank = result.entries[i].rank;
                int score = result.entries[i].score;
                string name = result.entries[i].player.publicName;

                if (string.IsNullOrEmpty(name))
                    name = _anonymousName;

                _leaderboardPlayers.Add(new(rank, name, score));
            }

            _leaderboardView.ConstructLeaderboard(_leaderboardPlayers);
        });
    }

    private void SetAnonymousTranslation() 
    {
        _anonymousName = LeanLocalization.GetFirstCurrentLanguage() switch
        {
            AvailableLanguages.English => AvailableLanguages.AnonymousEn,
            AvailableLanguages.Russian => AvailableLanguages.AnonymousRu,
            AvailableLanguages.Turkish => AvailableLanguages.AnonymousTr,
            _ => AvailableLanguages.AnonymousRu
        };
    }

    public void SetPlayerScore(string score) =>
        _playerScore = Convert.ToInt32(score);
}