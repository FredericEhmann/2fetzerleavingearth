using GoogleMobileAds.Api;
using GoogleMobileAds;
using UnityEngine;
using System;

public class GoogleAdsManager : MonoBehaviour
{
    private static DateTime lastAdTime = DateTime.MinValue;



    private void Awake()
    {
        if (AdDestroyer._bannerView != null && !AdDestroyer._bannerView.IsDestroyed)
        {
        if ((DateTime.Now - lastAdTime).TotalSeconds >= 50)
        {
            MobileAds.RaiseAdEventsOnUnityMainThread = true;
            AdDestroyer._bannerView.Show();
            lastAdTime = DateTime.Now;

            }
        }
    }

    


}
