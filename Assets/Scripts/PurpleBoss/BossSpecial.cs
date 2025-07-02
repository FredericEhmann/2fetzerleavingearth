using System.Collections;
using UnityEngine;

public class BossSpecial : BossBaseState
{

    [SerializeField] private float speed;
    [SerializeField] private float waitTime;
    [SerializeField] private GameObject specialBullet;
    [SerializeField] private Transform shootingPoint;
    private Vector2 targetPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        targetPoint = mainCam.ViewportToWorldPoint(new Vector3(0.5f, 0.9f));
        speed = 4+Mathf.Log(Constants.getLevel(), 10);
        waitTime = Constants.getLevelDecay(4, 0.01f, 0.01f);
    }

    public override void RunState()
    {
        StartCoroutine(RunSpecialState());
    }

    public override void StopState()
    {
        base.StopState();
    }
    IEnumerator RunSpecialState() {
        while (Vector2.Distance(transform.position, targetPoint)> 0.01f) 
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        Instantiate(specialBullet, shootingPoint.position, Quaternion.identity);
        yield return new WaitForSeconds(waitTime);
        bossController.ChangeState(BossState.fire);
    }

}
