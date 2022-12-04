using System;
using UnityEngine;
using UnityEngine.UI;

public class LoseWindow : MonoBehaviour
{
    public event Action RetryButtonPressed;

    [SerializeField] private Button _retryButton;

    private void OnEnable()
    {
        _retryButton.onClick.AddListener(OnRetryButtonPressed);
    }

    private void OnDisable()
    {
        _retryButton.onClick.RemoveListener(OnRetryButtonPressed);
    }

    public void SetActive(bool isOn)
    {
        gameObject.SetActive(isOn);
    }

    private void OnRetryButtonPressed()
    {
        RetryButtonPressed?.Invoke();
    }
}

