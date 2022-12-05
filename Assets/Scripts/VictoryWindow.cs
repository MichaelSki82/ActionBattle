using System;
using UnityEngine;
using UnityEngine.UI;

public class VictoryWindow : MonoBehaviour
{
    public event Action NextLeveleButtonPressed;
    public event Action ResturtGameButtonPressed;

    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _resturtGameButton;


    private void OnEnable()
    {
        _nextLevelButton.onClick.AddListener(OnNewGameButtonPressed);
        _resturtGameButton.onClick.AddListener(OnResturtGameButtonPressed);
    }

    private void OnDisable()
    {
        _nextLevelButton.onClick.RemoveListener(OnNewGameButtonPressed);
        _resturtGameButton.onClick.AddListener(OnResturtGameButtonPressed);
    }

    private void OnResturtGameButtonPressed()
    {
        ResturtGameButtonPressed?.Invoke();
    }

    public void SetActive(bool isOn)
    {
        gameObject.SetActive(isOn);
    }

    private void OnNewGameButtonPressed()
    {
        
        NextLeveleButtonPressed?.Invoke();
    }
}
