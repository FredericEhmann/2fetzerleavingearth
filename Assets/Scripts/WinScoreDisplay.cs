using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Xml;
using System;


public class WinScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI secondsLeft;
    [SerializeField] private TextMeshProUGUI mmrText = null;
    [SerializeField] private Button shopButton;

    private void OnEnable()
    {
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
        if (mmrText != null)
        {
            double mmr = Constants.GetMMR();
            double mmrChange = Constants.getLastMMRChange();
            if (Constants.isSurvivalMode())
            {
                mmrChange = 0;
            }
            mmrText.text = ("MMR: " + (long)mmr + "." + (long)((mmr * 1000) % 1000) + "" + " (" + ((mmrChange > 0) ? "+" : "") + (long)mmrChange + "." + (long)((mmrChange * 1000) % 1000) + ")").Replace(".-", ".");
        }
        secondsLeft.text = "Seconds: 0";
        long level = Constants.getLevel()-1;
        long highScore = Constants.getLastLevelHighScore();
        long score = Constants.getLastLevelScore();
        scoreText.text = "Score: "+score.ToString();
        highScoreText.text = "Highscore: "+highScore.ToString();
        levelText.text = "Level: "+level.ToString();
        EndGameManager.getInstance().RegisterLevelNumberText(levelText);
        EndGameManager.getInstance().RegisterScoreText(scoreText);


    }
}
