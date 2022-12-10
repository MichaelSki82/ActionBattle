using System;
using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using Random = UnityEngine.Random;

public class PhotonManager : MonoBehaviourPunCallbacks
{

    [SerializeField] private GameUI _gameUI;
    
    private bool isFirstStart = true;
    private void Awake()
    {
        _gameUI.StartGameButtonPressed += GameUIOnStartGameButtonPressed;
        PhotonNetwork.AutomaticallySyncScene = true;
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(),
            OnGetAccountSuccess, OnFailure);
    }

    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomOrCreateRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            
        }
    }
    private void GameUIOnStartGameButtonPressed()
    {
        _gameUI.ButtonSoundPlay();
        Time.timeScale = 1f;
        _gameUI.SetMainSceneWindow(true);
        _gameUI.SetLoadingText(true);
        Connect();
        _gameUI.SetPhotonMenu(false);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log($"You jouned room: {PhotonNetwork.CurrentRoom.Name}");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.LogError(message);
    }

    private void OnGetAccountSuccess(GetAccountInfoResult result)
    {
        if (isFirstStart)
        {
             _gameUI.SetPhotonMenu(true);
             _gameUI._WelcomeText.text = $"Welcome back, Player {result.AccountInfo.Username}";
             isFirstStart = false;
        }
       
    }
    private void OnFailure(PlayFabError error)
    {
        var errorMessage = error.GenerateErrorReport();
        Debug.LogError($"Something went wrong: {errorMessage}");
    }
    private void OnDisable()
    {
        _gameUI.StartGameButtonPressed -= GameUIOnStartGameButtonPressed;
    }
}
