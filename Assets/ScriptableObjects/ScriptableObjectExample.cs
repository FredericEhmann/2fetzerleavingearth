using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObject/PowerUpSpawner",fileName = "Spawner")]
public class ScriptableObjectExample : ScriptableObject
{
    public GameObject[] powerUp;

    public void SpawnPowerUp(Vector3 spawnPos, float spwnTreshold) {
        float randomChance = Random.Range(0f, 1f);
        if (randomChance < spwnTreshold) {
            int randomPowerUp = Random.Range(0, powerUp.Length);
            Instantiate(powerUp[randomPowerUp], spawnPos, Quaternion.identity);
        }

    }
}
