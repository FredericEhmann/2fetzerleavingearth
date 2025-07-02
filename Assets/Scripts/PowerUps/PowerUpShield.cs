using UnityEngine;

public class PowerUpShield : GeneralPowerUp

{
    [SerializeField] private AudioSource clipToPlay;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            PlayerShieldActivator shieldActivator = collision.GetComponent<PlayerShieldActivator>();
            shieldActivator.ActivateShield();
            clipToPlay.Play();
            Destroy(gameObject);
        }
    }

}
