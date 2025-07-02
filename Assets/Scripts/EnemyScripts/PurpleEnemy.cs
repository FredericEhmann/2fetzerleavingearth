using System.Runtime.CompilerServices;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Pool;

public class PurpleEnemy : Enemy
{
    [SerializeField] private float speed;
    private float shootTimer = 0;
    [SerializeField] private float shootInterval;
    [SerializeField] private Transform leftCannon;
    [SerializeField] private Transform rightCannon;
    [SerializeField] private static PurpleBullet bulletPrefab;
    [SerializeField] protected Animator anim;
    private Camera mainCam;
    private float maxDown;
    private ObjectPool<PurpleEnemy> referencePool;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        WinCondition.GetInstance()?.AddEnemy(this.gameObject);
        chanceToDrop = 1f / Mathf.Log((Constants.getUpgradeLevel() + (int)(Constants.getHealingBonus() * 30)), 2);
        health += Random.Range(0, Mathf.Max(0, Mathf.Log(Constants.getLevel())));
        damage += Random.Range(0, Mathf.Max(0, Mathf.Log(Constants.getLevel())));
        rb.linearVelocity = Vector2.down * speed;
        mainCam = Camera.main;
        maxDown = mainCam.ViewportToWorldPoint(new Vector2(0, 0.7f)).y;
        score = 10;
    }
    public void SetPool(ObjectPool<PurpleEnemy> pool)
    {
        this.referencePool = pool;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.linearVelocity.y != 0 && maxDown > transform.position.y) {
            rb.linearVelocity = Vector2.right * speed*Random.Range(0.4f,Constants.getLevelDecay(0.5f,3f,0.01f));
        }
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootInterval) {
            PurpleBullet bullet1 = PoolPurpleBullets.getInstance().GetFromPool();
            bullet1.ChangeDamageTo(0.7f);
            bullet1.transform.position = leftCannon.position;
            bullet1.transform.rotation = Quaternion.identity;
            bullet1.SetDirectionAndSpeed();
            bullet1.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

            PurpleBullet bullet2 = PoolPurpleBullets.getInstance().GetFromPool();
            bullet2.transform.position = rightCannon.position;
            bullet2.ChangeDamageTo(0.7f); 
            bullet2.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            bullet2.transform.rotation = Quaternion.identity;
            bullet2.SetDirectionAndSpeed();

            shootTimer = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            collision.GetComponent<PlayerStats>().PlayerTakeDamage(damage, this.transform);
            Instantiate(explosionPrefab, transform.position, transform.rotation);

            if (Time.timeScale != Constants.deathSpeed())
            {
                if (gameObject.activeSelf)
                {
                    referencePool.Release(this);
                }
            }
        }
    }


    public override void HurtSequence() {
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Dmg")) {
            return;
        }
        anim.SetTrigger("DamageTrigger");
    }
    public override void DeathSequence()
    {
        base.DeathSequence();
        WinCondition.GetInstance()?.RemoveEnemy(this.gameObject);
        Instantiate(explosionPrefab, transform.position, transform.rotation);
    }

    private void OnDisable()
    {
        WinCondition.GetInstance()?.RemoveEnemy(this.gameObject);
    }

    private void OnDestroy()
    {
        WinCondition.GetInstance()?.RemoveEnemy(this.gameObject);
    }

    private void OnBecameInvisible()
    {
        if (rb.linearVelocity.x < 0) { 
            rb.linearVelocity = Vector2.right * Mathf.Abs(rb.linearVelocity.x);
        } else {
            rb.linearVelocity = Vector2.left * Mathf.Abs(rb.linearVelocity.x);
        }
    }


    private IEnumerator WaitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }


}
