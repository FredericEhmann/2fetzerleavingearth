using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{

    [SerializeField] private GameObject[] meteor;
    [SerializeField] private float spawnTime;
    private float timer=0f;
    private int i;

    private Camera mainCam;
    private float maxLeft;
    private float maxRight;
    private float yPos;
    private void Start()
    {
        if (!Constants.isSurvivalMode() && (Constants.getLevel() % 10 == 3 || Constants.getLevel() % 10 == 4 || Constants.getLevel() % 10 == 7))
        {
            gameObject.SetActive(false);
        }
        else
        {
            mainCam = Camera.main;
            StartCoroutine(SetBoundaries());
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
       
        if (timer > spawnTime)
        {
            PoolMeteors.getInstance().InitVariables(maxLeft, maxRight, yPos);
            Meteor meteor=PoolMeteors.getInstance().GetFromPool();
            meteor.GetComponent<SpriteRenderer>().color = Constants.GetThisLevelMeteorColor();
            timer = 0;
            spawnTime = Random.Range(0.01f+ (0.05f - 0.01f) * Mathf.Exp(-0.007f * Constants.getLevel()), 0.01f + (1.5f - 0.1f) * Mathf.Exp(-0.007f * Constants.getLevel()));
            if (Constants.getLevel() % 10 == 9)
            {
                spawnTime /= 2;
            }
            spawnTime = Mathf.Pow(500,10) / (float)Mathf.Pow(Constants.getDifficultyAsLong(),10) * spawnTime;
        }   
    }

    private IEnumerator SetBoundaries()
    {
        //do something or nothing
        yield return new WaitForSeconds(0.4f);
        maxLeft = mainCam.ViewportToWorldPoint(new Vector2(0.15f, 0)).x;
        maxRight = mainCam.ViewportToWorldPoint(new Vector2(0.85f, 0)).x;
        yPos = mainCam.ViewportToWorldPoint(new Vector2(0, 1.1f)).y;

    }


}
