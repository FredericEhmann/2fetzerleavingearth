using System;
using GoogleMobileAds.Api;
using GoogleMobileAds;
using UnityEngine;

public class LoseAd : MonoBehaviour
{
    CanvasGroup cGroup;
    GameObject loseScreen;
    AudioSource audioSourceLose;
        // These ad units are configured to always serve test ads.
#if UNITY_ANDROID
        private string _adUnitId = "ca-app-pub-4137999179372784/8720384730";
#elif UNITY_IPHONE
        private string _adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        private string _adUnitId = "unused";
#endif
    private static DateTime lastAdTime = DateTime.MinValue;

    private void ResetCStuff() {
        if (cGroup != null)
        {
            cGroup.alpha = 1;
        }
        if (loseScreen != null)
        {
            loseScreen.SetActive(true);
        }
        if (audioSourceLose != null)
        {
            audioSourceLose.Play();
        }
        Time.timeScale = 1f;
    }
    internal void ShowAdWith(CanvasGroup cGroup, GameObject loseScreen, AudioSource audioSourceLose)
    {
        try
        {
            this.cGroup = cGroup;
            this.loseScreen = loseScreen;
            this.audioSourceLose = audioSourceLose;
            // Check if 5 minutes have passed since the last ad was shown
            if ((DateTime.Now - lastAdTime).TotalSeconds >= 140)
            {
                MobileAds.RaiseAdEventsOnUnityMainThread = true;
                if (PreloadInterstitialAd._interstitialAd != null && PreloadInterstitialAd._interstitialAd.CanShowAd())
                {
                    RegisterEventHandlers(PreloadInterstitialAd._interstitialAd);
                    Debug.Log("Showing interstitial ad.");
                    PreloadInterstitialAd._interstitialAd.Show();
                }
                else
                {
                    Debug.LogError("Interstitial ad is not ready yet.");
                    ResetCStuff();
                }
            }
            else {
                ResetCStuff();
            }
        }
        catch (Exception e) {
            Debug.LogError(e);
            ResetCStuff();
        }
    }



    private void RegisterEventHandlers(InterstitialAd interstitialAd)
    {
        // Raised when the ad is estimated to have earned money.
        interstitialAd.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        interstitialAd.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        interstitialAd.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        interstitialAd.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Interstitial ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial ad full screen content closed.");
            ResetCStuff();
            lastAdTime = DateTime.Now;
        };
        // Raised when the ad failed to open full screen content.
        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);
            ResetCStuff();

        };
    }
}
