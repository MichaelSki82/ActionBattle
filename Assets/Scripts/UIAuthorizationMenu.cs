using System;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIAuthorizationMenu : MonoBehaviour
{
    public event Action OkLogInButtonPressed;
    public event Action OkSignInButtonPressed;
    public event Action BackLogInButtonPressed;
    public event Action BackSignButtonPressed;
    
    [SerializeField] private AudioSource _buttonClickSource;
    [SerializeField] private TMP_InputField _userEmailField;
    [SerializeField] private TMP_InputField _userNameField;
    [SerializeField] private TMP_InputField _userPasswordField;

    [SerializeField] private Button _okLogInInButton;
    [SerializeField] private Button _okSignInButton;
    [SerializeField] private Button _backLoginButton;
    [SerializeField] private Button _backSigInButton;

    [SerializeField] public TMP_Text _errorText;
    [SerializeField] private TMP_Text _loadingText;

    [SerializeField] private GameObject _signIn;
    [SerializeField] private GameObject _createAccount;

    private string _userEmail;
    private string _userName;
    private string _userPassword;

    private void Awake()
    {
        _userEmailField.onValueChanged.AddListener(SetUserEmail);
        _userNameField.onValueChanged.AddListener(SetUserName);
        _userPasswordField.onValueChanged.AddListener(SetUserPassword);
        _loadingText.gameObject.SetActive(false);
        _errorText.gameObject.SetActive(false);
        _okSignInButton.onClick.AddListener(OnOkSignInButtonPressed);
        _okLogInInButton.onClick.AddListener(OnOkLogInButtonPressed);
        _backLoginButton.onClick.AddListener(OnBackLogInButtonPressed);
        _backSigInButton.onClick.AddListener(OnBackSignButtonPressed);
        _signIn.gameObject.SetActive(false);
    }


    public void ButtonSoundPlay()
    {
        _buttonClickSource.Play();
    }

    public void SetActiveCreateWindow(bool isOn)
    {
        _createAccount.gameObject.SetActive(isOn);
    }
    
    public void SetActiveSigInWindow(bool isOn)
    {
        _signIn.gameObject.SetActive(isOn);
    }


    public void LogIn()
    {
        ButtonSoundPlay();
        _loadingText.gameObject.SetActive(true);
        PlayFabClientAPI.RegisterPlayFabUser(new RegisterPlayFabUserRequest()
        {
            Email = _userEmail,
            Username = _userName,
            Password = _userPassword,
            RequireBothUsernameAndEmail = true
        }, result =>
        {
            Debug.Log($"Success: {_userName}");
            _loadingText.gameObject.SetActive(false);
            _errorText.gameObject.SetActive(false);
            _createAccount.gameObject.SetActive(false);
            _signIn.gameObject.SetActive(true);
            //SceneManager.LoadScene(1);
        }, error =>
        {
            Debug.LogError($"Fail: {error}");
            _errorText.gameObject.SetActive(true);
            _errorText.text = error.ToString();
        });
    }

    private void SetUserEmail(string value)
    {
        _userEmail = value;
    }

    private void SetUserName(string value)
    {
        _userName = value;
    }

    private void OnBackSignButtonPressed()
    {
       BackSignButtonPressed?.Invoke();
    }

    private void OnBackLogInButtonPressed()
    {
        BackLogInButtonPressed?.Invoke();
    }

    private void OnOkLogInButtonPressed()
    {
        OkLogInButtonPressed?.Invoke();
    }

    private void OnOkSignInButtonPressed()
    {
        OkSignInButtonPressed?.Invoke();
    }
    private void SetUserPassword(string value)
    {
        _userPassword = value;
    }


    public void SignIn()
    {
        ButtonSoundPlay();
        _loadingText.gameObject.SetActive(true);
        PlayFabClientAPI.LoginWithPlayFab(new LoginWithPlayFabRequest()
        {
            Username = _userName,
            Password = _userPassword,
        }, result =>
        {
            Debug.Log($"Success: {_userName}");
            _loadingText.gameObject.SetActive(false);
            _errorText.gameObject.SetActive(false);
            SceneManager.LoadScene(1);
            
        }, error =>
        {
            Debug.LogError($"Fail: {error.ErrorMessage}");
            _errorText.gameObject.SetActive(true);
            _errorText.text = error.ErrorMessage;
        });
    }

    private void OnDisable()
    {
        _userEmailField.onValueChanged.RemoveListener(SetUserEmail);
        _userNameField.onValueChanged.RemoveListener(SetUserName);
        _userPasswordField.onValueChanged.RemoveListener(SetUserPassword);
        _okSignInButton.onClick.RemoveListener(OnOkSignInButtonPressed);
        _okLogInInButton.onClick.RemoveListener(OnOkLogInButtonPressed);
        _backLoginButton.onClick.RemoveListener(OnBackLogInButtonPressed);
        _backSigInButton.onClick.RemoveListener(OnBackSignButtonPressed);
    }
}