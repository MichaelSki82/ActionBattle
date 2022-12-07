using System;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using Photon.Pun;

public class AuthorizationManager : MonoBehaviour
{
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void Start()
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
    }

    private void OnLoginSuccess(LoginResult result)
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
    }
}