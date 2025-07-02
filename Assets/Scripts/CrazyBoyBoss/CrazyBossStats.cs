using System;
using UnityEngine;

public class CrazyBossStats : Enemy
{
    [SerializeField] private CrazyBossController crazyBossController;
    [SerializeField] protected Animator anim;
    public override void HurtSequence()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Dmg")) {
            return;
        }
        anim.SetTrigger("DamageTrigger");
    }

    public override void DeathSequence()
    {
        try
        {
            WinCondition.GetInstance()?.RemoveEnemy(this.gameObject);
            Destroy(GetComponent<CrazyBossEnter>().s1.gameObject);
            Destroy(GetComponent<CrazyBossEnter>().s2.gameObject);
            Destroy(GetComponent<CrazyBossEnter>().s3.gameObject);
            Destroy(GetComponent<CrazyBossEnter>().s4.gameObject);
            Destroy(GetComponent<CrazyBossEnter>().s5.gameObject);
            Destroy(GetComponent<CrazyBossEnter>().lineRenderer.gameObject);
            Destroy(GetComponent<CrazyBossEnter>().lineRenderer2.gameObject);
            Instantiate(explosionPrefab, GetComponent<CrazyBossEnter>().s1.transform.position, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360)));
            Instantiate(explosionPrefab, GetComponent<CrazyBossEnter>().s2.transform.position, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360)));
            Instantiate(explosionPrefab, GetComponent<CrazyBossEnter>().s3.transform.position, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360)));
            Instantiate(explosionPrefab, GetComponent<CrazyBossEnter>().s4.transform.position, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360)));
            Instantiate(explosionPrefab, GetComponent<CrazyBossEnter>().s5.transform.position, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360)));
        }
        catch (Exception e) { 
            
        }
        base.DeathSequence();
        crazyBossController.ChangeState(CrazyBossState.death);
        Instantiate(explosionPrefab, transform.position, Quaternion.Euler(0, 0 , UnityEngine.Random.Range(0, 360)));
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            collision.GetComponent<PlayerStats>().PlayerTakeDamage(damage,this.transform);

            if (Time.timeScale != Constants.deathSpeed())
            {
                Destroy(gameObject);
            WinCondition.GetInstance()?.RemoveEnemy(this.gameObject);
            try
            {
                Destroy(GetComponent<CrazyBossEnter>().s1.gameObject);
                Destroy(GetComponent<CrazyBossEnter>().s2.gameObject);
                Destroy(GetComponent<CrazyBossEnter>().s3.gameObject);
                Destroy(GetComponent<CrazyBossEnter>().s4.gameObject);
                Destroy(GetComponent<CrazyBossEnter>().s5.gameObject);
                Destroy(GetComponent<CrazyBossEnter>().lineRenderer.gameObject);
                Destroy(GetComponent<CrazyBossEnter>().lineRenderer2.gameObject);
                Instantiate(explosionPrefab, GetComponent<CrazyBossEnter>().s1.transform.position, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360)));
                Instantiate(explosionPrefab, GetComponent<CrazyBossEnter>().s2.transform.position, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360)));
                Instantiate(explosionPrefab, GetComponent<CrazyBossEnter>().s3.transform.position, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360)));
                Instantiate(explosionPrefab, GetComponent<CrazyBossEnter>().s4.transform.position, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360)));
                Instantiate(explosionPrefab, GetComponent<CrazyBossEnter>().s5.transform.position, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360)));
            }
            catch (Exception e)
            {

            }
            }

            Instantiate(explosionPrefab, transform.position, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360)));
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        WinCondition.GetInstance()?.AddEnemy(this.gameObject);
        chanceToDrop = 1 / Constants.getUpgradeDecay(1,0.4f,0.001f);
        health += Mathf.Log(Constants.getLevel(), 2)+4.5f;
        damage += Mathf.Log(Constants.getLevel(), 2);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 200 * Time.deltaTime);
    }
}
