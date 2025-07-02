using UnityEngine;
using TMPro;

public class ShieldRegistration : MonoBehaviour
{

    [SerializeField] private PlayerStats playerStats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TextMeshProUGUI textForRegistration = GetComponent<TextMeshProUGUI>();
        playerStats.RegisterShieldText(textForRegistration);
        textForRegistration.text = "0";
    }

}
