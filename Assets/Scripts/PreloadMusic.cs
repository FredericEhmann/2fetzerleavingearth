using UnityEngine;

public class PreloadMusic : MonoBehaviour
{
    [SerializeField] AudioSource loseAudio;
    [SerializeField] AudioSource winAudio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        loseAudio.Play();
        loseAudio.Stop();
        winAudio.Play();
        winAudio.Stop();

    }

}
