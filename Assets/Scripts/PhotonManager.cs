using Photon.Pun;
using UnityEngine;

public class PhotonManager : MonoBehaviourPunCallbacks
{

    [SerializeField] private GameUI _gameUI;
    private void Awake()
    {
        _gameUI.StartGameButtonPressed += GameUIOnStartGameButtonPressed;
        PhotonNetwork.AutomaticallySyncScene = true;
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

    private void OnDisable()
    {
        _gameUI.StartGameButtonPressed -= GameUIOnStartGameButtonPressed;
    }
}
