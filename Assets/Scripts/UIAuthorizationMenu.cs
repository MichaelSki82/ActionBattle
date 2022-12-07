using UnityEngine;
using UnityEngine.UI;

public class UIAuthorizationMenu : MonoBehaviour
{
    [SerializeField] private InputField _userNameField;
    [SerializeField] private InputField _userPasswordField;
    [SerializeField] private Button _okLogInInButton;
    [SerializeField] private Button _okSignInButton;
    [SerializeField] private Button _backLoginButton; 
    [SerializeField] private Button _backSigInButton;
    [SerializeField] private Text _errorText;
    [SerializeField] private Text _loadingText;
    [SerializeField] private GameObject _signIn;
    [SerializeField] private GameObject _createAccount;
    
    private string _userName;
    private string _userPassword;
}