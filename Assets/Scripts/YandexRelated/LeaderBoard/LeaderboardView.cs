using System.Collections.Generic;
using UnityEngine;
using LeaderboardNS;

public class LeaderboardView : MonoBehaviour  
{
    [SerializeField] private Transform _container;
    [SerializeField] private LeaderboardElement _leaderboardElementPrefab;

    private List<LeaderboardElement> _spawnedElements = new();

    public void ConstructLeaderboard(List<LeaderboardPlayer> leaderboardPlayers) 
    {
        ClearLeaderboard();
        
        foreach (LeaderboardPlayer player in leaderboardPlayers) 
        {
            LeaderboardElement newLeaderboardElement = Instantiate (_leaderboardElementPrefab, _container);
            newLeaderboardElement.Initialize(player.Name, player.Rank, player.Score);
            _spawnedElements.Add(newLeaderboardElement);
        } 
    }

    private void ClearLeaderboard()
    {
       foreach (LeaderboardElement element in _spawnedElements) 
            Destroy(element.gameObject);

        _spawnedElements = new();
    }
}