using UnityEngine;
using TMPro;

public class HealthRegistration : MonoBehaviour
{

    [SerializeField] private PlayerStats playerStats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TextMeshProUGUI textForRegistration = GetComponent<TextMeshProUGUI>();
        playerStats.RegisterHealthText(textForRegistration);
        textForRegistration.text = "3/3";
    }

}
