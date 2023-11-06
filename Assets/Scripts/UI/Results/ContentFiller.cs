using System;
using System.Collections.Generic;
using UnityEngine;

public class ContentFiller : MonoBehaviour
{
    private const int ColumnCount = 4;

    [SerializeField] private ResultLine _resultLine;
    [SerializeField] private List<Sprite> _places;

    private RectTransform _transform;

    private void Awake()
    {
        _transform = GetComponent<RectTransform>();
    }

    public void DrawResults(string[,] results)
    {
        int rowsCount = results.GetLength(0);
        int columnsCount = results.GetLength(1);

        if (columnsCount != ColumnCount)
            throw new ArgumentOutOfRangeException(nameof(columnsCount), 
                $"The size of a source array for drawing results must consist of {ColumnCount} columns");

        for (int i = 0; i < rowsCount; i++) 
        {
            ResultLine newResult = Instantiate(_resultLine, _transform);
            int place = Convert.ToInt32(results[i, 0]) - 1;
            string name = results[i, 1];
            string time = results[i, 2];
            string score = results[i, 3];

            newResult.Render(_places[place], name, time, score);
        }
    }

}
