using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public event Action RetryButtonPressed;
    public event Action NextLevelButtonPressed;
    public event Action ResturtGameButtonPressed;
    public event Action StartGameButtonPressed;

    [SerializeField] private AudioSource _buttonClickSource;
    [SerializeField] private AudioSource _backgroundSourse;
    
    [SerializeField] private TMP_Text _hpText;
    [SerializeField] private TMP_Text _hurdLevelText;
    [SerializeField] private TMP_Text _ScoreText;

    [SerializeField] private LoseWindow _loseWindow;
    [SerializeField] private VictoryWindow _victoryWindow;
    [SerializeField] private GameObject _photonMenuWindow;
    [SerializeField] private TMP_Text _loadingText;
    [SerializeField] private Button _startGameButton;
     public TMP_InputField _roomNameField;

    [SerializeField] private GameObject _mainSceneWindow;
    [SerializeField] private Button _optionsButton;

    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private Button _resturtGameButton;
    [SerializeField] private Button _backMenuButton;
    [SerializeField] private Button _musicOnButton;
    [SerializeField] private Button _musicOffButton;

    public bool _gameIsPoused = true;
    public string _roomName;
    private bool _backGroundMusicIsPlayed = true;
    

    private void Awake()
    {
        Time.timeScale = 0f;
        _roomNameField.onValueChanged.AddListener(SetRoomName);
        _loseWindow = GetComponentInChildren<LoseWindow>(true);
        _victoryWindow = GetComponentInChildren<VictoryWindow>(true);
        _mainMenu.SetActive(false);
        _mainSceneWindow.SetActive(false);
        _loadingText.gameObject.SetActive(false);
        _optionsButton.onClick.AddListener(OnOptionButtonPressed);
        _backMenuButton.onClick.AddListener(OnBackMenuButtonPressed);
        _musicOnButton.onClick.AddListener(OnMusicOnButtonPressed);
        _musicOffButton.onClick.AddListener(OnMusicOffButtonPressed);
        _resturtGameButton.onClick.AddListener(OnResturtGameButtonPressed);
        _startGameButton.onClick.AddListener(OnStartGameButtonPressed);
    }


    
    public void SetLoadingText(bool isOn)
    {
        _loadingText.gameObject.SetActive(isOn);
    }
    public void SetMainSceneWindow(bool isOn)
    {
        _mainSceneWindow.gameObject.SetActive(isOn);
    }
    public void SetPhotonMenu(bool isOn)
    {
        _photonMenuWindow.gameObject.SetActive(isOn);
    }
    private void OnStartGameButtonPressed()
    {

        StartGameButtonPressed?.Invoke();
       
    }
    private void SetRoomName(string value)
    {
        _roomName = value;
    }
    
    private void OnResturtGameButtonPressed()
    {
        RestartGame();
    }

    private void OnEnable()
    {
        _loseWindow.RetryButtonPressed += OnRetryButtonPressed;
        _victoryWindow.NextLeveleButtonPressed += OnNextLevelButtonPressed;
        _victoryWindow.ResturtGameButtonPressed += OnResturtGameButtonPressed;
    }

    public void ButtonSoundPlay()
    {
        _buttonClickSource.Play();
    }

    private void OnNextLevelButtonPressed()
    {
        NextLevelButtonPressed?.Invoke();
        ButtonSoundPlay();
    }

    private void OnRetryButtonPressed()
    {
        RetryButtonPressed?.Invoke();
        ButtonSoundPlay();
    }

    private void Start()
    {
        _loseWindow.SetActive(false);
        _victoryWindow.SetActive(false);
    }


    public void RestartGame()
    {
        ButtonSoundPlay();
        _mainMenu.SetActive(false);
        Time.timeScale = 1f;
        _gameIsPoused = false;
        Stats.ResetAllStats();
        SceneManager.LoadScene(1);
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
        ButtonSoundPlay();
        if (!_backGroundMusicIsPlayed)
        {
            _backgroundSourse.Play();
            _backGroundMusicIsPlayed = true;
        }
    }

    private void OnMusicOffButtonPressed()
    {
        ButtonSoundPlay();
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
        _victoryWindow.NextLeveleButtonPressed -= OnNextLevelButtonPressed;
        _victoryWindow.ResturtGameButtonPressed -= OnResturtGameButtonPressed;
        _resturtGameButton.onClick.AddListener(OnResturtGameButtonPressed);
        _startGameButton.onClick.RemoveListener(OnStartGameButtonPressed);
        _roomNameField.onValueChanged.RemoveListener(SetRoomName);
    }
}