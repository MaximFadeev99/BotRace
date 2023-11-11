using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;
using LeaderboardNS;
using System;

public class Leaderboard : MonoBehaviour
{
    private const string AnonymousName = "Anonymous";
    private const string LeaderboardName = "LeaderboardName";

    [SerializeField] private LeaderboardView _leaderboardView;

    private readonly List<LeaderboardPlayer> _leaderboardPlayers = new();

    private int _playerScore;

    private void OnEnable()
    {
        if (PlayerAccount.IsAuthorized)
        {
            SetPlayer();
            Fill();
        }
        else 
        {
            gameObject.SetActive(false);
        }     
    }

    private void SetPlayer() 
    {
        //if (PlayerAccount.IsAuthorized == false)
        //    return;

        Agava.YandexGames.Leaderboard.GetPlayerEntry(LeaderboardName, _ => 
        {
            Agava.YandexGames.Leaderboard.SetScore(LeaderboardName, _playerScore);        
        });
    }

    private void Fill() 
    {
        _leaderboardPlayers.Clear();

        //if (PlayerAccount.IsAuthorized == false)
        //    return;

        Agava.YandexGames.Leaderboard.GetEntries(LeaderboardName, result =>
        {
            for (var i = 0; i < result.entries.Length; i++) 
            {
                int rank = result.entries[i].rank;
                int score = result.entries[i].score;
                string name = result.entries[i].player.publicName;

                if (string.IsNullOrEmpty(name))
                    name = AnonymousName;

                _leaderboardPlayers.Add(new(rank, name, score));             
            }

            _leaderboardView.ConstructLeaderboard(_leaderboardPlayers);
        });   
    }

    public void SetPlayerScore(string score) =>
        _playerScore = Convert.ToInt32(score);
}
