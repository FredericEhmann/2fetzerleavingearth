using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class EndGameManager : MonoBehaviour
{
    public static EndGameManager instance = null;
    public bool gameOver;

    private PanelController panelController;
    private TextMeshProUGUI scoreTextComponent;
    private TextMeshProUGUI upgradeTextComponent;
    private TextMeshProUGUI levelNumberTextComponent;
    public long score = 0;
    private PlayerStats playerStats;
    public bool possibleWin;
    [SerializeField] private AudioSource newHighScoreSound = null;
    [SerializeField] private AudioSource newHighLevelSound = null;
    public AudioSource bossMusic;
    private Boolean startedResolve = false;
    private bool resolveSequence1 = false;


    private IEnumerator ResolveSequence()
    {
        if (!resolveSequence1)
        {
            Debug.Log("Resolve Sequence1");
            resolveSequence1 = true;
            yield return new WaitForSeconds(0.6f);
            ResolveGame();
        }
        

    }

    public void PlayNewHighScoreSound()
    {
        if (newHighScoreSound!=null&& !newHighScoreSound.IsDestroyed() &&!newHighScoreSound.isPlaying)
        {
            StartCoroutine(PlayRoutine(newHighScoreSound, 0.4f));
        }
    }


    private IEnumerator PlayRoutine(AudioSource s, float waittime)
    {
        if (s != null)
        {
            yield return new WaitForSeconds(waittime);
            if (newHighScoreSound == null || newHighLevelSound == null || newHighScoreSound.IsDestroyed() || newHighLevelSound.IsDestroyed())
            {

            }
            else
            {
                while ((newHighScoreSound.isPlaying || newHighLevelSound.isPlaying))
                {
                    yield return new WaitForSeconds(0.1f);
                }
                s.Play();
            }
        }
    }

    public void PlayNewHighLevelSound()
    {
        if (newHighLevelSound != null && !newHighLevelSound.IsDestroyed() && !newHighLevelSound.isPlaying)
        {
            StartCoroutine(PlayRoutine(newHighLevelSound, 0.1f));
        }
    }

    private IEnumerator WaitForSpecificSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    private IEnumerator WaitForWin()
    {
        Debug.Log("Wait for win");
        while (WinCondition.GetInstance().StillHasEnemies() && !gameOver)
        {
            yield return new WaitForSeconds(0.4f);
        }
        if (possibleWin == true && gameOver == false)
        {
            WinGame();
        }
        else
        {
            LoseGame();
        }
    }





    public void resetStartedResolve()
    {
        startedResolve = false;
        resolveSequence1 = false;
        gameOver = false;
        possibleWin = false;
    }

    public long GetLevel()
    {
        long level;
        level = Constants.getLevel();
        return level;
    }
    public void IncreaseLevel()
    {
        long level = 1;
        try
        {
            level = Constants.getLevel();
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            level = 1;
        }
        level += 1;
        Constants.setLevel(level);
    }
    public void StartResolveSequence()
    {
        StartCoroutine(ResolveSequence());
    }
    public static EndGameManager getInstance()
    {

        if (instance == null)
        {
            GameObject gObj = new GameObject();
            gObj.name = "EndGameManager";
            instance = gObj.AddComponent<EndGameManager>();
        }
        return instance;
    }
    public void UpdateScore(int addScore)
    {
        score += addScore;
        scoreTextComponent.text = "Score: " + score.ToString();
    }



    public bool isGameOver() {
        return gameOver;
    }
    public void SetGameOver(bool gameOver)
    {
        this.gameOver = gameOver;
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance.newHighLevelSound == null)
            {
                instance.newHighLevelSound = newHighLevelSound;
            }
            if (instance.newHighScoreSound == null)
            {
                instance.newHighScoreSound = newHighScoreSound;
            }
            instance.gameOver = false;
            instance.possibleWin = false;
            instance.resolveSequence1 = false;
            instance.startedResolve = false;
            resetStartedResolve();
            instance.resetStartedResolve();
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (gameOver&&!resolveSequence1) {
            Debug.Log("Resolve Sequence start from update");
            StartResolveSequence();
        }
    }
    public void ResolveGame()
    {
        if (startedResolve) {
            return;
        }
        startedResolve = true;
        Debug.Log("Resolve Game1");
        if (possibleWin == true && gameOver == false)
        {
            StartCoroutine(WaitForWin());
        }
        //        else if (possibleWin == false && gameOver == true) { 
        //            AdLoseGame();
        //        }
        else if (gameOver)
        //      if (possibleWin == true && gameOver == true) 
        {
            LoseGame();
        }
        else {
            gameOver = false;
            possibleWin = false;
            resolveSequence1 = false;
            startedResolve = false;
            StopCoroutine(nameof(ResolveSequence));
        }
    }
    public void WinGame()
    {
        SongChooser.StopAudio();
        Constants.addWinOnAuto();
        Debug.Log("Win Game1");
        playerStats.canTakeDmg = false;
        {
            UnityEngine.Object[] objects = GameObject.FindObjectsByType(typeof(Enemy), FindObjectsSortMode.None);
            foreach (Enemy obj in objects)
            {
                Destroy(obj);
            }
        }
        {
            UnityEngine.Object[] objects = GameObject.FindObjectsByType(typeof(PurpleBullet), FindObjectsSortMode.None);
            foreach (PurpleBullet obj in objects)
            {
                Destroy(obj);
            }
        }
        ScoreSet();
        EndGameManager.getInstance().IncreaseLevel();
        panelController.activateWin();
        StopCoroutine(nameof(ResolveSequence));
    }


    public void LoseGame()
    {
        Constants.addLoseOnAuto();
        Debug.Log("Lose Game1");
        SongChooser.StopAudio();
        ScoreSet();
        panelController.activateLose();
        playerStats.gameObject.SetActive(false);
        StopCoroutine(nameof(ResolveSequence));
     }

    public void StopResolveSequence() {
        StopCoroutine(nameof(ResolveSequence));
    }


    private void ScoreSet()
    {
        Constants.setScore(score);
        long highScore = Constants.getHighScore();
        if (score >= highScore)
        {
            highScore = score;
            Constants.setHighScore(highScore);
        }
        score = 0;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartBossMusic();
        StopBossMusic();
    }

    public void EndLevelMusic()
    {
        SongChooser.StopAudio();
    }
    public void RepeatLevelMusic()
    {
        if (SongChooser.audioSource != null)
        {
            SongChooser.audioSource.Play();
        }
    }
    public void StartBossMusic()
    {
        if (bossMusic != null) { 
            bossMusic.Play();
        }
    }
    public void StopBossMusic()
    {
        if (bossMusic != null)
        {
            bossMusic.Stop();
        }
    }

    public void registerBossMusic()
    {
        SongChooser.audioSource = bossMusic;
        SongChooser.GetInstance().SetText();
    }

    public void RegisterPanelController(PanelController pC)
    {
        panelController = pC;
    }
    

    public void RegisterPlayerStats(PlayerStats playerStats)
    {
        this.playerStats = playerStats;
    }

    public void RegisterScoreText(TextMeshProUGUI scoreTextComponent)
    {

        this.scoreTextComponent = scoreTextComponent;
    }



    public void RegisterLevelNumberText(TextMeshProUGUI levelNumberTextComponent)
    {

        this.levelNumberTextComponent = levelNumberTextComponent;
    }

    internal void RegisterDamageUpgradeNumberText(TextMeshProUGUI textForRegistration)
    {
        this.upgradeTextComponent = textForRegistration;

    }

   

}
