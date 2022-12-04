using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public event Action RetryButtonPressed;
    public event Action NewGameButtonPressed;

    [SerializeField] private AudioSource _buttonClickSource;
    [SerializeField] private AudioSource _backgroundSourse;
    
    [SerializeField] private TMP_Text _hpText;
    [SerializeField] private TMP_Text _hurdLevelText;
    [SerializeField] private TMP_Text _ScoreText;

    [SerializeField] private LoseWindow _loseWindow;
    [SerializeField] private VictoryWindow _victoryWindow;

    [SerializeField] private GameObject _mainSceneWindow;
    [SerializeField] private Button _optionsButton;

    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private Button _resturtGameButton;
    [SerializeField] private Button _backMenuButton;
    [SerializeField] private Button _musicOnButton;
    [SerializeField] private Button _musicOffButton;

    private bool _gameIsPoused = false;
    private bool _backGroundMusicIsPlayed = true;

    private void Awake()
    {
        _loseWindow = GetComponentInChildren<LoseWindow>(true);
        _victoryWindow = GetComponentInChildren<VictoryWindow>(true);
        _mainMenu.SetActive(false);
        _optionsButton.onClick.AddListener(OnOptionButtonPressed);
        _backMenuButton.onClick.AddListener(OnBackMenuButtonPressed);
        _musicOnButton.onClick.AddListener(OnMusicOnButtonPressed);
        _musicOffButton.onClick.AddListener(OnMusicOffButtonPressed);
        _resturtGameButton.onClick.AddListener(OnResturtGameButtonPressed);
    }

    private void OnResturtGameButtonPressed()
    {
        RestartGame();
    }

    private void OnEnable()
    {
        _loseWindow.RetryButtonPressed += OnRetryButtonPressed;
        _victoryWindow.NewGameButtonPressed += OnNewGameButtonPressed;
    }

    private void OnNewGameButtonPressed()
    {
        NewGameButtonPressed?.Invoke();
    }

    private void OnRetryButtonPressed()
    {
        RetryButtonPressed?.Invoke();
    }

    private void Start()
    {
        _loseWindow.SetActive(false);
        _victoryWindow.SetActive(false);
    }


    public void RestartGame()
    {
        _buttonClickSource.Play();
        _mainMenu.SetActive(false);
        Time.timeScale = 1f;
        _gameIsPoused = false;
        Stats.ResetAllStats();
        SceneManager.LoadScene(0);
    }
    public void SetLoseWindow(bool isOn)
    {
        _loseWindow.SetActive(isOn);
    }

    public void SetVictoryWindow(bool isOn)
    {
        _victoryWindow.SetActive(isOn);
    }
    
    
    private void OnMusicOnButtonPressed()
    {
        _buttonClickSource.Play();
        if (!_backGroundMusicIsPlayed)
        {
            _backgroundSourse.Play();
            _backGroundMusicIsPlayed = true;
        }
    }

    private void OnMusicOffButtonPressed()
    {
        _buttonClickSource.Play();
        if (_backGroundMusicIsPlayed)
        {
            _backgroundSourse.Stop();
            _backGroundMusicIsPlayed = false;
        }
    }


    private void OnBackMenuButtonPressed()
    {
        _buttonClickSource.Play();
        _mainMenu.SetActive(false);
        Time.timeScale = 1f;
        _gameIsPoused = false;
    }

    private void OnOptionButtonPressed()
    {
        _buttonClickSource.Play();
        _mainMenu.SetActive(true);
        Time.timeScale = 0f;
        _gameIsPoused = true;
    }


    public void UpdateScoreAndLevel()
    {
        _hurdLevelText.text = $"Level {Stats.HurdLevel}";
        _ScoreText.text = "Score: " + Stats.Score.ToString("D4");
    }

    public void UpdateHp(int hp)
    {
        _hpText.text = $"HP: {hp}";
    }

    private void OnDisable()
    {
        _optionsButton.onClick.RemoveListener(OnOptionButtonPressed);
        _backMenuButton.onClick.RemoveListener(OnBackMenuButtonPressed);
        _musicOnButton.onClick.RemoveListener(OnMusicOnButtonPressed);
        _musicOffButton.onClick.RemoveListener(OnMusicOffButtonPressed);
        _loseWindow.RetryButtonPressed -= OnRetryButtonPressed;
        _victoryWindow.NewGameButtonPressed -= OnNewGameButtonPressed;
        _resturtGameButton.onClick.AddListener(OnResturtGameButtonPressed);
    }
}