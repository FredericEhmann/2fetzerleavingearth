using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using System.Text.RegularExpressions;

public class LoginUIStartscreen : MonoBehaviour
{
    [SerializeField] private Transform loading = null;
    [SerializeField] private Button _loginButton=null;
    [SerializeField] private TextMeshProUGUI _usernameError = null;
    [SerializeField] private TextMeshProUGUI _passwordError = null;
    [SerializeField] private TextMeshProUGUI _loginError = null;

    [SerializeField] private TMP_InputField _usernameInput = null;
    [SerializeField] private TMP_InputField _passwordInput = null;
    private string _username = string.Empty;
    private string _password = string.Empty;
    [SerializeField] private int _maxUsernameLength = 20;
    [SerializeField] private int _maxPasswordLength = 20;
    [SerializeField] private int _minUsernameLength = 3;
    [SerializeField] private int _minPasswordLength = 3;
    private bool _isConnected = false;


    private void Start()
    {
        if (_loginButton == null)
        {
            _loginButton = transform.Find("OnlineButton").GetComponent<Button>();
        }
        _loginButton.onClick.AddListener(Connect);

        if (_usernameInput == null)
        {
            _usernameInput = transform.Find("UsernameInput").GetComponent<TMP_InputField>();
        }
        _usernameInput.onValueChanged.AddListener(UpdateUsername);

        if (_usernameError == null)
        {
            _usernameError = transform.Find("NameError").GetComponent<TextMeshProUGUI>();
        }
        if (_passwordError == null)
        {
            _passwordError = transform.Find("NameError").GetComponent<TextMeshProUGUI>();
        }
        if (_loginError == null)
        {
            _loginError = transform.Find("LoginError").GetComponent<TextMeshProUGUI>();
        }
        if (loading == null) { 
            loading = transform.Find("Loading").GetComponent<Transform>();
        }

        if (_passwordInput == null)
        {
            _passwordInput = transform.Find("PasswordInput").GetComponent<TMP_InputField>();
        }
        _passwordInput.onValueChanged.AddListener(UpdatePassword);
        ValidateAndUpdateUI();
        loading.gameObject.SetActive(false);
    }
    public void Connect()
    {
        StopCoroutine(LoginRoutine());
        StartCoroutine(LoginRoutine());
    }

    private IEnumerator LoginRoutine()
    {
        EnableLoginButton(false);
        loading.gameObject.SetActive(true);
        NetworkClient.Instance.Connect();
        while (!_isConnected) {
            Debug.Log("Waiting!");
            yield return null;
        }
        Debug.Log("Conncted to the Server!");
//        var authRequest = "";
//        NetworkClient.Instance.SendServer();
    }

    public void UpdateUsername(string value)
    {
        _username = value.Trim();
        _usernameInput.text = _username;
        ValidateAndUpdateUI();
    }

    public void UpdatePassword(string value)
    {
        _password = value.Trim();
        _passwordInput.text = _password;
        ValidateAndUpdateUI();
    }

    private void ValidateAndUpdateUI()
    {
        var usernameRegex = Regex.Match(_username, "^[a-zA-Z0-9]+$");
        bool interactable = true;
        _passwordError.gameObject.SetActive(false);
        _usernameError.gameObject.SetActive(false);
        _loginError.gameObject.SetActive(false);
        _passwordError.text = "";
        _usernameError.text = "";
        _loginError.text = "";
        if (_username.Length < _minUsernameLength)
        {
            _usernameError.text += "Username Length should be " + _minUsernameLength + "+. ";
            interactable = false;
            _usernameError.gameObject.SetActive(true);
        }
        if (_password.Length < _minPasswordLength)
        {
            _passwordError.text += "Password Length should be " + _minPasswordLength + "+. ";
            interactable = false;
            _passwordError.gameObject.SetActive(true);
        }
        if (_username.Length > _maxUsernameLength)
        {
            _usernameError.text += "Username Length should be smaller than " + _maxUsernameLength + ". ";
            interactable = false;
            _usernameError.gameObject.SetActive(true);
        }
        if (_password.Length > _maxPasswordLength)
        {
            _passwordError.text += "Password Length should be smaller than " + _minPasswordLength + ". ";
            interactable = false;
            _passwordError.gameObject.SetActive(true);
        }
        if (!usernameRegex.Success)
        {
            _usernameError.text += "Username should only contain letters and numbers. ";
            interactable = false;
            _usernameError.gameObject.SetActive(true);
        }
        EnableLoginButton(interactable);
    }

    private void EnableLoginButton(bool interactable)
    {
        _loginButton.GetComponent<Button>().enabled = interactable;
        _loginButton.GetComponent<Image>().color = interactable ? Color.white : Color.grey;
    }

    public void Send()
    {
        NetworkClient.Instance.SendServer("Hello there!");
    }
}
