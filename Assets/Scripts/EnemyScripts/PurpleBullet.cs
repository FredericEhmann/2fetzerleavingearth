using UnityEngine;
using UnityEngine.Pool;

public class PurpleBullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private Rigidbody2D rb;
    private ObjectPool<PurpleBullet> referencePool;


    public void ChangeDamageTo(float newDamage)
    {
        damage = newDamage;
    }
    private void Start()
    {
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.forward * speed;
        speed = speed * Random.Range(0.9f, 1.1f) + Mathf.Max(0, Mathf.Log(Constants.getLevel()));
        damage = damage * Random.Range(0.9f, 1.1f)+Mathf.Max(0,Mathf.Log(Constants.getLevel()));

    }

    public void SetPool(ObjectPool<PurpleBullet> pool)
    {
        this.referencePool = pool;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) { 
        collision.GetComponent<PlayerStats>().PlayerTakeDamage(damage,this.transform);
            if (gameObject.activeSelf)
            {
                if (Time.timeScale != Constants.deathSpeed())
                {
                    referencePool.Release(this);
                }
            }
        }   
    }

    public void SetDirectionAndSpeed()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Quaternion.Inverse(transform.rotation) * Vector2.down * speed;
        speed = speed * Random.Range(0.9f, 1.1f) + Mathf.Max(0, Mathf.Log(Constants.getLevel(),100));
        damage = damage * Random.Range(0.9f, 1.1f) + Mathf.Max(0, Mathf.Log(Constants.getLevel(),2));

    }
    private void OnBecameInvisible()
    {
        if (gameObject.activeSelf)
        {
            referencePool.Release(this);
        }
    }
    private void OnDisable()
    {
        damage = 0.5f;
        speed = 9;
        transform.rotation = Quaternion.identity;
    }

    internal void SetSpeed(float v)
    {
        speed = v;
    }
}
