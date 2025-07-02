using System;
using UnityEngine;
using UnityEngine.Pool;

public class PoolPurpleBullets : MonoBehaviour
{
    [SerializeField] private PurpleBullet bulletPrefab;
    private ObjectPool<PurpleBullet> pool;
    private static PoolPurpleBullets instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null)
        {
            pool = new ObjectPool<PurpleBullet>(CreatePoolObj, OnTakeBulletFromPool, OnReturnBulletFromPool, OnDestroyPoolObj, true, 10, 50);
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
            
    }

    public static PoolPurpleBullets getInstance() {
        return instance;
    }

    public PurpleBullet GetFromPool()
    {
        PurpleBullet b=pool.Get();
        if (b == null || !b.isActiveAndEnabled)
        {
            return CreatePoolObj();
        }
        return b;
    }
    private PurpleBullet CreatePoolObj()
    {
        PurpleBullet bullet = Instantiate(bulletPrefab);
        bullet.SetPool(pool);
        bullet.gameObject.SetActive(true);
        return bullet;
    }
    private void OnDestroyPoolObj(PurpleBullet bullet)
    {
        pool.Release(bullet);
        Destroy(bullet.gameObject);
    }
    private void OnTakeBulletFromPool(PurpleBullet bullet)
    {
        if (bullet != null && bullet.gameObject.activeSelf)
        {
            bullet.gameObject.SetActive(true);
        }
    }
    private void OnReturnBulletFromPool(PurpleBullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    public void ReleaseFromPool(PurpleBullet bullet)
    {
        pool.Release(bullet);
    }
}
