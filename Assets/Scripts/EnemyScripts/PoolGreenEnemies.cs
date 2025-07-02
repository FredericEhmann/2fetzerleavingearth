using System;
using UnityEngine;
using UnityEngine.Pool;

public class PoolGreenEnemies : MonoBehaviour { 

    [SerializeField] private GreenEnemy GreenEnemyPrefab;
    private ObjectPool<GreenEnemy> pool;
    private static PoolGreenEnemies instance;
    private float maxLeft;
    private float maxRight;
    private float yPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null)
        {
            pool = new ObjectPool<GreenEnemy>(CreatePoolObj, OnTakeGreenEnemyFromPool, OnReturnGreenEnemyFromPool, OnDestroyPoolObj, true, 10, 50);
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
            
    }

    public static PoolGreenEnemies getInstance() {
        return instance;
    }

    
    public GreenEnemy GetFromPool()
    {
        GreenEnemy b = pool.Get();
        if (b == null || !b.isActiveAndEnabled)
        {
            return CreatePoolObj();
        }
        return b;
    }
    private GreenEnemy CreatePoolObj()
    {
        GreenEnemy greenEnemy = Instantiate(GreenEnemyPrefab, new Vector3(UnityEngine.Random.Range(maxLeft, maxRight), yPos, -5), Quaternion.identity);
        float size = UnityEngine.Random.Range(0.8f, 1.4f);
        greenEnemy.transform.localScale = new Vector3(size, size, 1);
        greenEnemy.SetPool(pool);
        greenEnemy.gameObject.SetActive(true);
        return greenEnemy;
    }
    private void OnDestroyPoolObj(GreenEnemy greenEnemy)
    {
        pool.Release(greenEnemy);
        Destroy(greenEnemy.gameObject);
    }
    private void OnTakeGreenEnemyFromPool(GreenEnemy greenEnemy)
    {
        if (greenEnemy != null && greenEnemy.gameObject.activeSelf)
        {
            greenEnemy.gameObject.SetActive(true);
        }
    }
    private void OnReturnGreenEnemyFromPool(GreenEnemy greenEnemy)
    {
        greenEnemy.gameObject.SetActive(false);
    }

    public void ReleaseFromPool(GreenEnemy greenEnemy)
    {
        pool.Release(greenEnemy);
    }
    internal void InitVariables(float maxLeft, float maxRight, float yPos)
    {
        this.maxLeft = maxLeft;
        this.maxRight = maxRight;
        this.yPos = yPos;
    }
}
