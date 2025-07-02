using UnityEngine;


public class PlayerShieldActivator : MonoBehaviour
{
    [SerializeField] private Shield shield;

    public void ActivateShield() {
        if (!shield.gameObject.activeSelf)
        {
            shield.RepairShield();
            shield.gameObject.SetActive(true);
        }
        else {
            shield.RepairShield();
        }
    }

}
