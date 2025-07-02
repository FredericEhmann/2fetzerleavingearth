using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SpecialBullet : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private GameObject miniBullet;
    [SerializeField] private Transform[] spawnPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearVelocity = Vector2.down * speed;
        damage += Mathf.Max(0, Mathf.Log(Constants.getLevel(),2));
        StartCoroutine(Explode());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);

    }

    IEnumerator Explode() {
        float randomExplodeTime = UnityEngine.Random.Range(1.5f, 2.5f);
        yield return new WaitForSeconds(randomExplodeTime);
        for (int i = 0; i < spawnPoint.Length; i++) {
            PurpleBullet bullet = PoolPurpleBullets.getInstance().GetFromPool();
            bullet.ChangeDamageTo(4);
            bullet.transform.localScale = new Vector3(2f, 2f, 2f);
            bullet.transform.position = spawnPoint[i].position;
            bullet.transform.rotation = spawnPoint[i].rotation;
            bullet.SetDirectionAndSpeed();
            bullet.SetSpeed(3+Constants.getLevelDecay(0,10,0.01f));
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            collision.GetComponent<PlayerStats>().PlayerTakeDamage(damage,this.transform);

            if (Time.timeScale != Constants.deathSpeed())
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
