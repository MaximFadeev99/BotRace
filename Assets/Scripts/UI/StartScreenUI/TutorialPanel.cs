using System;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPanel : MonoBehaviour
{
    [SerializeField] private Button _closeButton;
    
    public Action<bool> Closed;

    public void OnEnable() =>
        _closeButton.onClick.AddListener(OnCloseButtonClick);

    public void OnDisable()=>
        _closeButton.onClick.RemoveListener(OnCloseButtonClick);

    public void OnCloseButtonClick() 
    {
        Closed?.Invoke(true);
        gameObject.SetActive(false);
    }
}