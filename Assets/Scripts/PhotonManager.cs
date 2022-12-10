using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Random = UnityEngine.Random;

public class PhotonManager : MonoBehaviourPunCallbacks
{

    [SerializeField] private GameUI _gameUI;
    
    private void Awake()
    {
        _gameUI.StartGameButtonPressed += GameUIOnStartGameButtonPressed;
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    /*public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomOrCreateRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            
        }
    }*/
    private void GameUIOnStartGameButtonPressed()
    {
        _gameUI.ButtonSoundPlay();
        Time.timeScale = 1f;
        _gameUI.SetMainSceneWindow(true);
        _gameUI.SetLoadingText(true);
        string roomName = _gameUI._roomNameField.text;
        roomName = (roomName.Equals(String.Empty)) ? "Room " + Random.Range(1000, 10000) : roomName;

        byte maxPlayers = 8;
        RoomOptions options = new RoomOptions { MaxPlayers = maxPlayers, PlayerTtl = 10000 };
        PhotonNetwork.JoinRandomOrCreateRoom(roomName: roomName, roomOptions: options);
        //Connect();
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

    private void OnDisable()
    {
        _gameUI.StartGameButtonPressed -= GameUIOnStartGameButtonPressed;
    }
}
