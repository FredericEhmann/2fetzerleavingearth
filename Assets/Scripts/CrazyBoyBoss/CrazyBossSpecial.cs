using System.Collections;
using UnityEngine;

public class CrazyBossSpecial : CrazyBossBaseState
{

    [SerializeField] private float speed;
    private Vector2 targetPoint1;
    private Vector2 targetPoint2;
    private Vector2 targetPoint3;
    private Vector2 targetPoint4;
    private Vector2 targetPoint5;
    private Vector2 targetPoint6;
    private Vector2 targetPoint7;
    private Vector2 targetPoint8;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        targetPoint1 = mainCam.ViewportToWorldPoint(new Vector3(0.1f, 0.9f));
        targetPoint2 = mainCam.ViewportToWorldPoint(new Vector3(0.1f, 0.1f));
        targetPoint3 = mainCam.ViewportToWorldPoint(new Vector3(0.9f, 0.1f));
        targetPoint4 = mainCam.ViewportToWorldPoint(new Vector3(0.9f, 0.9f));
        targetPoint5 = mainCam.ViewportToWorldPoint(new Vector3(Random.value, Random.value));
        targetPoint6 = mainCam.ViewportToWorldPoint(new Vector3(Random.value, Random.value));
        targetPoint7 = mainCam.ViewportToWorldPoint(new Vector3(Random.value, Random.value));
        targetPoint8 = mainCam.ViewportToWorldPoint(new Vector3(Random.value, Random.value));
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
        yield return MoveTo(targetPoint1);
        yield return MoveTo(targetPoint2);
        yield return MoveTo(targetPoint3);
        yield return MoveTo(targetPoint4);
        yield return MoveTo(targetPoint5);
        yield return MoveTo(targetPoint6);
        yield return MoveTo(targetPoint7);
        yield return MoveTo(targetPoint8);
        bossController.ChangeState(CrazyBossState.fire);
    }

    private IEnumerator MoveTo(Vector2 tp)
    {
        float f = Random.Range(0f, 1f);
        if (f < 0.1f)
        {
            tp = new Vector3(Random.value, Random.value);
        }
        else if (f < 0.7f)
        {
            PlayerStats player = PlayerStats.GetInstance();
            if (player != null && player.gameObject != null && player.gameObject.activeSelf)
            {
                tp = new Vector3(player.transform.position.x, player.transform.position.y);
            }
        }
        GetComponent<CrazyBossEnter>().sourceSplit.pitch = Random.Range(0.5f, 1.5f);
        GetComponent<CrazyBossEnter>().sourceSplit.volume = Random.Range(0.01f, 0.2f);
        GetComponent<CrazyBossEnter>().sourceSplit.Play();
        GetComponent<CrazyBossEnter>().s1.gameObject.SetActive(true);
        GetComponent<CrazyBossEnter>().s2.gameObject.SetActive(true);
        GetComponent<CrazyBossEnter>().s3.gameObject.SetActive(true);
        GetComponent<CrazyBossEnter>().s4.gameObject.SetActive(true);
        GetComponent<CrazyBossEnter>().s5.gameObject.SetActive(true);
        GetComponent<CrazyBossEnter>().s1.position = transform.position;
        GetComponent<CrazyBossEnter>().s1.rotation = transform.rotation;
        GetComponent<CrazyBossEnter>().s2.position = transform.position;
        GetComponent<CrazyBossEnter>().s2.rotation = transform.rotation;
        GetComponent<CrazyBossEnter>().s3.position = transform.position;
        GetComponent<CrazyBossEnter>().s3.rotation = transform.rotation;
        GetComponent<CrazyBossEnter>().s4.position = transform.position;
        GetComponent<CrazyBossEnter>().s4.rotation = transform.rotation;
        GetComponent<CrazyBossEnter>().s5.position = transform.position;
        GetComponent<CrazyBossEnter>().s5.rotation = transform.rotation;

        while (Vector2.Distance(GetComponent<CrazyBossEnter>().s3.position, tp) > 0.01f)
        {
            Constants.MoveShadowBoy(GetComponent<CrazyBossEnter>().s3, tp, speed * (1f + Mathf.Log(Constants.getLevel(), 50)));
            Constants.MoveShadowBoy(GetComponent<CrazyBossEnter>().s4, tp, speed * (2f + Mathf.Log(Constants.getLevel(), 50)));
            Constants.MoveShadowBoy(GetComponent<CrazyBossEnter>().s5, tp, speed * (3f + Mathf.Log(Constants.getLevel(), 50)));
            GetComponent<CrazyBossEnter>().s1.rotation = transform.rotation;
            GetComponent<CrazyBossEnter>().s2.rotation = transform.rotation;
            GetComponent<CrazyBossEnter>().s3.rotation = transform.rotation;
            GetComponent<CrazyBossEnter>().s4.rotation = transform.rotation;
            GetComponent<CrazyBossEnter>().s5.rotation = transform.rotation;
            yield return new WaitForEndOfFrame();
        }
        GetComponent<CrazyBossEnter>().sourceArrive.pitch = Random.Range(5f, 9f);
        GetComponent<CrazyBossEnter>().sourceArrive.volume = Random.Range(0.01f, 0.05f);
        GetComponent<CrazyBossEnter>().sourceArrive.Play();
        yield return new WaitForSeconds(0.1f);
        while (Vector2.Distance(transform.position, tp) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, tp, (speed + Mathf.Log(Constants.getLevel(), 50)) * Time.deltaTime);
            Constants.MoveShadowBoy(GetComponent<CrazyBossEnter>().s1, tp, (speed * 2f + Mathf.Log(Constants.getLevel(), 50)));
            Constants.MoveShadowBoy(GetComponent<CrazyBossEnter>().s2, tp, (speed * 3f + Mathf.Log(Constants.getLevel(), 50)));
            GetComponent<CrazyBossEnter>().s1.rotation = transform.rotation;
            GetComponent<CrazyBossEnter>().s2.rotation = transform.rotation;
            GetComponent<CrazyBossEnter>().s3.rotation = transform.rotation;
            GetComponent<CrazyBossEnter>().s4.rotation = transform.rotation;
            GetComponent<CrazyBossEnter>().s5.rotation = transform.rotation;
            yield return new WaitForEndOfFrame();
        }
        GetComponent<CrazyBossEnter>().sourceArrive.pitch = Random.Range(0.5f, 1.5f);
        GetComponent<CrazyBossEnter>().sourceArrive.volume = Random.Range(0.01f, 0.2f);
        GetComponent<CrazyBossEnter>().sourceArrive.Play();
        GetComponent<CrazyBossEnter>().s1.gameObject.SetActive(false);
        GetComponent<CrazyBossEnter>().s2.gameObject.SetActive(false);
        GetComponent<CrazyBossEnter>().s3.gameObject.SetActive(false);
        GetComponent<CrazyBossEnter>().s4.gameObject.SetActive(false);
        GetComponent<CrazyBossEnter>().s5.gameObject.SetActive(false);
    }
}
