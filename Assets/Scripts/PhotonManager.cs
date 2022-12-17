using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PhotonManager : MonoBehaviourPunCallbacks
{
   
    
    [SerializeField] private AudioSource _buttonSound;
    [SerializeField] private TMP_Text _loadingText;
    [SerializeField] private TMP_Text _welcomeText;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _deleteAccount;
    
    private void Awake()
    {
        _loadingText.gameObject.SetActive(false);
        _startButton.onClick.AddListener(GameUIOnStartGameButtonPressed);
        _deleteAccount.onClick.AddListener(OnDeleteAccountPressed);
        PhotonNetwork.AutomaticallySyncScene = true;
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(),
            OnGetAccountSuccess, OnFailure);
    }

    private void OnDeleteAccountPressed()
    {
        _buttonSound.Play();
        PlayerPrefs.DeleteAll();
       SceneManager.LoadScene(0);
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
       
        _buttonSound.Play();
        Time.timeScale = 1f;
        _loadingText.gameObject.SetActive(true);
        Connect();
        SceneManager.LoadScene(2);
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
       
             _welcomeText.text = $"Welcome back, Player {result.AccountInfo.Username}";
            
        
    }
    private void OnFailure(PlayFabError error)
    {
        var errorMessage = error.GenerateErrorReport();
        Debug.LogError($"Something went wrong: {errorMessage}");
    }
    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(GameUIOnStartGameButtonPressed);
    }
}
