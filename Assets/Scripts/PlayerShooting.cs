using UnityEngine;
using UnityEngine.Pool;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private LaserBullet laserBullet;
    [SerializeField] private float shootingInterval;
    [SerializeField] private float shootingInterval2;
    [SerializeField] private float shootingInterval3;
    [SerializeField] private float shootingInterval4;

    [Header("Upgrade Points")]
    [SerializeField] private Transform[] shootPoints;

    [SerializeField] private AudioSource source;

    private float intervalReset;
    private ObjectPool<LaserBullet> pool;


    private void Awake()
    {
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pool = new ObjectPool<LaserBullet>(CreatePoolObj, OnTakeBulletFromPool, OnReturnBulletFromPool, OnDestroyPoolObj, true,10, 50);
        intervalReset = Constants.getUpgradeDecay(0.3f, 0.2f, 0.05f);
        shootingInterval4 = Constants.getUpgradeDecay(3, 0.2f, 0.05f);
        shootingInterval3 = Constants.getUpgradeDecay(5, 0.2f, 0.02f);
        shootingInterval2 = Constants.getUpgradeDecay(10, 0.2f, 0.003f);

    }

    private LaserBullet CreatePoolObj() {
        LaserBullet bullet = Instantiate(laserBullet, transform.position, transform.rotation);
        bullet.SetPool(pool);
        bullet.gameObject.SetActive(true);
        return bullet;
    }
    private void OnDestroyPoolObj(LaserBullet bullet) {
        Destroy(bullet.gameObject);
    }
    private void OnTakeBulletFromPool(LaserBullet bullet) {
        bullet.gameObject.SetActive(true);
    }
    private void OnReturnBulletFromPool(LaserBullet bullet) {
        bullet.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

            Shoot();

    }

    public void IncreaseUpgrade() {
        Constants.addUpgradeLevel();
        Constants.SetBulletColor();
    }
    private void Shoot()
    {
        if (EndGameManager.getInstance().isGameOver()) {
            return;
        }
        shootingInterval -= Time.deltaTime;
        shootingInterval2 -= Time.deltaTime;
        shootingInterval3 -= Time.deltaTime;
        shootingInterval4 -= Time.deltaTime;
        float upgradeLevel = Constants.getUpgradeLevel();
        if (shootingInterval <= 0)
        {
            source.pitch = Random.Range(0.5f, 1.5f);
        source.volume = Random.Range(0.01f, 0.1f);
        source.Play();

        //Instantiate(laserBullet, shootPoints[0].position, shootPoints[0].rotation);
        LaserBullet bullet = pool.Get();
        bullet.transform.position = shootPoints[0].position;
        bullet.transform.rotation = shootPoints[0].rotation;
        bullet.transform.localScale = Constants.GetBulletScale();
        bullet.SetDirectionAndSpeed();
        bullet.gameObject.GetComponent<SpriteRenderer>().color = Constants.GetBulletColor();
            shootingInterval = intervalReset;
        }

        {
            if (shootingInterval2 <= 0)
            {
                LaserBullet bullet1 = pool.Get();
                bullet1.transform.position = shootPoints[1].position;
                bullet1.transform.rotation = shootPoints[1].rotation;
                bullet1.transform.localScale = Constants.GetBulletScale();
                bullet1.SetDirectionAndSpeed();
                bullet1.gameObject.GetComponent<SpriteRenderer>().color = Constants.GetBulletColor();

                LaserBullet bullet4 = pool.Get();
                bullet4.transform.position = shootPoints[4].position;
                bullet4.transform.rotation = shootPoints[4].rotation;
                bullet4.transform.localScale = Constants.GetBulletScale();
                bullet4.SetDirectionAndSpeed();
                bullet4.gameObject.GetComponent<SpriteRenderer>().color = Constants.GetBulletColor();
                shootingInterval2 = Constants.getUpgradeDecay(10, 0.3f, 0.001f);
            }
        }
        {
            if (shootingInterval3 <= 0)
            {
                LaserBullet bullet2 = pool.Get();
                bullet2.transform.position = shootPoints[2].position;
                bullet2.transform.rotation = shootPoints[2].rotation;
                bullet2.transform.localScale = Constants.GetBulletScale();
                bullet2.gameObject.GetComponent<SpriteRenderer>().color = Constants.GetBulletColor();
                bullet2.SetDirectionAndSpeed();


                LaserBullet bullet5 = pool.Get();
                bullet5.transform.position = shootPoints[5].position;
                bullet5.transform.rotation = shootPoints[5].rotation;
                bullet5.transform.localScale = Constants.GetBulletScale();
                bullet5.gameObject.GetComponent<SpriteRenderer>().color = Constants.GetBulletColor();
                bullet5.SetDirectionAndSpeed();
                shootingInterval3 = Constants.getUpgradeDecay(5, 0.3f, 0.005f);
            }
        }
        {
            if (shootingInterval4 <= 0)
            {
                LaserBullet bullet3 = pool.Get();
                bullet3.transform.position = shootPoints[3].position;
                bullet3.transform.rotation = shootPoints[3].rotation;
                bullet3.transform.localScale = Constants.GetBulletScale();
                bullet3.gameObject.GetComponent<SpriteRenderer>().color = Constants.GetBulletColor();
                bullet3.SetDirectionAndSpeed();


                LaserBullet bullet6 = pool.Get();
                bullet6.transform.position = shootPoints[6].position;
                bullet6.transform.rotation = shootPoints[6].rotation;
                bullet6.transform.localScale = Constants.GetBulletScale();
                bullet6.gameObject.GetComponent<SpriteRenderer>().color = Constants.GetBulletColor();
                bullet6.SetDirectionAndSpeed();
                shootingInterval4 = Constants.getUpgradeDecay(3, 0.3f, 0.01f);


            }
        }
        
        
        

    }
}
