using System;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AuthorizationManager : MonoBehaviour
{

    [SerializeField] private UIAuthorizationMenu _uiAuthorizationMenu;
    
    private const string PLAYFAB_TITLE = "E6E4C";
    private const string AUTHENTIFUCATION_KEY = "AUTHENTIFUCATION_KEY";
    

    private struct Data
    {
        public bool NeedCreation;
        public string ID;
    }
    
    private void Awake()
    {
        _uiAuthorizationMenu.OkLogInButtonPressed += LogIn;
        _uiAuthorizationMenu.OkSignInButtonPressed += SignIn;
        _uiAuthorizationMenu.BackLogInButtonPressed += ExitGame;
        _uiAuthorizationMenu.BackSignButtonPressed += ExitGame;
    }

    private void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            PlayFabSettings.staticSettings.TitleId = PLAYFAB_TITLE;
        }
        var needCreation = PlayerPrefs.HasKey(AUTHENTIFUCATION_KEY);
        var id = PlayerPrefs.GetString(AUTHENTIFUCATION_KEY, Guid.NewGuid().ToString());
        var data = new Data { NeedCreation = needCreation, ID = id };

        if (needCreation)
        {
            _uiAuthorizationMenu.SetLoadingText(true);
            PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest()
            {
                CustomId = id,
                CreateAccount = !needCreation
            }, succses =>
            {
                PlayerPrefs.SetString(AUTHENTIFUCATION_KEY, id);
                Debug.Log("Congratulations, you made successful API call!");
                SceneManager.LoadScene(1);
            }, Fail);
           
        }
       
    }

    private void Fail(PlayFabError error)
    {
        var errorMessage = error.GenerateErrorReport();
        //Debug.LogError($"Something went wrong: {errorMessage}");
        _uiAuthorizationMenu.SetActiveCreateWindow(true);
       
    }

    private void Succes(LoginResult result)
    {
        PlayerPrefs.SetString(AUTHENTIFUCATION_KEY, Guid.NewGuid().ToString());
        Debug.Log("Congratulations, you made successful API call!");
        Debug.Log(result.PlayFabId);
        Debug.Log(((Data)result.CustomData).NeedCreation);
        Debug.Log(((Data)result.CustomData).ID);
        SceneManager.LoadScene(1);
        
    }

    private void LogIn()
       {
           _uiAuthorizationMenu.ButtonSoundPlay();
           _uiAuthorizationMenu.SetLoadingText(true);
           PlayFabClientAPI.RegisterPlayFabUser(new RegisterPlayFabUserRequest()
           {
               Email = _uiAuthorizationMenu._userEmail,
               Username = _uiAuthorizationMenu._userName,
               Password = _uiAuthorizationMenu._userPassword,
               RequireBothUsernameAndEmail = true
           }, result =>
           {
               Debug.Log($"Success: {_uiAuthorizationMenu._userName}");
               _uiAuthorizationMenu.SetLoadingText(false);
               _uiAuthorizationMenu.SetErrorText(false);
               _uiAuthorizationMenu.SetActiveCreateWindow(false);
                   _uiAuthorizationMenu.SetActiveSigInWindow(true);
               //SceneManager.LoadScene(1);
           }, error =>
           {
               Debug.LogError($"Fail: {error}");
               _uiAuthorizationMenu.SetErrorText(true);
               _uiAuthorizationMenu._errorText.text = error.ToString();
           });
       }

 private void SignIn()
    {
        _uiAuthorizationMenu.ButtonSoundPlay();
        _uiAuthorizationMenu.SetLoadingText(true);
        PlayFabClientAPI.LoginWithPlayFab(new LoginWithPlayFabRequest()
        {
            Username =  _uiAuthorizationMenu._userName,
            Password = _uiAuthorizationMenu._userPassword,
        }, result =>
        {
            Debug.Log($"Success: {_uiAuthorizationMenu._userName}");
            PlayerPrefs.SetString(AUTHENTIFUCATION_KEY, result.PlayFabId);
            _uiAuthorizationMenu.SetLoadingText(true);
            _uiAuthorizationMenu.SetErrorText(false);
            _uiAuthorizationMenu.SetActiveSigInWindow(false);
            var id = PlayerPrefs.GetString(AUTHENTIFUCATION_KEY, Guid.NewGuid().ToString());
            
            PlayFabClientAPI.LinkCustomID(new LinkCustomIDRequest
            {
                CustomId = id
            }, succes => { }, error => {});
            SceneManager.LoadScene(1);
            
        }, error =>
        {
            Debug.LogError($"Fail: {error.ErrorMessage}");
            _uiAuthorizationMenu.SetErrorText(true);
            _uiAuthorizationMenu._errorText.text = error.ErrorMessage;
        });
    }

   

    private void ExitGame()
    {
        _uiAuthorizationMenu.ButtonSoundPlay();
        Application.Quit();
    }

    private void OnDisable()
    {
        _uiAuthorizationMenu.OkLogInButtonPressed -= LogIn;
        _uiAuthorizationMenu.OkSignInButtonPressed -= SignIn;
        _uiAuthorizationMenu.BackLogInButtonPressed -= ExitGame;
        _uiAuthorizationMenu.BackSignButtonPressed -= ExitGame;
    }

}