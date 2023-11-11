using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardElement : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameField;
    [SerializeField] private TMP_Text _rankField;
    [SerializeField] private TMP_Text _scoreField;

    public void Initialize(string name, int rank, int score)
    {
        _nameField.text = name;
        _rankField.text = rank.ToString();
        _scoreField.text = score.ToString();
    }
}
