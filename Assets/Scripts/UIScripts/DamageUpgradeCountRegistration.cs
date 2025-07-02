using UnityEngine;
using TMPro;

public class DamageUpgradeCountRegistration : MonoBehaviour
{
    TextMeshProUGUI textForRegistration;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textForRegistration = GetComponent<TextMeshProUGUI>();
        EndGameManager.getInstance().RegisterDamageUpgradeNumberText(textForRegistration);
        textForRegistration.text = "Upgrade: " + Constants.getUpgradeLevel();
    }

    private void Update()
    {
        textForRegistration.text = "Upgrade: " + Constants.getUpgradeLevel();
    }

}
