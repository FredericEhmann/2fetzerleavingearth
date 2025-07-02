using UnityEngine;

public class PowerUpShooting : GeneralPowerUp

{

    [SerializeField] private AudioSource clipToPlay;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            PlayerShooting playerShooting = collision.GetComponent<PlayerShooting>();
            playerShooting.IncreaseUpgrade();
            clipToPlay.Play();
            Destroy(gameObject);
        }
    }

}
