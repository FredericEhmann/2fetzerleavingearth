using GoogleMobileAds.Api;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{

    [SerializeField] GameObject loginScreen=null;
    public static bool showLoginScreen = false;
    [SerializeField] Slider musicSlider = null;
    [SerializeField] Slider sfxSlider = null;
    [SerializeField] Slider difficultySlider = null;
    [SerializeField] AudioMixer mixer = null;
    [SerializeField] AudioSource change = null;
    [SerializeField] Toggle difficultyAutoCheckBox = null;
    [SerializeField] Button shopButton = null;

    private void Awake()
    {
        if (shopButton != null) {
            if (Constants.connected)
            {
                shopButton.gameObject.SetActive(true);
            }
            else {
                shopButton.gameObject.SetActive(false);
            }
        }
        if (!Constants.IsLoggedIn() && showLoginScreen)
        {
            if (loginScreen != null)
            {
                loginScreen?.SetActive(true);
            }
        }
        else
        {
            if (loginScreen != null)
            {
                loginScreen?.SetActive(false);
            }
        }
        if (mixer != null)
        {
            mixer.SetFloat(Constants.SFXVolume, (float)Constants.GetSFXVolume());
            mixer.SetFloat(Constants.SFXNotFromMusicVolume, (float)Constants.GetSFXVolume());
            mixer.SetFloat(Constants.MusicVolume, (float)Constants.GetMusicVolume());
        }
    }
    public void LoadLevelString(string levelName) {
        // Initialize the Google Mobile Ads SDK.
        EndGameManager.getInstance().resetStartedResolve();
        Time.timeScale = 1;
        FadeCanvas.getInstance()?.FaderLoadString(levelName);
    }

    public void LoadLevelInt(int levelIndex)
    {
        EndGameManager.getInstance().resetStartedResolve();
        Time.timeScale = 1;
        FadeCanvas.getInstance()?.FaderLoadInt(levelIndex);
    }

    public void RestartLevel()
    {
        EndGameManager.getInstance().resetStartedResolve();
        Time.timeScale = 1;
        FadeCanvas.getInstance()?.FaderLoadString("Level");
//        FadeCanvas.getInstance().FaderLoadString(SceneManager.GetActiveScene().name);
    }
    public void NextLevel()
    {
        EndGameManager.getInstance().resetStartedResolve();
        Time.timeScale = 1;
        //next level
        FadeCanvas.getInstance()?.FaderLoadString("Level");
        //        FadeCanvas.getInstance().FaderLoadString(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        FadeCanvas.getInstance()?.FaderLoadString("StartScene");
    }
    public void Shop()
    {
        Time.timeScale = 1;
        if (Constants.IsLoggedIn())
        {
            FadeCanvas.getInstance()?.FaderLoadString("Shop");
        }
        else
        {
            showLoginScreen = true;
            FadeCanvas.getInstance()?.FaderLoadString("StartScene");
        }
    }

    public void Survival()
    {
        Time.timeScale = 1;
        Constants.SetGameMode("Survival");
        FadeCanvas.getInstance()?.FaderLoadString("Level");
    }

    public void Story()
    {
        Time.timeScale = 1;
        Constants.SetGameMode("Story");
        FadeCanvas.getInstance()?.FaderLoadString("Level");
    }

    public void Arcade()
    {
        Time.timeScale = 1;
        Constants.SetGameMode("Arcade");
        FadeCanvas.getInstance()?.FaderLoadString("Level");
    }

    public void Options(GameObject ob) {
        ob.SetActive(true);
        if (sfxSlider != null)
        {
            sfxSlider.value = (float)Constants.GetSFXSliderVolume();
            SetSFXVolume(sfxSlider);
        }
        if (musicSlider != null)
        {
            musicSlider.value = (float)Constants.GetMusicSliderVolume();
            SetMusicVolume(musicSlider);
        }
        if (difficultyAutoCheckBox != null)
        {
            difficultyAutoCheckBox.isOn = Constants.getDifficultyAutoTrue();
            SetDifficultyAuto(difficultyAutoCheckBox);
        }
        if (difficultySlider != null)
        {
            difficultySlider.value = (float)Constants.getDifficulty();
            SetDifficulty(difficultySlider);
        }
    }


    public void MainMenuPlusLevelUp() {
        Time.timeScale = 1;
        MainMenu();
    }

    public void SetMusicVolume(Slider slider)
    {
        float f = slider.value;
        Constants.SetMusicSliderVolume(f);
        f = Constants.ChangeSliderValueToVolumeValue(f);
        Constants.SetMusicVolume(f);
        List<GameObject> objects = Constants.getChildrenOf(slider.gameObject);
        for (int i = 0; i < objects.Count; i++)
        {
            GameObject o = objects[i];
            if (o.name.ToLower().EndsWith("asvalue"))
            {
                o.GetComponent<TextMeshProUGUI>().text = Constants.GetMusicVolume() + "db";
            }
        }
        if (mixer != null)
        {
            mixer.SetFloat(Constants.MusicVolume, f);
        }
    }

    public void SetSFXVolume(Slider slider)
    {
        float f = slider.value;
        Constants.SetSFXSliderVolume(f);
        f = Constants.ChangeSliderValueToVolumeValue(f);
        Constants.SetSFXVolume(f);
        List<GameObject> objects = Constants.getChildrenOf(slider.gameObject);
        for (int i = 0; i < objects.Count; i++)
        {
            GameObject o = objects[i];
            if (o.name.ToLower().EndsWith("asvalue"))
            {
                o.GetComponent<TextMeshProUGUI>().text = Constants.GetSFXVolume()+"db";
            }
        }
        if (mixer != null)
        {
            mixer.SetFloat(Constants.SFXVolume, f);
            mixer.SetFloat(Constants.SFXNotFromMusicVolume, f);

        }
        if (change != null)
        {
            change.Play();
        }

    }


    public void SetDifficultyAuto(Toggle difficultyAutoCheckBox)
    {
        Constants.setDifficultyAuto(difficultyAutoCheckBox.isOn);
        difficultyAutoCheckBox.isOn = Constants.getDifficultyAutoTrue();
        difficultySlider.value = (float)Constants.getDifficulty();
        SetDifficulty(difficultySlider);
        Constants.ReloadMainMenuValues();
    }

    public void SetDifficultyFromSlider(Slider slider)
    {
        if (slider.value != Constants.getDifficulty())
        {
            difficultyAutoCheckBox.isOn = false;
            Constants.setDifficultyAuto(false);
        }
        SetDifficulty(slider);
    }
    public void SetDifficulty(Slider slider)
    {
        if (Constants.getDifficultyAutoTrue()) {
            slider.value = (float)Constants.getDifficulty();
        }
        float f = slider.value;
        Constants.setDifficulty(f);
        List<GameObject> objects=Constants.getChildrenOf(slider.gameObject);
        for (int i = 0; i < objects.Count; i++)
        {
            GameObject o=objects[i];
            if (o.name.ToLower().EndsWith("asvalue"))
            {
                o.GetComponent<TextMeshProUGUI>().text = Constants.getDifficultyAutoTrue()?"auto":""+Constants.getDifficultyAsLong();
            }
        }
        Constants.ReloadMainMenuValues();
    }


    public void ResetSliders()
    {
        Constants.setDifficultyAuto(true);
        difficultyAutoCheckBox.isOn = Constants.getDifficultyAutoTrue();
        SetDifficultyAuto(difficultyAutoCheckBox);
        Constants.SetSFXSliderVolume(Constants.StandardForResetSliderSFXStandardValue());
        sfxSlider.value = (float)Constants.GetSFXSliderVolume();
        SetSFXVolume(sfxSlider);
        Constants.SetMusicSliderVolume(Constants.StandardForResetSliderMusic());
        musicSlider.value = (float)Constants.GetMusicSliderVolume();
        SetMusicVolume(musicSlider);
        Constants.setDifficulty(Constants.StandardForResetSliderDifficulty());
        difficultySlider.value = (float)Constants.getDifficulty();
        SetDifficulty(difficultySlider);
        Constants.MakeRandomNewColorsForThisNewLevel(Constants.getLevel());
        Constants.MakeRandomNewColorsForThisNewLevel(Constants.GetHighestSurvivalLevel());
        for (long i = Math.Min(Constants.getLevel(),Constants.GetHighestSurvivalLevel()); i < Math.Max(Constants.getLevel(), Constants.GetHighestSurvivalLevel()); i++) {
            Constants.MakeRandomNewColorsForThisNewLevel(i);
        }
        Constants.SetBulletColor();
        Constants.ReloadMainMenuValues();


    }


}
