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

     public string _userEmail;
     public string _userName;
    public  string _userPassword;

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
        //_createAccount.gameObject.SetActive(false);
    }

    public void SetLoadingText(bool isOn)
    {
        _loadingText.gameObject.SetActive(isOn);
    }

    public void SetErrorText(bool isOn)
    {
        _errorText.gameObject.SetActive(isOn);
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


    public void SetUserEmail(string value)
    {
        _userEmail = value;
    }

    public void SetUserName(string value)
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
    public void SetUserPassword(string value)
    {
        _userPassword = value;
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