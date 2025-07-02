using UnityEngine;
using System;
# if UNITY_ANDROID
using Unity.Notifications.Android;
#endif


public class AndroidNotifications : MonoBehaviour
{
    [SerializeField] private int notificationId;
    [SerializeField] private string channelIdExample;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }
#if UNITY_ANDROID
    public void NotificationExample(DateTime timeToFire)
    {
        AndroidNotificationChannel notificationChannel = new AndroidNotificationChannel()
        {
            Id = channelIdExample,
            Name = "Leaving Earth For The Better",
            Description = "Leaving Earth For The Better",
            Importance = Importance.Default
        };
        AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);
        AndroidNotification notification = new AndroidNotification()
        {
            Title = "I thought you wanted to leave earth?",
            Text = "You can do it!",
            SmallIcon = "defaultsmall",
            LargeIcon = "defaultlarge",
            FireTime = timeToFire
        };
        AndroidNotificationCenter.SendNotificationWithExplicitID(notification, channelIdExample, notificationId);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus == false)
        {
            DateTime whenToFire = DateTime.Now.AddDays(1);
            NotificationExample(whenToFire);
        }
        else {
            AndroidNotificationCenter.CancelScheduledNotification(notificationId);
        }
    }
#endif

}
