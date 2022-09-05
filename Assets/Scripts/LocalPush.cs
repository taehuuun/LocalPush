using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;

using NotificationServices = UnityEngine.iOS.NotificationServices;
using NOtificationType = UnityEngine.iOS.NotificationType;
using LocalNotification = UnityEngine.iOS.LocalNotification;

public class LocalPush : MonoBehaviour
{
    [SerializeField] private Simulator simulator;
    [SerializeField] private string channderID = "LIFE";

    private void Start()
    {
        CheckNotirficationIntentData();
        RegisterNotificationChannel();
        AndroidNotificationCenter.OnNotificationReceived += OnNotificationReceived;
        SendNotification();
    }

    private void CheckNotirficationIntentData()
    {
        var notificationsIntentData = AndroidNotificationCenter.GetLastNotificationIntent();

        if(notificationsIntentData != null)
        {
            var id = notificationsIntentData.Id;
            var channel = notificationsIntentData.Channel;
            var notifications = notificationsIntentData.Notification;
            Debug.Log($"Notification IntentData : \n ID : {id}\n Channel : {channel}\n Notifications : {notifications}");
        }
    }

    private void OnNotificationReceived(AndroidNotificationIntentData data)
    {
        Debug.Log($"Notification Received : \n ID : {data.Id}\n Channel : {data.Channel}\n Title : {data.Notification.Title}\n Text : {data.Notification.Text}");
    }

    private void RegisterNotificationChannel()
    {
        var channel = new AndroidNotificationChannel()
        {
            Id = "LIFE",
            Name = "Test Channel",
            Importance = Importance.Default,
            Description = "Generic notifications",
        };

        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    private void SendNotification()
    {
        var notification = new AndroidNotification();
        notification.Title = "생명력 10% 미만!";
        notification.Text = "생명력이 10% 미만입니다";
        notification.FireTime = System.DateTime.Now.AddSeconds(20);

        // notification.SmallIcon = "app_icon_id";
        // notification.LargeIcon = "app_large_icon_id";

        notification.IntentData = "{\"title\":\"Notification:1\",\"data\":\"200\"}";

        AndroidNotificationCenter.SendNotification(notification,channderID);
    }
}
