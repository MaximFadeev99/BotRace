using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]

public class ContentFiller : MonoBehaviour
{
    private const int PlaceColumn = 0;
    private const int NameColumn = 1;
    private const int TimeColumn = 2;
    private const int ScoreColumn = 3;

    [SerializeField] private ResultLine _resultLine;
    [SerializeField] private List<Sprite> _places;

    private RectTransform _transform;

    private void Awake() =>
        _transform = GetComponent<RectTransform>();

    public void DrawResults(string[,] results)
    {
        int rowsCount = results.GetLength(0);

        for (int i = 0; i < rowsCount; i++) 
        {
            if (results[i,1] == null)
                continue;
            
            ResultLine newResult = Instantiate(_resultLine, _transform);
            int place = Convert.ToInt32(results[i, PlaceColumn]) - 1;
            string name = results[i, NameColumn];
            string time = results[i, TimeColumn];
            string score = results[i, ScoreColumn];

            newResult.Render(_places[place], name, time, score);
        }
    }
}