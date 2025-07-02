using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.UI;


public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI survivalHighLevelText=null;
    [SerializeField] private TextMeshProUGUI survivalHighScoreText = null;
    [SerializeField] private TextMeshProUGUI difficultyText = null;
    [SerializeField] private TextMeshProUGUI mmrText = null;
    [SerializeField] private TextMeshProUGUI recommendedDifficultyText = null;
    [SerializeField] private Button shopButton;

    public static ScoreDisplay instance;


    private void OnEnable()
    {
        instance = this;
        if (shopButton != null)
        {
            if (Constants.connected)
            {
                shopButton.gameObject.SetActive(true);
            }
            else
            {
                shopButton.gameObject.SetActive(false);
            }
        }
    }
    private void Start()
    {
        instance = this;
        LoadScoreDisplay();
    }
    public static ScoreDisplay GetInstance() {
        return instance;
    }

    public void LoadScoreDisplay()
    {
        long level = Constants.getLevel();
        long highScore = Constants.getHighScore();
        long score = Constants.getScore();
        long difficulty = Constants.getDifficultyAsLong();
        if (survivalHighLevelText != null)
        {
            if (Constants.isSurvivalMode() || FadeCanvas.getInstance() == null || FadeCanvas.getInstance().isMainMenu())
            {
                long highlevelsurvival = Constants.GetHighestSurvivalLevel();
                survivalHighLevelText.text = "Highlevel: " + highlevelsurvival;
            }
            else
            {
                survivalHighLevelText.gameObject.SetActive(false);
            }
        }
        if (survivalHighScoreText != null)
        {
            long highscoresurvival = Constants.GetHighestSurvivalScore();
            survivalHighScoreText.text = "Highscore: " + highscoresurvival;
        }
        if (difficultyText != null)
        {
            difficultyText.text = "Difficulty: " + difficulty + (Constants.getDifficultyAutoTrue() ? "(auto)" : "(manual)");
        }
        if (Constants.getDifficultyAutoTrue())
        {
            if (recommendedDifficultyText != null)
            {
                recommendedDifficultyText.gameObject.SetActive(false);
            }
        }
        else
        {
            if (recommendedDifficultyText != null)
            {
                recommendedDifficultyText.gameObject.SetActive(true);
                recommendedDifficultyText.text = "Auto: " + Constants.getMMRDifficultyAsLong();
            }
        }
        if (mmrText != null)
        {
            double mmr = Constants.GetMMR();
            double mmrChange = Constants.getLastMMRChange();
            if (Constants.isSurvivalMode()) {
                mmrChange = 0;
            }
            mmrText.text = ("MMR: " + (long)mmr + "." + (long)((mmr * 1000) % 1000) + "" + " (" + ((mmrChange > 0) ? "+" : "") + (long)mmrChange + "." + (long)((mmrChange * 1000) % 1000) + ")").Replace(".-", ".");
        }
        highScoreText.text = "Highscore: " + highScore.ToString();
        scoreText.text = "Score: " + score.ToString();
        levelText.text = "Level: " + level.ToString();
        EndGameManager.getInstance().RegisterLevelNumberText(levelText);
        EndGameManager.getInstance().RegisterScoreText(scoreText);


    }
}
