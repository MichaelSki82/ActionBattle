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
        public bool needCreation;
        public string id;
    }
    
    private void Awake()
    {
        _uiAuthorizationMenu.OkLogInButtonPressed += Login;
        _uiAuthorizationMenu.OkSignInButtonPressed += SigIn;
        _uiAuthorizationMenu.BackLogInButtonPressed += ExitGame;
        _uiAuthorizationMenu.BackSignButtonPressed += ExitGame;
    }

    private void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            PlayFabSettings.staticSettings.TitleId = PLAYFAB_TITLE;
        }
        var needCreation = !PlayerPrefs.HasKey(AUTHENTIFUCATION_KEY);
        var id = PlayerPrefs.GetString(AUTHENTIFUCATION_KEY, Guid.NewGuid().ToString());
        var data = new Data { needCreation = needCreation, id = id };
        var request = new LoginWithCustomIDRequest()
        {
            CustomId = id,
            CreateAccount = needCreation
        };
        PlayFabClientAPI.LoginWithCustomID(request, Succes, Fail, data);
        // PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest()
        // {
        //     CustomId = id,
        //     CreateAccount = !needCreation
        // }, success => { PlayerPrefs.SetString(AUTHENTIFUCATION_KEY, id); }, OnFailure);}
    }

    private void Fail(PlayFabError error)
    {
        var errorMessage = error.GenerateErrorReport();
        Debug.LogError($"Something went wrong: {errorMessage}");
       
    }

    private void Succes(LoginResult result)
    {
        PlayerPrefs.SetString(AUTHENTIFUCATION_KEY, Guid.NewGuid().ToString());
        Debug.Log(result.PlayFabId);
        Debug.Log(((Data)result.CustomData).needCreation);
        //SceneManager.LoadScene(1);
    }

    

    private void Login()
    {
       _uiAuthorizationMenu.LogIn();
    }

    private void SigIn()
    {
       _uiAuthorizationMenu.SignIn();
    }

    private void ExitGame()
    {
        _uiAuthorizationMenu.ButtonSoundPlay();
        Application.Quit();
    }

    private void OnDisable()
    {
        _uiAuthorizationMenu.OkLogInButtonPressed -= Login;
        _uiAuthorizationMenu.OkSignInButtonPressed -= SigIn;
        _uiAuthorizationMenu.BackLogInButtonPressed -= ExitGame;
        _uiAuthorizationMenu.BackSignButtonPressed -= ExitGame;
    }


    /*public void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            PlayFabSettings.staticSettings.TitleId = "E6E4C";
        }

        var request = new LoginWithCustomIDRequest
        {
            CustomId = "GeekBrainsLesson3",
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);

        Connect();
    }*/

    /*private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made successful API call!");
    }

    private void OnLoginFailure(PlayFabError error)
    {
        var errorMessage = error.GenerateErrorReport();
        Debug.LogError($"Something went wrong: {errorMessage}");
    }

    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }*/
}