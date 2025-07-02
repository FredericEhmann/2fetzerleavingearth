using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public class BossFire : BossBaseState
{
    [SerializeField] private float speed;
    [SerializeField] private float shootRate;
    [SerializeField] private GameObject bulletPrefab;
    [Header("Shooting Points")]
    [SerializeField] private Transform[] shootPoint;

    public override void RunState()
    {
        StartCoroutine(RunFireState());
    }
    public override void StopState()
    {
        base.StopState();
    }

    IEnumerator RunFireState()
    {
        EndGameManager.getInstance().registerBossMusic();
        shootRate = Constants.getLevelDecay(2, 0.01f, 0.01f);
        speed = Constants.getLevelDecay(3, 8, 0.01f);
        float shootTimer = 0f;
        float fireStateTimer = 0f;
        float fireStateExitTime = Random.Range(8f, 20f+Mathf.Log(Constants.getLevel(),10));
        Vector2 targetPosition = new Vector2(Random.Range(maxLeft, maxRight), Random.Range(maxDown, maxUp));
        while (fireStateTimer <= fireStateExitTime) {
            if (Vector2.Distance(transform.position, targetPosition) > 0.01f)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            }
            else {
                targetPosition = new Vector2(Random.Range(maxLeft, maxRight), Random.Range(maxDown, maxUp));

            }
            shootTimer += Time.deltaTime;
            if (shootTimer >= shootRate) {
                for (int i = 0; i < shootPoint.Length; i++) {
                    PurpleBullet bullet = PoolPurpleBullets.getInstance().GetFromPool();
                    bullet.ChangeDamageTo(2);
                    bullet.transform.localScale = new Vector3(0.8f,0.8f,0.8f);
                    bullet.transform.position = shootPoint[i].position;
                    bullet.transform.rotation = Quaternion.identity;
                    bullet.SetDirectionAndSpeed();
                }
                shootTimer = 0;
            }
            yield return new WaitForEndOfFrame();
            fireStateTimer += Time.deltaTime;
        }
//        int randomPick = Random.Range(0, 2);
 //       if (randomPick == 0) {
//            bossController.ChangeState(BossState.fire);
//        }
        bossController.ChangeState(BossState.special);
    }



}
