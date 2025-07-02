using UnityEngine;

public class PowerUpHeal : GeneralPowerUp

{
    [SerializeField] private float healAmount;
    [SerializeField] private AudioSource clipToPlay;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            PlayerStats player = collision.GetComponent<PlayerStats>();
            player.AddHealth(healAmount);
            clipToPlay.Play();
            Destroy(gameObject);
        }
    }

}
