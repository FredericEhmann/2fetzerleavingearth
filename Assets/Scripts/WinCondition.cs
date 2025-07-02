using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class WinCondition : MonoBehaviour
{

    float timer;
    [SerializeField] private float possibleWinTime;
    [SerializeField] private float startWinTime;
    [SerializeField] private GameObject[] spawner;
    [SerializeField] private TextMeshProUGUI secondsLeft;
    [SerializeField] private Boolean hasBoss;
    public bool canSpawnBoss = false;
    private static WinCondition instance = null;
    private HashSet<GameObject> enemies = new HashSet<GameObject>();

    public static WinCondition GetInstance()
    {
        return instance;
    }

    public void AddEnemy(GameObject enemy) {
        enemies.Add(enemy);
    }

    public void RemoveEnemy(GameObject enemy) {
        enemies.Remove(enemy);
    }
    public bool StillHasEnemies() {
        foreach (GameObject enemy in enemies) {
            if (enemy != null && enemy.activeSelf) {
                return true;
            }
        }
        return false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        FinishThem.GetInstance().Hide();
        BossIncoming.GetInstance().Hide();
        possibleWinTime += (0.05f * Constants.getLevel())+Mathf.Log(Constants.getLevel(),2);
        if (Constants.getLevel() % 10 == 3 || Constants.getLevel() % 10 == 4 || Constants.getLevel() % 10 == 7)
        {
            possibleWinTime += 10 + (Mathf.Log(Constants.getLevel(), 10)/4f);
            possibleWinTime /= 1.5f;
        }
        if (Constants.getLevel() % 10 == 1 || Constants.getLevel() % 10 == 2 || Constants.getLevel() % 10 == 6 || Constants.getLevel() % 10 == 8 || Constants.getLevel() % 10 == 9)
        {
            possibleWinTime += 4+Mathf.Log(Constants.getLevel(),10);
            possibleWinTime *= 1.1f;
        }

        if ((Constants.getLevel()%5) != 0) {
            hasBoss = false;
        }
        possibleWinTime *= Constants.GetGameSpeed();
        possibleWinTime -= 4;
        possibleWinTime = 5+(((float)Mathf.Pow(Constants.getDifficultyAsLong(),10) / Mathf.Pow(500,10)) * possibleWinTime);
        startWinTime = possibleWinTime;
        Time.timeScale = Constants.GetGameSpeed();
        if (Constants.getGameMode().Equals("Survival"))
        {
            possibleWinTime = 3600;
            startWinTime = possibleWinTime;
        }
        if (Constants.getGameMode().Equals("Story"))
        {
            possibleWinTime = 3600;
            startWinTime = possibleWinTime;
        }

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float winTime=((possibleWinTime - timer) + 1);
        String secondsL = (int)winTime+"."+(int)((winTime*10)%10)+"";
        if (secondsL.StartsWith("0.") || secondsL.StartsWith("1.")) {
            secondsL = "!!!";
            if (!hasBoss)
            {
                if (!EndGameManager.getInstance().gameOver)
                {
                    FinishThem.GetInstance().Show();
                }
                //TODO SHOW LINES TO ALL ENEMIES FROM FINISH THEM?
            }
            else {
                if (!EndGameManager.getInstance().gameOver)
                {
                    BossIncoming.GetInstance().Show();
                    BossStats[] bosses = (BossStats[])FindObjectsByType(typeof(BossStats), FindObjectsSortMode.None);
                    for (int i = 0; i < bosses.Count(); i++)
                    {
                        //TODO SHOW LINES TO ALL BOSSES
                    }
                }
            }
        }
        secondsLeft.text="Seconds: "+secondsL;
        if (timer >= possibleWinTime)
        {
            EndGameManager.getInstance().EndLevelMusic();
            EndGameManager.getInstance().possibleWin = true;
            if (!hasBoss)
            {
                if (!EndGameManager.getInstance().gameOver)
                {
                    EndGameManager.getInstance().StartResolveSequence();
                }
            }
            else
            {
                canSpawnBoss = true;
            }
            for (int i = 0; i < spawner.Length; i++)
            {
                spawner[i].SetActive(false);
            }
            
            gameObject.SetActive(false);

            //create a function that will check if the player survived the last spawned enemy/meteor
            // win or lose screen
            // GAME MANAGER
        }
        
        void OnDestroy(){
            instance = null;
        }

    }

    internal long GetSurvivalLevel()
    {
        return 1+(long)timer;
    }
}
