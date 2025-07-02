using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    private float timer=0f;
    private int i;

    private Camera mainCam;
    private float maxLeft;
    private float maxRight;
    private float yPos;
    [SerializeField] private GameObject[] enemy;
    private float enemyTimer;
    [Space(15)]
    [SerializeField] private float spawnTime;
    [Header("BOSS")]
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private WinCondition winCondition;
    [SerializeField] private CrazyBossStats crazyEnemyPrefab;
    GameObject boss = null;


    private void Start()
    {
        mainCam = Camera.main;
        if (Constants.getLevel() % 10 == 9)
        {
            gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(SetBoundaries());
        }
        Vector2 spawnPos = mainCam.ViewportToWorldPoint(new Vector2(0.5f, 1.2f));
        boss=Instantiate(bossPrefab, spawnPos, Quaternion.identity);
        boss.SetActive(false);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnTime){
            i = Random.Range(0, enemy.Length);
            int j = Random.Range(0, 999);
            if
            (Constants.isSurvivalMode() && j > 995)
                {
                    Vector2 spawnPos = mainCam.ViewportToWorldPoint(new Vector2(0.5f, 1.2f));
                    boss.SetActive(true);
                    boss.transform.position = spawnPos;
                    boss = Instantiate(bossPrefab, spawnPos, Quaternion.identity);
                    boss.SetActive(false);
                }else
            if ((Constants.isSurvivalMode()&& j < 50)||(Constants.getLevel() % 10 != 3 && Constants.getLevel() % 10 != 4 && (Constants.getLevel() % 10 == 7||j<Constants.getLevelDecay(2,10,0.1f)))) {
                CrazyBossStats crazyEnemy = Instantiate(crazyEnemyPrefab, new Vector3(UnityEngine.Random.Range(maxLeft, maxRight), yPos, -5), Quaternion.identity);
                crazyEnemy.gameObject.SetActive(true);
                crazyEnemy.GetComponent<SpriteRenderer>().color = Constants.GetThisLevelGreenEnemyColor();
            }
            else
            if ((Constants.isSurvivalMode() && i == 0) || ((i == 0 || Constants.getLevel() % 10 == 3 || Constants.getLevel() % 10 == 1) && Constants.getLevel() % 10 != 4 && Constants.getLevel() % 10 != 8))
            {
                PoolGreenEnemies.getInstance().InitVariables(maxLeft, maxRight, yPos);
                GreenEnemy enemy=PoolGreenEnemies.getInstance().GetFromPool();
                enemy.GetComponent<SpriteRenderer>().color = Constants.GetThisLevelGreenEnemyColor();
            }
            else
            {
                PoolPurpleEnemies.getInstance().InitVariables(maxLeft, maxRight, yPos);
                PurpleEnemy enemy=PoolPurpleEnemies.getInstance().GetFromPool();
                enemy.GetComponent<SpriteRenderer>().color = Constants.GetThisLevelPurpleEnemyColor();

            }
            timer = 0;
            spawnTime = Random.Range(0.1f+ (0.5f - 0.1f) * Mathf.Exp(-0.0009f * Constants.getLevel()), 0.1f + (4f - 0.1f) * Mathf.Exp(-0.0009f * Constants.getLevel()));
            spawnTime = spawnTime / Mathf.Max(1, Mathf.Log(Constants.getLevel(), 100));
            if (Constants.getLevel() % 10 == 1)
            {
                spawnTime /= 1.5f;
            }
            else
                if (Constants.getLevel() % 10 == 3)
            {
                spawnTime /= 7f;
            }
            else
            if (Constants.getLevel() % 10 == 4)
            {
                spawnTime /= 1.68f;
            }
            else
            if (Constants.getLevel() % 10 == 7)
            {
                spawnTime += 1.6f;
            }
            spawnTime = Mathf.Pow(500,10)/(float)Mathf.Pow(Constants.getDifficultyAsLong(),10) * spawnTime;
            if (Constants.isSurvivalMode()) { 
                
            }
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

    private void OnDisable()
    {
        if (!winCondition.canSpawnBoss) {
            Debug.Log("cannot spawn boss");
            return;
        }
        Debug.Log("can spawn boss");
        if (bossPrefab != null)
        {
            Debug.Log("can spawn boss and prefab not null");
            if (boss != null)
            {
                boss.SetActive(true);
            }
        }
    }

}
