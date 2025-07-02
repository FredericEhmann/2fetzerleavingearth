using System;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public bool protection = false;
    [SerializeField] private GameObject shieldBase;
    [SerializeField] private string playerName;


    public string getPlayerName() {
        return playerName;

    }
    private void Start()
    {
        if (Constants.getShield(playerName) > 0) {
            if ("Player".Equals(playerName))
            {
                shieldBase.SetActive(true);
            }
            gameObject.SetActive(true);
            protection = true;
        }
        else
        {
            if ("Player".Equals(playerName))
            {
                shieldBase.SetActive(false);
            }
            gameObject.SetActive(false);
            protection = false;
        }
    }
    private void onEnable() {
        Constants.setShield(playerName,Constants.getShield(playerName) +3);
        protection = true;
        shieldBase.SetActive(true);
    }
    private void DamageShield() {
        Constants.setShield(playerName, Constants.getShield(playerName) - 1);
        if (Constants.getShield(playerName) <= 0) {
            Constants.setShield(playerName, 0);
            protection = false;
            gameObject.SetActive(false);
            if ("Player".Equals(playerName))
            {
                shieldBase.SetActive(false);
            }
        }
    }

    public long getHitsToDestroy() {

        return Constants.getShield(playerName);
    }

    public void RepairShield()
    {
        if ("Player".Equals(playerName))
        {
            shieldBase.SetActive(true);
        }
        Constants.setShield(playerName, Constants.getShield(playerName) +3);
        if (Constants.getShield(playerName) > 3)
        {
            Constants.setShield(playerName, Constants.getShield(playerName) -1);
        }
        if (Constants.getShield(playerName) > 5)
        {
            Constants.setShield(playerName, Constants.getShield(playerName) - 1);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy)) {

            if (collision.CompareTag("Boss")) {
                Constants.setShield(playerName, 0);
                DamageShield();
                return;
            }
            enemy.TakeDamage(10000);
            DamageShield();
        }
        else
        if (collision.TryGetComponent(out PurpleBullet bullet))
        {
            PoolPurpleBullets.getInstance().ReleaseFromPool(bullet);
            DamageShield();
        }
        else if (collision.TryGetComponent(out GreenEnemy greenEnemy))
        {
            PoolGreenEnemies.getInstance().ReleaseFromPool(greenEnemy);
            DamageShield();
        }
        else if (collision.TryGetComponent(out PurpleEnemy purpleEnemy))
        {
            PoolPurpleEnemies.getInstance().ReleaseFromPool(purpleEnemy);
            DamageShield();
        }
        else if (collision.TryGetComponent(out Meteor meteor))
        {
            PoolMeteors.getInstance().ReleaseFromPool(meteor);
            DamageShield();
        }
        else
        {
            Destroy(collision.gameObject);
            DamageShield();
        }
    }

}
