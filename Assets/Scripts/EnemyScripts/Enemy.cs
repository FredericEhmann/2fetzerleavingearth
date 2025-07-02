using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

// A behaviour that is attached to a playable
public class Enemy : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected float damage;
    [SerializeField] protected GameObject explosionPrefab;
    [SerializeField] protected int score;
    [SerializeField] private ScriptableObjectExample powerUpSpawner;
    protected float chanceToDrop=1f;

    void Start()
    {
        chanceToDrop = 1f / Mathf.Log((Constants.getUpgradeLevel() + (int)(Constants.getHealingBonus() * 30)), 2);

    }
    void Update()
    {
    }

    public void TakeDamage(float dmg) {
        health -= dmg;
        HurtSequence();
        if (health <= 0) {
            DeathSequence();
        }
    }

    public virtual void HurtSequence() { 
    }

    public virtual void DeathSequence() {
        EndGameManager.getInstance().UpdateScore(score);
        Destroy(gameObject);
        if (powerUpSpawner != null)
        {
            powerUpSpawner.SpawnPowerUp(transform.position, chanceToDrop);
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
