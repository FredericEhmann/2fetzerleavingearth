using TMPro;using UnityEngine;

public class GriefChooser : MonoBehaviour
{
    [SerializeField] AudioSource[] audioSources;
    [SerializeField] AudioSource[] winSources;
    [SerializeField] AudioSource[] deathSources;
    public static AudioSource audioSource;
    [SerializeField] TextMeshProUGUI texty;
    private static GriefChooser instance = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }
    private void OnEnable()
    {
        texty.gameObject.SetActive(false);
        instance = this;
    }

    public void ShowGrief(Vector3 position)
    {
        texty.gameObject.SetActive(true);
        audioSource = audioSources[Random.Range(0, audioSources.Length)];
        audioSource.Play();
        SetText();
    }

    public void ShowWin(Vector3 position)
    {
        texty.gameObject.SetActive(true);
        audioSource = winSources[Random.Range(0, winSources.Length)];
        audioSource.Play();
        SetText();
    }

    public void ShowDeath(Vector3 position)
    {
        texty.gameObject.SetActive(true);
        audioSource = deathSources[Random.Range(0, deathSources.Length)];
        audioSource.Play();
        SetText();
    }

    public static GriefChooser GetInstance()
    {
        return instance;
    }

    public void SetText() {
        Debug.Log("chose to play: " + audioSource.resource.name);
        string whatToWrite = audioSource.resource.name;
        switch (audioSource.resource.name)
        {
            case "3. Against The Odds (feat Ishii Qash)":
                whatToWrite = "Freder Seric - Against The Odds (feat Ishii Qash)";
                break;
            case "5. It Really Hurts":
                whatToWrite = "Freder Seric - It Really Hurts";
                break;
            case "8. Come Inside Our Little Paradise":
                whatToWrite = "Freder Seric - Come Inside Our Little Paradise";
                break;
            case "12. Happy Already":
                whatToWrite = "Freder Seric - Happy Already";
                break;
            case "Freder Seric - Chase Your Dreams":
                whatToWrite = "Freder Seric - Chase Your Dreams";
                break;
            case "Freder Seric - Dreamer":
                whatToWrite = "Freder Seric - Dreamer";
                break;
            case "Freder Seric - Haters":
                whatToWrite = "Freder Seric - Haters";
                break;
            case "Freder Seric - Insecure":
                whatToWrite = "Freder Seric - Insecure";
                break;
            case "Freder Seric - Ocean":
                whatToWrite = "Freder Seric - Ocean";
                break;
            case "Freder Seric - Panic":
                whatToWrite = "Freder Seric - Panic";
                break;
            case "Freder Seric - Star":
                whatToWrite = "Freder Seric - Star";
                break;
            case "Loving your Vibe":
                whatToWrite = "Freder Seric - Loving your Vibe";
                break;
            case "fetzer leaving earth spoken intro":
                whatToWrite = "Freder Seric - Fetzer Leaving Earth Spoken Intro";
                break;
            case "fetzer leaving earth song version censored":
                whatToWrite = "Freder Seric - Fetzer Leaving Earth Song Intro";
                break;
            case "easytensiononyoasslaenger":
                whatToWrite = "Freder Seric - Fetzer Leaving Earth Lose";
                break;
            case "numetal shit in D beat":
                whatToWrite = "Freder Seric - Fetzer Leaving Earth Victory";
                break;
            case "5jahrezusammenbeat":
                whatToWrite = "Freder Seric - Fetzer Leaving Earth Background Level Music 1";
                break;
            case "shit beat":
                whatToWrite = "Freder Seric - Fetzer Leaving Earth Boss";
                break;
            default:
                // Handle unknown cases
                break;
        }
        texty.text = whatToWrite;
        Color c = texty.color;
        c.a = 0.4f;
        texty.color = c;

    }

    public static void StopAudio()
    {
        Debug.Log("Stopping Audio " + audioSource.ToString());
        if (audioSource != null) { 
            audioSource.Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (texty.isActiveAndEnabled) 
        {
            Color c = texty.color;
            if (c.a > 0.01f)
            {
                c.a -= Random.Range(-0.001f, +0.002f);
                texty.color = c;
            }
            else
            {
                c.a -= 0;
                texty.color = c;
            }
        }

    }
}
