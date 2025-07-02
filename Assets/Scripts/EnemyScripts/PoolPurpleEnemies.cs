using System;
using UnityEngine;
using UnityEngine.Pool;

public class PoolPurpleEnemies : MonoBehaviour { 

    [SerializeField] private PurpleEnemy PurpleEnemyPrefab;
    private ObjectPool<PurpleEnemy> pool;
    private static PoolPurpleEnemies instance;
    private float maxLeft;
    private float maxRight;
    private float yPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null)
        {
            pool = new ObjectPool<PurpleEnemy>(CreatePoolObj, OnTakePurpleEnemyFromPool, OnReturnPurpleEnemyFromPool, OnDestroyPoolObj, true, 10, 50);
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
            
    }

    public static PoolPurpleEnemies getInstance() {
        return instance;
    }

 

    public PurpleEnemy GetFromPool()
    {
        PurpleEnemy b = pool.Get();
        if (b == null || !b.isActiveAndEnabled)
        {
            return CreatePoolObj();
        }
        return b;
    }
    private PurpleEnemy CreatePoolObj()
    {
        PurpleEnemy purpleEnemy = Instantiate(PurpleEnemyPrefab, new Vector3(UnityEngine.Random.Range(maxLeft, maxRight), yPos, -5), Quaternion.identity);
        float size = UnityEngine.Random.Range(0.6f, 1.4f);
        purpleEnemy.transform.localScale = new Vector3(size, size, 1);
        purpleEnemy.SetPool(pool);
        purpleEnemy.gameObject.SetActive(true);
        return purpleEnemy;
    }
    private void OnDestroyPoolObj(PurpleEnemy purpleEnemy)
    {
        pool.Release(purpleEnemy);
        Destroy(purpleEnemy.gameObject);
    }
    private void OnTakePurpleEnemyFromPool(PurpleEnemy purpleEnemy)
    {
        if (purpleEnemy != null && purpleEnemy.gameObject.activeSelf)
        {
            purpleEnemy.gameObject.SetActive(true);
        }
    }
    private void OnReturnPurpleEnemyFromPool(PurpleEnemy purpleEnemy)
    {
        purpleEnemy.gameObject.SetActive(false);
    }

    public void ReleaseFromPool(PurpleEnemy purpleEnemy)
    {
        pool.Release(purpleEnemy);
    }
    internal void InitVariables(float maxLeft, float maxRight, float yPos)
    {
        this.maxLeft = maxLeft;
        this.maxRight = maxRight;
        this.yPos = yPos;
    }
}
