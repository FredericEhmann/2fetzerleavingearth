using System.Collections;
using TMPro;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class CrazyBossEnter : CrazyBossBaseState
{

    private Vector2 enterPoint;
    [SerializeField] private float speed;
    [SerializeField] private Transform shadowboy1;
    [SerializeField] private Transform shadowboy2;
    [SerializeField] private Transform shadowboy3;
    [SerializeField] private Transform shadowboy4;
    [SerializeField] private Transform shadowboy5;
    [SerializeField] public AudioSource sourceArrive;
    [SerializeField] public AudioSource sourceSplit;
    [SerializeField] private LineRenderer lineRendererPrefab;
    internal LineRenderer lineRenderer = null;
    internal LineRenderer lineRenderer2 = null;
    internal Transform s1;
    internal Transform s2;
    internal Transform s3;
    internal Transform s4;
    internal Transform s5;

    protected override void Start()
    {
        base.Start();
        mainCam = Camera.main;
        StartCoroutine(Wait(0.4f));
        enterPoint = mainCam.ViewportToWorldPoint(new Vector2(0.5f, 0.1f));
        if (lineRenderer == null)
        {
            lineRenderer = Instantiate(lineRendererPrefab);
        }
        if (lineRenderer2 == null)
        {
            lineRenderer2 = Instantiate(lineRendererPrefab);
        }
        lineRenderer.gameObject.SetActive(true);
        lineRenderer2.gameObject.SetActive(true);
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

        s1 = Instantiate(shadowboy1, transform.position, transform.rotation);
        s1.GetComponent<SpriteRenderer>().color = new Color(0.4f, 0.1f, 0.9f, 0.3f);
        s2 = Instantiate(shadowboy1, transform.position, transform.rotation);
        s2.GetComponent<SpriteRenderer>().color = new Color(0.9f, 0.4f, 0.1f, 0.25f);
        s3 = Instantiate(shadowboy1, transform.position, transform.rotation);
        s3.GetComponent<SpriteRenderer>().color = new Color(0.1f, 0.9f, 0.4f, 0.2f);
        s4 = Instantiate(shadowboy1, transform.position, transform.rotation);
        s4.GetComponent<SpriteRenderer>().color = new Color(0.9f, 0.0f, 0.4f, 0.15f);
        s5 = Instantiate(shadowboy1, transform.position, transform.rotation);
        s5.GetComponent<SpriteRenderer>().color = new Color(0.1f, 0.4f, 0.9f, 0.1f);
        sourceSplit.pitch = Random.Range(0.5f, 1.5f);
        sourceSplit.volume = Random.Range(0.9f, 1f);
        sourceSplit.Play();
        while (Vector2.Distance(transform.position, enterPoint) > 0.01f)
        {
            if (speed <= 0)
            {
                yield break;
            }

            transform.position = Vector2.MoveTowards(transform.position, enterPoint, speed * Time.deltaTime);
            Constants.MoveShadowBoy(s1, enterPoint, speed*1.1f);
            Constants.MoveShadowBoy(s2, enterPoint, speed * 1.2f);
            Constants.MoveShadowBoy(s3, enterPoint, speed * 1.3f);
            Constants.MoveShadowBoy(s4, enterPoint, speed * 1.4f);
            Constants.MoveShadowBoy(s5, enterPoint, speed * 1.5f);
            GetComponent<CrazyBossEnter>().s1.rotation = transform.rotation;
            GetComponent<CrazyBossEnter>().s2.rotation = transform.rotation;
            GetComponent<CrazyBossEnter>().s3.rotation = transform.rotation;
            GetComponent<CrazyBossEnter>().s4.rotation = transform.rotation;
            GetComponent<CrazyBossEnter>().s5.rotation = transform.rotation;
            yield return new WaitForEndOfFrame();
        }
        sourceArrive.pitch = Random.Range(0.5f, 1.5f);
        sourceArrive.volume = Random.Range(0.9f, 1f);
        sourceArrive.Play();


        s1.gameObject.SetActive(false);
        s2.gameObject.SetActive(false);
        s3.gameObject.SetActive(false);
        s4.gameObject.SetActive(false);
        s5.gameObject.SetActive(false);
        
        bossController.ChangeState(CrazyBossState.fire);
    }


    void OnDestroy()
    {
        Debug.Log($"{gameObject.name} was destroyed, stopping coroutine.");
    }

}
