using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultLine : MonoBehaviour
{
    [SerializeField] private Image _place;
    [SerializeField] private TextMeshProUGUI _racer;
    [SerializeField] private TextMeshProUGUI _time;
    [SerializeField] private TextMeshProUGUI _score;

    public void Render(Sprite place, string racer, string time, string score) 
    {
        _place.sprite = place;
        _racer.text = racer;
        _time.text = time;
        _score.text = score;
    }
}
