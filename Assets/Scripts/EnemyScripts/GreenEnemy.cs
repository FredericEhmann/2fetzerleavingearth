using UnityEngine;
using UnityEngine.Pool;
public class GreenEnemy : Enemy
{
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    private float speed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float currentRotateSpeed;
    private float startHealth;
    [SerializeField] protected Animator anim;
    private ObjectPool<GreenEnemy> referencePool;


    void OnEnable()
    {
        chanceToDrop = 1f / Mathf.Log((Constants.getUpgradeLevel() + (int)(Constants.getHealingBonus() * 30)), 2);
        speed = Random.Range(minSpeed, maxSpeed-4)+ Random.Range(0, Mathf.Max(0, Mathf.Log(Constants.getLevel())));
        Vector2 old=Vector2.down* speed;
        rb.linearVelocity = new Vector2(Random.Range(-6,6),old.y);
        rotateSpeed = Random.Range(50f, 300f);
        health = ((((gameObject.transform.localScale.y * rotateSpeed) - speed) / 220)+ Random.Range(0, Mathf.Max(0, Mathf.Log(Constants.getLevel()))))-0.03f;
        startHealth = health;
        score = (int)startHealth*5;
        damage = damage * Random.Range(0.5f, 1.9f)+ Random.Range(0, Mathf.Max(0, Mathf.Log(Constants.getLevel())));
        WinCondition.GetInstance()?.AddEnemy(this.gameObject);
    }

    public void SetPool(ObjectPool<GreenEnemy> pool)
    {
        this.referencePool = pool;
    }
    void Update()
    {
        currentRotateSpeed = rotateSpeed * (startHealth/health);
        transform.Rotate(0,0, currentRotateSpeed * Time.deltaTime);
        Vector3 p=Camera.main.WorldToScreenPoint(transform.position);
        if (p.y < 0)
        {
            if (gameObject.activeSelf)
            {
                referencePool.Release(this);
            }
        }
        if (p.x < 0)
        {
            rb.linearVelocityX = Mathf.Abs(rb.linearVelocityX);
        }
        if (p.x > Screen.width)
        {
            rb.linearVelocityX = -Mathf.Abs(rb.linearVelocityX);
        }
    }

    public override void HurtSequence()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Dmg"))
        {
            return;
        }
        anim.SetTrigger("DamageTrigger");
    }


    public override void DeathSequence()
    {
        base.DeathSequence();
        Instantiate(explosionPrefab,transform.position, transform.rotation);
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

    private void OnDestroy()
    {
        WinCondition.GetInstance()?.RemoveEnemy(this.gameObject);
    }
    private void OnDisable()
    {
        WinCondition.GetInstance()?.RemoveEnemy(this.gameObject);
    }

}
