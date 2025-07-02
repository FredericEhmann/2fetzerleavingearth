using UnityEngine;

public class CloseOptions : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseOptionsNow() {
        gameObject.SetActive(false);
    }

    public void OpenOptionsNow() {
        gameObject.SetActive(true);
    }
}
