using UnityEngine;

public class CloseLogin : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CloseLoginNow();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseLoginNow() {
        gameObject.SetActive(false);
        ButtonController.showLoginScreen = false;
    }

    public void OpenLoginNow() {
        gameObject.SetActive(true);
        ButtonController.showLoginScreen = true;
    }
}
