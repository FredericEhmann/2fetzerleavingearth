using System.Collections;
using TMPro;
using UnityEngine;

public class BossEnter : BossBaseState
{

    private Vector2 enterPoint;
    [SerializeField] private float speed;
    private void OnEnable()
    {
        transform.position = new Vector3(0, +10, 0);
        if (EndGameManager.getInstance().bossMusic == null) { 
            EndGameManager.getInstance().bossMusic = GetComponent<AudioSource>();
        }
        EndGameManager.getInstance().StartBossMusic();
        base.Start();
        mainCam = Camera.main;
        StartCoroutine(Wait(0.4f));
        enterPoint = mainCam.ViewportToWorldPoint(new Vector2(0.5f, 0.7f));
    }
    private IEnumerator Wait(float f)
    {
        //do something or nothing
        yield return new WaitForSeconds(f);
    }



    public override void RunState()
    {
        StartCoroutine(RunEnterState());
    }

    public override void StopState()
    {
        Debug.Log("StopState called!");
        base.StopState();
    }

    IEnumerator RunEnterState()
    {

        if (enterPoint == null)
        {
            yield break;
        }

        float initialDistance = Vector2.Distance(transform.position, enterPoint);

        while (Vector2.Distance(transform.position, enterPoint) > 0.01f)
        {
            if (speed <= 0)
            {
                yield break;
            }

            transform.position = Vector2.MoveTowards(transform.position, enterPoint, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        bossController.ChangeState(BossState.fire);
    }


    void OnDestroy()
    {
        Debug.Log($"{gameObject.name} was destroyed, stopping coroutine.");
        EndGameManager.getInstance().bossMusic = GetComponent<AudioSource>();
        EndGameManager.getInstance().StopBossMusic();
    }

}
