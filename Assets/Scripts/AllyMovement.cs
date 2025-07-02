using System.Collections;
using UnityEditor.Tilemaps;
using UnityEngine;

public class AllyMovement : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        StartCoroutine(MoveToStart());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator MoveToStart()
    {
        yield return new WaitForSeconds(1f);
        yield return MoveTo();
    }
    private IEnumerator MoveTo()
    {
        float beforeX = transform.position.x;
        float beforeY = transform.position.y;
        Vector3 p=PlayerStats.GetInstance().transform.position;
        p.y -= 20 + Random.Range(-16, 100);
        p.x = Random.Range(-200, 200);
        Vector3 tp = p;
        float speed = 8;
        while (Vector2.Distance(GetComponent<CrazyBossEnter>().s3.position, tp) > 0.01f){
            transform.position = Vector2.MoveTowards(transform.position, tp, (Mathf.Min(Vector2.Distance(transform.position, tp), Vector2.Distance(transform.position, new Vector2(beforeX, beforeY))) * ((speed) * (0.1f + Mathf.Log(Constants.getLevel(), 50))) + (0.2f * (Vector2.Distance(tp, new Vector2(beforeX, beforeY)))) / 40f) * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        MoveTo();
    }
}
