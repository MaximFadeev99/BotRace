using System.Collections.Generic;
using UnityEngine;

public class BonusGenerator : MonoBehaviour
{
    private const string Bonus1 = nameof(Bonus1);
    private const string Bonus2 = nameof(Bonus2);
    
    [SerializeField] private List<Bonus> _bonuses;
    [SerializeField] private float _bonusRate = 0.05f;

    private readonly List<Transform> _bonusPlaces = new();

    private Bonus GetRandomBonus() 
    {
        return _bonuses[Random.Range(0, _bonuses.Count)];
    }

    private Transform GetVacantBonusPlace() 
    {
        Transform nextPlace;

        do 
        {
            nextPlace = _bonusPlaces[Random.Range(0, _bonusPlaces.Count)];
            _bonusPlaces.Remove(nextPlace);
        }
        while (nextPlace == null);
        
        return nextPlace;
    }

    public void GenerateBonuses(IReadOnlyList<GameObject> trackParts) 
    {
        for (int i = 1; i < trackParts.Count; i++) 
        {
            _bonusPlaces.Add(trackParts[i].transform.Find(Bonus1));
            _bonusPlaces.Add(trackParts[i].transform.Find(Bonus2));
        }

        int bonusCount = Mathf.RoundToInt(_bonusPlaces.Count * _bonusRate);

        for (int i = 0; i < bonusCount; i++) 
        {
            Transform nextBonusPlace = GetVacantBonusPlace();
            Bonus nextBonus = GetRandomBonus();
            nextBonus = Instantiate(nextBonus, nextBonusPlace.position, nextBonusPlace.rotation);
            nextBonus.gameObject.isStatic = true;
        }   
    }
}