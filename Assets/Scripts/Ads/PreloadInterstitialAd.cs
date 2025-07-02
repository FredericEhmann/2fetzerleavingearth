using System;
using System.Collections;
using GoogleMobileAds.Api;
using GoogleMobileAds;
using UnityEngine;

public class PreloadInterstitialAd : MonoBehaviour
{
    // These ad units are configured to always serve test ads.
#if UNITY_ANDROID
    private string _adUnitId = "ca-app-pub-4137999179372784/8720384730";
#elif UNITY_IPHONE
    private string _adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
    private string _adUnitId = "unused";
#endif

    private static bool isAdLoading = false;
    private static bool retryScheduled = false;
    private PreloadInterstitialAd _instance;
    public static InterstitialAd _interstitialAd;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
        _instance.doLoad();
    }

    public void doLoad()
    {
        if (isAdLoading || (PreloadInterstitialAd._interstitialAd != null && PreloadInterstitialAd._interstitialAd.CanShowAd()))
        {
            return;
        }

        MobileAds.RaiseAdEventsOnUnityMainThread = true;
        isAdLoading = true;

        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            try
            {
                LoadInterstitialAd();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                isAdLoading = false;
            }
        });
    }

    /// <summary>
    /// Loads the interstitial ad.
    /// </summary>
    public void LoadInterstitialAd()
    {
        try
        {
            // Clean up the old ad before loading a new one.
            if (PreloadInterstitialAd._interstitialAd != null)
            {
                PreloadInterstitialAd._interstitialAd.Destroy();
                PreloadInterstitialAd._interstitialAd = null;
            }

            Debug.Log("Loading the interstitial ad.");

            // Create the request used to load the ad.
            var adRequest = new AdRequest();

            // Send the request to load the ad.
            InterstitialAd.Load(_adUnitId, adRequest, (InterstitialAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError("Interstitial ad failed to load: " + error);
                    isAdLoading = false;

                    // Schedule a retry if not already scheduled.
                    if (!retryScheduled)
                    {
                        retryScheduled = true;
                        StartCoroutine(RetryLoadAfterDelay(30)); // Retry after 5 minutes (300 seconds).
                    }

                    return;
                }

                Debug.Log("Interstitial ad loaded successfully: " + ad.GetResponseInfo());
                PreloadInterstitialAd._interstitialAd = ad;
                isAdLoading = false;
                retryScheduled = false; // Reset retry flag.
            });
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            isAdLoading = false;
        }
    }

    /// <summary>
    /// Coroutine to retry loading the ad after a delay.
    /// </summary>
    private IEnumerator RetryLoadAfterDelay(float delay)
    {
        Debug.Log($"Retrying to load interstitial ad in {delay} seconds.");
        yield return new WaitForSeconds(delay);
        retryScheduled = false;
        doLoad();
    }
}
