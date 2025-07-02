using UnityEngine;
using TMPro;

public class LevelNumberRegistration : MonoBehaviour
{
    TextMeshProUGUI textForRegistration;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textForRegistration = GetComponent<TextMeshProUGUI>();
        EndGameManager.getInstance().RegisterLevelNumberText(textForRegistration);
        textForRegistration.text = "Level: " + Constants.getLevel();
    }

    private void Update()
    {
        if (Constants.isSurvivalMode())
        {
            textForRegistration.text = "Level: " + Constants.getLevel();
        }
    }

}
