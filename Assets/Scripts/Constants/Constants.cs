using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.SocialPlatforms.Impl;
using System.Collections.Generic;
using System.Linq;

public static class Constants
{
    private const int BASE_GAIN = 30; // Maximum possible MMR gain
    private const int BASE_LOSS = 30; // Maximum possible MMR loss

    public static string levelString = "longLevel";
    public static string highScoreString = "longHighScore";
    public static string survivalHighScoreString = "longHighScoreSurvival";
    public static string survivalHighLevelString = "longHighLevelSurvival";
    public static string lastScoreString = "longLastScore";
    public static string playerShootColorString = "playerShootColor";
    public static string backgroundColorString = "backgroundColor";
    public static string greenEnemyColorString = "greenEnemyColor";
    public static string meteorColorString = "meteorColor";
    public static string purpleEnemyColorString = "PurpleEnemyColor";
    public static string healingBonusString = "healingBonus";
    public static string maxHealthBonusString = "maxHealthBonus";
    public static string upgradeLevelString = "upgradeLevel";
    public static string shieldString = "shield";
    public static string sfxVolumeString = "sfxVolume";
    public static string musicVolumeString = "musicVolume";
    public static string sfxSliderVolumeString = "sfxSliderVolume";
    public static string musicSliderVolumeString = "musicSliderVolume";
    public static string MusicVolume = "MusicVolume";
    public static string SFXVolume = "SFXVolume";
    public static string SFXNotFromMusicVolume = "SFXNotFromMusicVolume";
    public static string Difficulty = "Difficulty";
    public static string GameHistoryAuto = "GameHistoryAuto";
    public static string autoDifficultyOn = "AutoDifficultyOn";
    public static string mmrString = "mmrString";
    public static string lastMMRChange = "LastMMRChange";
    public static string SurvivalModeName = "Survival";
    public static string StoryModeName = "Story";

    private static GameObject player;

    public static string GameMode="Arcade";
    internal static bool connected;

    public static void setPlayer(GameObject player) {
        Constants.player = player;
    }

    public static void SetSFXVolume(double volume)
    {
        SetFloat(sfxVolumeString, volume);
    }

    public static void SetMusicVolume(double volume)
    {
        SetFloat(musicVolumeString, volume);
    }

    public static double GetSFXVolume()
    {
        return Constants.ChangeSliderValueToVolumeValue(Constants.GetSFXSliderVolume());
    }
    public static double GetMusicVolume()
    {
        return Constants.ChangeSliderValueToVolumeValue(Constants.GetMusicSliderVolume());

    }



    public static void SetSFXSliderVolume(double volume)
    {
        SetFloat(sfxSliderVolumeString, volume);
    }

    public static void SetMusicSliderVolume(double volume)
    {
        SetFloat(musicSliderVolumeString, volume);
    }

    public static double GetSFXSliderVolume()
    {
        return GetFloat(sfxSliderVolumeString, StandardForResetSliderSFXStandardValue());
    }
    public static double GetMusicSliderVolume()
    {
        return GetFloat(musicSliderVolumeString, StandardForResetSliderMusic());
    }


    public static GameObject getPlayer(GameObject player)
    {
        return Constants.player;

    }
    public static long GetHighestSurvivalLevel()
    {
        return GetInt(survivalHighLevelString, 0);
    }

    public static long GetHighestSurvivalScore()
    {
        return GetInt(survivalHighScoreString, 0);
    }

    public static void SetHighestSurvivalLevel(long v)
    {
        if (v > GetHighestSurvivalLevel())
        {
            EndGameManager.getInstance().PlayNewHighLevelSound();
            SetInt(survivalHighLevelString, v);
        }
    }

    public static void SetHighestSurvivalScore(long v)
    {
        EndGameManager.getInstance().PlayNewHighScoreSound();
        SetInt(survivalHighScoreString, v);
    }
    public static long getLevel()
    {
        if (isStoryMode())
        {
            return GetInt(levelString + getGameMode(), 1);
        }
        if (isSurvivalMode() && WinCondition.GetInstance() != null) {
            long currentSurvivalLevel = WinCondition.GetInstance().GetSurvivalLevel();
            if (!EndGameManager.getInstance().isGameOver())
            {
                if (currentSurvivalLevel > GetHighestSurvivalLevel()) {

                    SetHighestSurvivalLevel(currentSurvivalLevel);
                }
            } 
            return currentSurvivalLevel;
        }
        if (getDifficultyAutoTrue())
        {
            return GetInt(levelString + "auto", 1);
        }
        return GetInt(levelString+getDifficulty(), 1);
    }


    public static float deathSpeed()
    {
        return 0.02f;
    }
    public static long GetInt(string which, long defaultValue)
    {
        return long.Parse(PlayerPrefs.GetString(which, defaultValue+""));
    }
    public static double GetFloat(string which, double defaultValue)
    {
        return double.Parse(PlayerPrefs.GetString(which, defaultValue+""));
    }
    public static void SetInt(string which, long defaultValue)
    {
        PlayerPrefs.SetString(which, defaultValue+"");
    }
    public static void SetFloat(string which, double defaultValue)
    {
        PlayerPrefs.SetString(which, defaultValue + "");
    }
    public static void SetString(string which, string defaultValue)
    {
        PlayerPrefs.SetString(which, defaultValue);
    }
    public static string GetString(string which, string defaultValue)
    {
        return PlayerPrefs.GetString(which, defaultValue);
    }

    public static void SetBool(string which, bool defaultValue)
    {
        PlayerPrefs.SetString(which, defaultValue.ToString());
    }
    public static bool GetBool(string which, bool defaultValue)
    {
        return bool.Parse(PlayerPrefs.GetString(which, defaultValue.ToString()));
    }
    public static long getShield(string playername)
    {
        return GetInt(playername+shieldString, 0);
    }
    public static void setShield(string playername, long shield)
    {
        SetInt(playername+shieldString, shield);
    }

    public static float getLevelDecay(double startValue, double targetValue, float decayRate)
    {
        return Convert.ToSingle((targetValue + (startValue - targetValue) * Mathf.Exp(-decayRate * Constants.getLevel())));
    }


    public static float getUpgradeDecay(double startValue, double targetValue, float decayRate)
    {
        return Convert.ToSingle((targetValue + (startValue - targetValue) * Mathf.Exp(-decayRate * Constants.getUpgradeLevel())));
    }

    public static float getDifficultyDecay(double startValue, double targetValue, float decayRate)
    {
        return Convert.ToSingle((targetValue + (startValue - targetValue) * Mathf.Exp(-decayRate * (float)Constants.getDifficultyAsLong())));
    }
    public static void setLevel(long level)
    {
        if (getDifficultyAutoTrue())
        {
            SetInt(levelString + "auto", level);
        }
        else
        {
            SetInt(levelString + getDifficulty(), level);
        }
        EndGameManager.getInstance().PlayNewHighLevelSound();
    }


    public static String getGameId() {
        String _gameId;
#if UNITY_IOS
                _gameId = 5752099+"";
#elif UNITY_ANDROID
        _gameId = 5752098+"";
#elif UNITY_EDITOR
            _gameId = 5752098+"";
#endif
        return _gameId;
    }

    public static String getAdIdInterstitial()
    {
        // Get the Ad Unit ID for the current platform:
        return (Application.platform == RuntimePlatform.IPhonePlayer)
            ? "Interstitial_iOS"
            : "Interstitial_Android";
        //return "xd89lq9i2xeplca1v";
    }

    public static String getAdIdBanner()
    {
        // Get the Ad Unit ID for the current platform:
        return (Application.platform == RuntimePlatform.IPhonePlayer)
           ? "Banner_iOS"
         : "Banner_Android";
        //return "cphelr8m2lgam8i9v";
    }
    public static String getAdIdRewarded()
    {
        // Get the Ad Unit ID for the current platform:
        return (Application.platform == RuntimePlatform.IPhonePlayer)
            ? "Rewarded_iOS"
            : "Rewarded_Android";
    }
    public static double getMaxHealth()
    {
        return GetFloat(maxHealthBonusString, 3);
    }

    public static long getUpgradeLevel()
    {
        return GetInt(upgradeLevelString, 0);
    }

    public static void addUpgradeLevel()
    {
        SetInt(upgradeLevelString, getUpgradeLevel()+1);
        player.GetComponent<PlayerStats>().SetScale();

    }

    public static void addMaxHealth(double maxHealthAdd)
    {
        SetFloat(maxHealthBonusString, maxHealthAdd + getMaxHealth());
    }

    public static double getHealingBonus()
    {
        return GetFloat(healingBonusString, 0.5f);
    }

    public static void addHealingBonus(double healingBonusAdd)
    {
        SetFloat(healingBonusString, healingBonusAdd + getHealingBonus());
    }



    public static long getHighScore()
    {
        if (isSurvivalMode()) {
            return GetHighestSurvivalScore();
        } 
        return GetInt(highScoreString + getDifficultyAsLong()+"Level" + getLevel(), 0);
    }

    internal static long getLastLevelHighScore()
    {
        if (isSurvivalMode())
        {
            return GetInt(getDifficultyAsLong() + "Level" + survivalHighScoreString, 0);
        }
        return GetInt(highScoreString + getDifficultyAsLong() + "Level" + (getLevel() - 1), 0);
    }

    public static void setHighScore(long highScore)
    {
        EndGameManager.getInstance().PlayNewHighScoreSound();
        if (isSurvivalMode())
        {
            if (highScore > GetHighestSurvivalScore())
            {
                SetHighestSurvivalScore(highScore);            
            }
        }
        else
        {
            if (highScore > getHighScore())
            {
                SetInt(highScoreString + getDifficultyAsLong() + "Level" + getLevel(), highScore);
            }
        }
    }
    public static long getScore()
    {
        return GetInt(lastScoreString + getDifficultyAsLong() + "Level" + getLevel(), 0);
    }

    public static void setScore(long score)
    {
        SetInt(lastScoreString + getDifficultyAsLong() + "Level" + getLevel(), score);
    }

    internal static long getLastLevelScore()
    {
        return GetInt(lastScoreString + getDifficultyAsLong() + "Level" + (getLevel()-1), 0);
    }


    internal static double getDifficulty()
    {
        if (getDifficultyAutoTrue()) {
            return MmrToDifficulty(GetMMR());
                }
        return GetFloat(Difficulty, StandardForResetSliderDifficulty());
    }

    public static double MmrToDifficulty(double mmr, double k = 0.002)
    {
        return 1.0 / (1.0 + Math.Exp(-k * (mmr - 1000)));
    }



    public static void addWinOnAuto()
    {
        double difficulty=getDifficulty();
        AddMMR(true,difficulty);
        SetString(GameHistoryAuto, GetString(GameHistoryAuto, "") + "W"+difficulty);
        
    }
    public static void addLoseOnAuto()
    {
        double difficulty = getDifficulty();
        AddMMR(false, difficulty);
        SetString(GameHistoryAuto, GetString(GameHistoryAuto, "") + "L" + difficulty);
        
    }

    private static void AddMMR(bool won, double difficulty)
    {
        if (isSurvivalMode())
        {
            return;
        }
        double mmr= GetMMR();
        double mmrBefore = mmr;

        difficulty = Math.Clamp(difficulty+ Constants.getLevel()/1000, 0.0000001, 0.999999); // Ensure difficulty stays between 0 and 1
        
        double scalingFactor = 1 + Math.Pow(Math.Abs(difficulty - 0.5), 2) * 2;
        double change;
        double mmrdifficulty=MmrToDifficulty(mmr);
        if (won)
        {
            change = BASE_GAIN * difficulty*scalingFactor* (Math.Min(difficulty - mmrdifficulty, 0.1f) /10 + 0.1f);
        }
        else
        {
            change = -BASE_LOSS * (1 - difficulty) * scalingFactor * (Math.Min(mmrdifficulty-difficulty,0.1f)/ 10 + 0.1f);
        }
        SetFloat(lastMMRChange, change);

        mmr += change;
        Debug.Log("MMR " + mmrBefore + " CHANGE " + change + " TO " +mmr+" from win:"+won+" on difficulty "+difficulty);
        mmr = Math.Max(mmr, 0); // Prevent MMR from going below 0

        SetMMR(mmr);
    }
    public static double getLastMMRChange() {
        return GetFloat(lastMMRChange, 0);
    }

    public static void SetMMR(double mmr)
    {
        SetFloat(mmrString, mmr);
    }


    public static double GetMMR()
    {
        return GetFloat(mmrString, 1000.0);
    }

    public static string GetGameHistoryAuto() {
        return GetString(GameHistoryAuto, GetString(GameHistoryAuto, ""));
    }
    
    public static float StandardForResetSliderSFXStandardValue()
    {
        return 0.8f;
    }

    public static float StandardForResetSliderMusic()
    {
        return 0.8f;
    }

    public static float StandardForResetSliderDifficulty()
    {
        return 0.5f;
    }
    internal static void setDifficulty(double i)
    {
        SetFloat(Difficulty, i);
    }
    internal static Color GetBulletColor()
    {
        return GetThisColor(playerShootColorString);
    }

    internal static void SetBulletColor()
    {
        SetFloat(playerShootColorString + "1", UnityEngine.Random.value);
        SetFloat(playerShootColorString + "2", UnityEngine.Random.value);
        SetFloat(playerShootColorString + "3", UnityEngine.Random.value);
        SetFloat(playerShootColorString + "xSize", UnityEngine.Random.Range(0.12f, 0.7f));
        SetFloat(playerShootColorString + "ySize", UnityEngine.Random.Range(0.12f, 0.7f));
        SetFloat(playerShootColorString + "zSize", UnityEngine.Random.Range(0.12f, 0.7f));
    }

    internal static void MakeRandomNewColorsForThisNewLevel(long level)
    {

        SetFloat(greenEnemyColorString + "1"+level, UnityEngine.Random.value);
        SetFloat(greenEnemyColorString + "2" + level, UnityEngine.Random.value);
        SetFloat(greenEnemyColorString + "3" + level, UnityEngine.Random.value);
        SetFloat(purpleEnemyColorString + "1" + level, UnityEngine.Random.value);
        SetFloat(purpleEnemyColorString + "2" + level, UnityEngine.Random.value);
        SetFloat(purpleEnemyColorString + "3" + level, UnityEngine.Random.value);
        SetFloat(meteorColorString + "1" + level, UnityEngine.Random.value);
        SetFloat(meteorColorString + "2" + level, UnityEngine.Random.value);
        SetFloat(meteorColorString + "3" + level, UnityEngine.Random.value);
        SetFloat(backgroundColorString + "1" + level, UnityEngine.Random.value);
        SetFloat(backgroundColorString + "2" + level, UnityEngine.Random.value);
        SetFloat(backgroundColorString + "3" + level, UnityEngine.Random.value);
    }

    public static Color GetThisLevelsBackgroundColor()
    {
        return GetThisColor(backgroundColorString+getLevel());
    }
    public static Color GetThisLevelPurpleEnemyColor()
    {
        return GetThisColor(purpleEnemyColorString + getLevel());
    }
    public static Color GetThisLevelGreenEnemyColor()
    {
        return GetThisColor(greenEnemyColorString + getLevel());
    }
    public static Color GetThisLevelMeteorColor()
    {
        return GetThisColor(meteorColorString + getLevel());
    }
    public static Color GetThisColor(String string1)
    {
        double f1 = GetFloat(string1 + "1", UnityEngine.Random.Range(0f,1f));
        SetFloat(string1 + "1", f1);
        double f2 = GetFloat(string1 + "2", UnityEngine.Random.Range(0f, 1f));
        SetFloat(string1 + "2", f2);
        double f3 = GetFloat(string1 + "3", UnityEngine.Random.Range(0f, 1f));
        SetFloat(string1 + "3", f3);
        return new Color(Convert.ToSingle(f1), Convert.ToSingle(f2), Convert.ToSingle(f3));
    }

    public static Vector3 GetBulletScale()
    {
        double f1=GetFloat(playerShootColorString + "xSize", UnityEngine.Random.Range(0.12f, 0.7f));
        double f2=GetFloat(playerShootColorString + "ySize", UnityEngine.Random.Range(0.12f, 0.7f));
        double f3=GetFloat(playerShootColorString + "zSize", UnityEngine.Random.Range(0.12f, 0.7f));
        return new Vector3(Convert.ToSingle(f1), Convert.ToSingle(f2), Convert.ToSingle(f3));
    }

    public static void MoveShadowBoy(Transform shadowboy, Vector2 targetPolong, double speed) {
       // if (Vector2.Distance(shadowboy.position, targetPolong) > 0.01f)
        {
            shadowboy.transform.position = Vector2.MoveTowards(shadowboy.transform.position, targetPolong, Convert.ToSingle(speed * Time.deltaTime));
        }
        //else {
         //   shadowboy.transform.position = targetPolong;
       // }
    }

    internal static float GetGameSpeed()
    {
       return Math.Max(1,Math.Min(0.1f,1 - (1 / (Constants.getLevel() + 1)) + getLevelDecay(-0.2f,+0.2f,0.01f)));
    }

    internal static bool IsLoggedIn()
    {
        return false;
        //TODO check if logged in
    }

    internal static string getGameMode() {
        return GameMode;
    }
    internal static bool isSurvivalMode()
    {
        return SurvivalModeName.Equals(GameMode);
    }
    internal static bool isStoryMode()
    {
        return StoryModeName.Equals(GameMode);
    }
    internal static void SetGameMode(string gameMode)
    {
        GameMode = gameMode;
    }
    internal static float ChangeSliderValueToVolumeValue(float f)
    {
        float returnvalue = (float)Double.Parse(Mathf.Lerp(-80f, 0f, f).ToString("F1"));
        return returnvalue;
    }
    internal static double ChangeSliderValueToVolumeValue(double f)
    {
        double returnvalue = Double.Parse(Mathf.Lerp(-80f, 0f, (float)f).ToString("F1"));
        return returnvalue;
    }

    internal static long getDifficultyAsLong()
    {
        long l = (long)(getDifficulty() * GetMaximumDifficulty());
        return l;
    }

    internal static long getMMRDifficultyAsLong()
    {
        long l = (long)(MmrToDifficulty(GetMMR()) * GetMaximumDifficulty());
        return l;
    }

    private static double GetMaximumDifficulty()
    {
        return 1000;
    }

    public static List<GameObject> getChildrenOf(GameObject obj) {

        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            list.Add(obj.transform.GetChild(i).gameObject);
        }
        return list;
    }

    public static List<GameObject> getChildrenOf(String nameOfParent) {
        GameObject originalGameObject = GameObject.Find(nameOfParent);
        return getChildrenOf(originalGameObject);
    }
    public static void ReloadMainMenuValues()
    {
        ScoreDisplay.GetInstance().LoadScoreDisplay();
    }

 

    internal static bool getDifficultyAutoTrue()
    {
        return GetBool(autoDifficultyOn, true);
    }

    internal static void setDifficultyAuto(bool isOn)
    {
        SetBool(autoDifficultyOn, isOn);
    }
}
