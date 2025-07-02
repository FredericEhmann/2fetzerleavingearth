using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LoginUI : MonoBehaviour
{
    [SerializeField] private Button _loginButton=null;
    [SerializeField] private Button _sendButton=null;

    private void Start()
    {
        if (_loginButton == null) {
            _loginButton = transform.Find("Connect").GetComponent<Button>();
        }
        _loginButton.onClick.AddListener(Connect);
        if (_sendButton == null)
        {
            _sendButton = transform.Find("Send").GetComponent<Button>();
        }
        _sendButton.onClick.AddListener(Send);
    }
    public void Connect()
    {
        NetworkClient.Instance.Connect();
    }
    public void Send()
    {
        NetworkClient.Instance.SendServer("Hello there!");
    }
}
