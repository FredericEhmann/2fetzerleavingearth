using UnityEngine;
using UnityEngine.Pool;


public class Meteor : Enemy
{
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    private float speed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float currentRotateSpeed;
    private float startHealth;
    private ObjectPool<Meteor> referencePool;

    void OnEnable()
    {

        WinCondition.GetInstance()?.AddEnemy(this.gameObject);
        chanceToDrop = 0.2f / Mathf.Log((Constants.getUpgradeLevel() + (int)(Constants.getHealingBonus() * 30)), 2);
        speed = Random.Range(minSpeed, maxSpeed-2+Mathf.Log(Constants.getLevel(),10));
        rb.linearVelocity = Vector2.down * speed;
        rotateSpeed = Random.Range(50f, 300f);
        health = (((gameObject.transform.localScale.y * rotateSpeed) - speed) / 220)+ Random.Range(0, Mathf.Max(0, Mathf.Log(Constants.getLevel())));
        startHealth = health;
        score = (int)(startHealth * score);
        damage = damage * Random.Range(0.5f, 1.9f+ Random.Range(0,Mathf.Max(0, Mathf.Log(Constants.getLevel()))));
    }
    public void SetPool(ObjectPool<Meteor> pool)
    {
        this.referencePool = pool;
    }
    
    void Update()
    {
        currentRotateSpeed = rotateSpeed * (startHealth/health);
        transform.Rotate(0,0, currentRotateSpeed * Time.deltaTime);
    }

    public override void HurtSequence()
    {
        
    }
    
    public override void DeathSequence()
    {
        base.DeathSequence();
        Instantiate(explosionPrefab, transform.position, transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D otherColl)
    {
        if (otherColl.CompareTag("Player")) {
            PlayerStats player = otherColl.GetComponent<PlayerStats>();
            player.PlayerTakeDamage(damage,this.transform);
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            if (gameObject.activeSelf)
            {
                if (Time.timeScale != Constants.deathSpeed())
                {
                    referencePool.Release(this);
                }
            }
        }
    }


    private void OnDisable()
    {
        WinCondition.GetInstance()?.RemoveEnemy(this.gameObject);
        health = 1;
        damage = 1;
        score = 5;
        minSpeed = 8;
        maxSpeed = 15;
        rotateSpeed = 100;
        currentRotateSpeed = 0;
    }

    private void OnDestroy()
    {
        WinCondition.GetInstance()?.RemoveEnemy(this.gameObject);
    }

    private void OnBecameInvisible()
    {
        if (gameObject.activeSelf)
        {
            referencePool.Release(this);
        }
    }
}
