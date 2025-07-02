using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class FadeCanvas : MonoBehaviour
{
    public static FadeCanvas instance;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float changeValue;
    [SerializeField] private float waitTime;
    [SerializeField] private bool fadeStarted = false;
    [SerializeField] private GameObject loadinScreen;
    [SerializeField] private Image loadingBar;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else {
            Destroy(gameObject);
        }
    }
    public static FadeCanvas getInstance() {
        return instance;
    }

    public void FaderLoadInt(int levelNumber)
    {
        StartCoroutine(FadeOutString(levelNumber));
    }

    public void FaderLoadString(string levelName) {
        StartCoroutine(FadeOutString(levelName));
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn() {
        loadinScreen.SetActive(false);
        fadeStarted = false;
        while (canvasGroup.alpha > 0) {
            if (fadeStarted) {
                yield break;
            }
            canvasGroup.alpha -= changeValue;
            yield return new WaitForSeconds(waitTime);
        }
    }

    IEnumerator FadeOutString(string levelName) {
        if (fadeStarted) {
            yield break;
        }
        fadeStarted = true;
        while (canvasGroup.alpha < 1) {
            canvasGroup.alpha += changeValue;
            yield return new WaitForSeconds(waitTime);
        }
        //SceneManager.LoadScene(levelName);
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName);
        ao.allowSceneActivation = false;
        loadinScreen.SetActive(true);
        loadingBar.fillAmount = 0;
        while (ao.isDone == false) {
            loadingBar.fillAmount = ao.progress / 0.9f;
            if (ao.progress >= 0.9f)
            {
                ao.allowSceneActivation=true;
            }
            yield return null;
        }
        StartCoroutine(FadeIn());
    }


    IEnumerator FadeOutString(int levelNumber)
    {
        if (fadeStarted)
        {
            yield break;
        }
        fadeStarted = true;
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += changeValue;
            yield return new WaitForSeconds(waitTime);
        }
        //SceneManager.LoadScene(levelName);
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelNumber);
        ao.allowSceneActivation = false;
        loadinScreen.SetActive(true);
        loadingBar.fillAmount = 0;
        while (ao.isDone == false)
        {
            loadingBar.fillAmount = ao.progress / 0.9f;
            if (ao.progress >= 0.9f)
            {
                ao.allowSceneActivation = true;
            }
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(FadeIn());
    }

    internal bool isMainMenu()
    {
        return "StartScene".Equals(SceneManager.GetActiveScene().name);
    }
}
