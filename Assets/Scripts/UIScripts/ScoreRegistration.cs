using UnityEngine;
using TMPro;

public class ScoreRegistration : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TextMeshProUGUI textForRegistration = GetComponent<TextMeshProUGUI>();
        EndGameManager.getInstance().RegisterScoreText(textForRegistration);
        textForRegistration.text = "Score: 0";
    }

}
