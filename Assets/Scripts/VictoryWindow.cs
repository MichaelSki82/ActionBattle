using System;
using UnityEngine;
using UnityEngine.UI;

public class VictoryWindow : MonoBehaviour
{
    public event Action NewGameButtonPressed;

    [SerializeField] private Button _newGameButton;


    private void OnEnable()
    {
        _newGameButton.onClick.AddListener(OnNewGameButtonPressed);
    }

    private void OnDisable()
    {
        _newGameButton.onClick.RemoveListener(OnNewGameButtonPressed);
    }

    public void SetActive(bool isOn)
    {
        gameObject.SetActive(isOn);
    }

    private void OnNewGameButtonPressed()
    {
        
        NewGameButtonPressed?.Invoke();
    }
}
