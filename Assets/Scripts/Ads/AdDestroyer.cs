using System;
using System.Collections;
using GoogleMobileAds.Api;
using UnityEngine;

public class AdDestroyer : MonoBehaviour
{
#if UNITY_ANDROID
    private string _adUnitId = "ca-app-pub-4137999179372784/8250477418";
#elif UNITY_IPHONE
    private string _adUnitId = "ca-app-pub-9613119499106932/2225190777";
#else
    private string _adUnitId = "unused";
#endif

    private static bool isAdLoading = false;
    private static AdDestroyer _instance;
    public static bool failedLoad = false;

    public static BannerView _bannerView = null;
    public void CreateBannerView()
    {
        Debug.Log("Creating banner view");

        // If we already have a banner, destroy the old one.
        if (_bannerView != null)
        {
            DestroyBannerAd();
        }

        _bannerView = new BannerView(_adUnitId, AdSize.Banner, AdPosition.TopLeft);
    }

    private void Awake()
    {
        _bannerView?.Hide();
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        _instance.DoLoadBannerStuff();
    }

    public void DoLoadBannerStuff() {
        if (isAdLoading)
        {
            return;
        }
        isAdLoading = true;
        MobileAds.RaiseAdEventsOnUnityMainThread = true;
        DestroyBannerAd();
        StartCoroutine(LoadBannerAdCoroutine());

    }

    private IEnumerator LoadBannerAdCoroutine()
    {
        yield return new WaitForEndOfFrame(); // Let Unity process other events first.

        if (_bannerView == null)
        {
            CreateBannerView();
        }

        ListenToBannerAdEvents();

        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        Debug.Log("Loading banner ad.");
        _bannerView.LoadAd(adRequest);
    }

    private void ListenToBannerAdEvents()
    {
        if (_bannerView == null) return;

        _bannerView.OnBannerAdLoaded += () =>
        {
            isAdLoading = false;
            failedLoad = false;
            Debug.Log("Banner ad loaded.");
            _bannerView.Hide();
        };

        _bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            isAdLoading = false;
            failedLoad = true;
            Debug.LogError($"Banner ad failed to load: {error}");
            DestroyBannerAd();
        };

        // Other event handlers...
    }

    public void DestroyBannerAd()
    {
        if (_bannerView != null)
        {
            Debug.Log("Destroying banner view.");
            _bannerView.Destroy();
            _bannerView = null;
        }
    }
}
