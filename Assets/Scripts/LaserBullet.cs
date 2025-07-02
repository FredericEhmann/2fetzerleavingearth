using UnityEngine;
using UnityEngine.Pool;

public class LaserBullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private Rigidbody2D rb;
    private ObjectPool<LaserBullet> referencePool;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        rb.linearVelocity = transform.up * speed;
        speed = speed * Random.Range(0.9f, 1.1f);
        damage = damage * Random.Range(0.68f, 1.1f+ (Random.Range(0, Mathf.Max(0, Mathf.Log(Constants.getUpgradeLevel()))) / 20));
    }
    public void SetPool(ObjectPool<LaserBullet> pool) {
        this.referencePool = pool;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        if (gameObject.activeSelf)
        {
            referencePool.Release(this);
        }
    }
    public void SetDirectionAndSpeed() {
        rb.linearVelocity = transform.up * speed;
        speed = speed * Random.Range(0.9f, 1.1f);
        damage = damage * Random.Range(0.68f, 1.1f + (Random.Range(0, Mathf.Max(0, Mathf.Log(Constants.getUpgradeLevel()))) / 20));
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
        speed = 15;
        damage = 1;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
