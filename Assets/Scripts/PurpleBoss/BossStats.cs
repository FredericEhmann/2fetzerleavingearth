using UnityEngine;

public class BossStats : Enemy
{
    [SerializeField] private BossController bossController;
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
        base.DeathSequence();
        bossController.ChangeState(BossState.death);
        Instantiate(explosionPrefab, transform.position, Quaternion.Euler(0, 0 , Random.Range(0, 360)));
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            collision.GetComponent<PlayerStats>().PlayerTakeDamage(damage,this.transform);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health += Constants.getLevel()*3;
        chanceToDrop = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
