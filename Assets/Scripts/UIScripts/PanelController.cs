using UnityEngine;
using UnityEngine.Audio;

public class PanelController : MonoBehaviour
{

    [SerializeField] private CanvasGroup cGroup;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private LoseAd loseAd;
    [SerializeField] private AudioSource audioSourceWin;
    [SerializeField] private AudioSource audioSourceLose;
    [SerializeField] private GameObject adLoseScreen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EndGameManager.getInstance().RegisterPanelController(this);
        if (cGroup != null)
        {
            cGroup.alpha = 0;
        }
        if (winScreen != null)
        {
            winScreen.SetActive(false);
        }
        if (loseScreen != null)
        {
            loseScreen.SetActive(false);
        }
        if (adLoseScreen != null)
        {
            adLoseScreen.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void activateWin()
    {
        Constants.MakeRandomNewColorsForThisNewLevel(Constants.getLevel());
        EndGameManager.getInstance().StopBossMusic();
        cGroup.alpha = 1;
        winScreen.SetActive(true);
        SongChooser.audioSource = audioSourceWin;
        audioSourceWin.Play();
        SongChooser.GetInstance().SetText();
    }

    public void activateLose()
    {
        EndGameManager.getInstance().StopBossMusic();
        loseAd.ShowAdWith(cGroup, loseScreen, audioSourceLose);
        SongChooser.audioSource = audioSourceLose;
        SongChooser.GetInstance().SetText();
    }

    public void activateAdLose()
    {
        EndGameManager.getInstance().StopBossMusic();
        cGroup.alpha = 1;
        adLoseScreen.SetActive(true);
        SongChooser.audioSource = audioSourceLose;
        SongChooser.GetInstance().SetText();
    }

    public void DeactivateAdLose()
    {
        cGroup.alpha = 0;
        adLoseScreen.SetActive(false);
    }
}
