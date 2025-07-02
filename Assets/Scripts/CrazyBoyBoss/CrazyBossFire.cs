using System.Collections;
using System.Net;
using System.Security.Cryptography;
using JetBrains.Annotations;
using UnityEngine;

public class CrazyBossFire : CrazyBossBaseState
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
    private Vector2 targetPoint9;
    private Vector2 targetPoint10;


    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        targetPoint1 = mainCam.ViewportToWorldPoint(new Vector3(0.9f, 0.9f));
        targetPoint2 = mainCam.ViewportToWorldPoint(new Vector3(0.9f, 0.1f));
        targetPoint3 = mainCam.ViewportToWorldPoint(new Vector3(0.1f, 0.1f));
        targetPoint4 = mainCam.ViewportToWorldPoint(new Vector3(0.1f, 0.9f));
        targetPoint5 = mainCam.ViewportToWorldPoint(new Vector3(Random.value, Random.value));
        targetPoint6 = mainCam.ViewportToWorldPoint(new Vector3(Random.value, Random.value));
        targetPoint7 = mainCam.ViewportToWorldPoint(new Vector3(Random.value, Random.value));
        targetPoint8 = mainCam.ViewportToWorldPoint(new Vector3(Random.value, Random.value));
        targetPoint9 = mainCam.ViewportToWorldPoint(new Vector3(0.2f, 0.2f));
        targetPoint10 = mainCam.ViewportToWorldPoint(new Vector3(0.8f, 0.2f));
    }
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
        GetComponent<CrazyBossEnter>().lineRenderer.gameObject.SetActive(true);
        GetComponent<CrazyBossEnter>().lineRenderer2.gameObject.SetActive(true);
        yield return MoveTo(targetPoint1);
        yield return MoveTo(targetPoint2);
        yield return MoveTo(targetPoint3);
        yield return MoveTo(targetPoint4);
        yield return MoveTo(targetPoint5);
        yield return MoveTo(targetPoint6);
        yield return MoveTo(targetPoint7);
        yield return MoveTo(targetPoint8);
        yield return MoveTo(targetPoint9);
        yield return MoveTo(targetPoint10);
        GetComponent<CrazyBossEnter>().lineRenderer.gameObject.SetActive(false);
        GetComponent<CrazyBossEnter>().lineRenderer2.gameObject.SetActive(false);
        bossController.ChangeState(CrazyBossState.fire);
    }

    private IEnumerator MoveTo(Vector2 tp)
    {
        PlayerStats player = PlayerStats.GetInstance();
        float beforeY = transform.position.y;
        float beforeX = transform.position.x;
        float f =Random.Range(0f, 1f);
        if (f < 0.2f) { 
            tp=new Vector3(Random.value, Random.value);
        }else if (f < 0.35f)
        {
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
        GetComponent<CrazyBossEnter>().lineRenderer.startColor = new Color(0, 0, 0, 0.2f);
        GetComponent<CrazyBossEnter>().lineRenderer.endColor = new Color(0.1f, 0.1f, 0.1f, 0.6f);
        GetComponent<CrazyBossEnter>().lineRenderer2.startColor = new Color(0.7f, 0.7f, 0.7f, 0.6f);
        GetComponent<CrazyBossEnter>().lineRenderer2.endColor = new Color(0.7f, 0.7f, 0.7f, 0.2f);
        while (Vector2.Distance(GetComponent<CrazyBossEnter>().s3.position, tp) > 0.01f)
        {
            Constants.MoveShadowBoy(GetComponent<CrazyBossEnter>().s3, tp, (Mathf.Min(Vector2.Distance(GetComponent<CrazyBossEnter>().s3.position, tp), Vector2.Distance(GetComponent<CrazyBossEnter>().s3.position, new Vector2(beforeX, beforeY))) * (speed) * (0.1f + Mathf.Log(Constants.getLevel(), 50))+(0.1f * (Vector2.Distance(tp, new Vector2(beforeX, beforeY)))))*2);
            Constants.MoveShadowBoy(GetComponent<CrazyBossEnter>().s4, tp, (Mathf.Min(Vector2.Distance(GetComponent<CrazyBossEnter>().s4.position, tp), Vector2.Distance(GetComponent<CrazyBossEnter>().s4.position, new Vector2(beforeX, beforeY))) * (speed) * (0.2f + Mathf.Log(Constants.getLevel(), 50))+(0.2f * (Vector2.Distance(tp, new Vector2(beforeX, beforeY)))))*6);
            Constants.MoveShadowBoy(GetComponent<CrazyBossEnter>().s5, tp, (Mathf.Min(Vector2.Distance(GetComponent<CrazyBossEnter>().s5.position, tp), Vector2.Distance(GetComponent<CrazyBossEnter>().s5.position, new Vector2(beforeX, beforeY))) * (speed) * (0.3f + Mathf.Log(Constants.getLevel(), 50))+(0.3f * (Vector2.Distance(tp, new Vector2(beforeX, beforeY)))))*10);
            GetComponent<CrazyBossEnter>().s1.rotation = transform.rotation;
            GetComponent<CrazyBossEnter>().s2.rotation = transform.rotation;
            GetComponent<CrazyBossEnter>().s3.rotation = transform.rotation;
            GetComponent<CrazyBossEnter>().s4.rotation = transform.rotation;
            GetComponent<CrazyBossEnter>().s5.rotation = transform.rotation;
            GetComponent<CrazyBossEnter>().lineRenderer.SetPosition(0, transform.position);
            GetComponent<CrazyBossEnter>().lineRenderer.SetPosition(1, GetComponent<CrazyBossEnter>().s1.position);
            GetComponent<CrazyBossEnter>().lineRenderer.SetPosition(2, GetComponent<CrazyBossEnter>().s2.position);
            GetComponent<CrazyBossEnter>().lineRenderer.SetPosition(3, GetComponent<CrazyBossEnter>().s3.position);
            GetComponent<CrazyBossEnter>().lineRenderer.SetPosition(4, GetComponent<CrazyBossEnter>().s4.position);
            GetComponent<CrazyBossEnter>().lineRenderer.SetPosition(5, GetComponent<CrazyBossEnter>().s5.position);
            GetComponent<CrazyBossEnter>().lineRenderer2.SetPosition(0, transform.position);
            GetComponent<CrazyBossEnter>().lineRenderer2.SetPosition(1, GetComponent<CrazyBossEnter>().s1.position);
            GetComponent<CrazyBossEnter>().lineRenderer2.SetPosition(2, GetComponent<CrazyBossEnter>().s2.position);
            GetComponent<CrazyBossEnter>().lineRenderer2.SetPosition(3, GetComponent<CrazyBossEnter>().s3.position);
            GetComponent<CrazyBossEnter>().lineRenderer2.SetPosition(4, GetComponent<CrazyBossEnter>().s4.position);
            GetComponent<CrazyBossEnter>().lineRenderer2.SetPosition(5, GetComponent<CrazyBossEnter>().s5.position);

            yield return new WaitForEndOfFrame();
        }
        GetComponent<CrazyBossEnter>().sourceArrive.pitch = Random.Range(5f, 9f);
        GetComponent<CrazyBossEnter>().sourceArrive.volume = Random.Range(0.01f, 0.05f);
        GetComponent<CrazyBossEnter>().sourceArrive.Play();
        yield return new WaitForSeconds(0.001f);
        while (Vector2.Distance(transform.position, tp) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, tp, (Mathf.Min(Vector2.Distance(transform.position, tp), Vector2.Distance(transform.position, new Vector2(beforeX, beforeY)))*((speed) * (0.1f+ Mathf.Log(Constants.getLevel(), 50)))+(0.2f * (Vector2.Distance(tp, new Vector2(beforeX, beforeY))))/40f) * Time.deltaTime);
            Constants.MoveShadowBoy(GetComponent<CrazyBossEnter>().s1, tp, (Mathf.Min(Vector2.Distance(GetComponent<CrazyBossEnter>().s1.position, tp), Vector2.Distance(GetComponent<CrazyBossEnter>().s1.position, new Vector2(beforeX, beforeY)))* ((speed) * (0.2f + Mathf.Log(Constants.getLevel(), 50))))+(0.25f * (Vector2.Distance(tp, new Vector2(beforeX, beforeY))))/30f);
            Constants.MoveShadowBoy(GetComponent<CrazyBossEnter>().s2, tp, (Mathf.Min(Vector2.Distance(GetComponent<CrazyBossEnter>().s2.position, tp), Vector2.Distance(GetComponent<CrazyBossEnter>().s2.position, new Vector2(beforeX, beforeY)))*((speed) * (0.3f + Mathf.Log(Constants.getLevel(), 50))))+(0.3f* (Vector2.Distance(tp, new Vector2(beforeX, beforeY))))/20f);
            GetComponent<CrazyBossEnter>().s1.rotation = transform.rotation;
            GetComponent<CrazyBossEnter>().s2.rotation = transform.rotation;
            GetComponent<CrazyBossEnter>().s3.rotation = transform.rotation;
            GetComponent<CrazyBossEnter>().s4.rotation = transform.rotation;
            GetComponent<CrazyBossEnter>().s5.rotation = transform.rotation;
            GetComponent<CrazyBossEnter>().lineRenderer.SetPosition(0, transform.position);
            GetComponent<CrazyBossEnter>().lineRenderer.SetPosition(1, GetComponent<CrazyBossEnter>().s1.position);
            GetComponent<CrazyBossEnter>().lineRenderer.SetPosition(2, GetComponent<CrazyBossEnter>().s2.position);
            GetComponent<CrazyBossEnter>().lineRenderer.SetPosition(3, GetComponent<CrazyBossEnter>().s3.position);
            GetComponent<CrazyBossEnter>().lineRenderer.SetPosition(4, GetComponent<CrazyBossEnter>().s4.position);
            GetComponent<CrazyBossEnter>().lineRenderer.SetPosition(5, GetComponent<CrazyBossEnter>().s5.position);
            GetComponent<CrazyBossEnter>().lineRenderer2.SetPosition(0, transform.position);
            GetComponent<CrazyBossEnter>().lineRenderer2.SetPosition(1, GetComponent<CrazyBossEnter>().s1.position);
            GetComponent<CrazyBossEnter>().lineRenderer2.SetPosition(2, GetComponent<CrazyBossEnter>().s2.position);
            GetComponent<CrazyBossEnter>().lineRenderer2.SetPosition(3, GetComponent<CrazyBossEnter>().s3.position);
            GetComponent<CrazyBossEnter>().lineRenderer2.SetPosition(4, GetComponent<CrazyBossEnter>().s4.position);
            GetComponent<CrazyBossEnter>().lineRenderer2.SetPosition(5, GetComponent<CrazyBossEnter>().s5.position);
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
