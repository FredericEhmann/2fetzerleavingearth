using System;
using UnityEngine;
using UnityEngine.Pool;

public class PoolMeteors : MonoBehaviour {

    private ObjectPool<Meteor> pool;
    private static PoolMeteors instance;
    private int i;
    private float maxLeft;
    private float maxRight;
    private float yPos;
    [SerializeField] private Meteor[] meteors;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null)
        {
            pool = new ObjectPool<Meteor>(CreatePoolObj, OnTakeMeteorFromPool, OnReturnMeteorFromPool, OnDestroyPoolObj, true, 10, 50);
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
            
    }

    public static PoolMeteors getInstance() {
        return instance;
    }



    public Meteor GetFromPool()
    {
        Meteor b = pool.Get();
        if (b == null || !b.isActiveAndEnabled)
        {
            return CreatePoolObj();
        }
        return b;
    }
    private Meteor CreatePoolObj()
    {
        i = UnityEngine.Random.Range(0, meteors.Length);
        Meteor meteor = Instantiate(meteors[i], new Vector3(UnityEngine.Random.Range(maxLeft, maxRight), yPos, -5), Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360)));
        meteor.SetPool(pool);
        meteor.gameObject.SetActive(true);
        float size = UnityEngine.Random.Range(0.6f, 1.4f);
        meteor.transform.localScale = new Vector3(size, size, 1);
        WinCondition.GetInstance()?.AddEnemy(meteor.gameObject);
        return meteor;
    }
    private void OnDestroyPoolObj(Meteor meteor)
    {
        pool.Release(meteor);
        Destroy(meteor.gameObject);
        WinCondition.GetInstance()?.RemoveEnemy(meteor.gameObject);
    }
    private void OnTakeMeteorFromPool(Meteor meteor)
    {
        if (meteor != null && meteor.gameObject.activeSelf)
        {
            WinCondition.GetInstance()?.AddEnemy(meteor.gameObject);
            meteor.gameObject.SetActive(true);
        }
    }
    private void OnReturnMeteorFromPool(Meteor meteor)
    {
        meteor.gameObject.SetActive(false);
        WinCondition.GetInstance()?.RemoveEnemy(meteor.gameObject);
    }

    public void ReleaseFromPool(Meteor meteor)
    {
        pool.Release(meteor);
    }

    internal void InitVariables(float maxLeft, float maxRight, float yPos)
    {
        this.maxLeft = maxLeft;
        this.maxRight = maxRight;
        this.yPos = yPos;
    }
}
