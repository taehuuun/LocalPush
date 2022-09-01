using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;

public class LocalPush : MonoBehaviour
{
    [SerializeField] private Simulator simulator;
    [SerializeField] private string channderID = "TEST_CHANNEL";

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
            Id = "Test",
            Name = "Test Channel",
            Importance = Importance.Default,
            Description = "Generic notifications",
        };

        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    private void SendNotification()
    {
        var notification = new AndroidNotification();
        notification.Title = "TEST TITLE";
        notification.Text = "TEST TEXT";
        notification.FireTime = System.DateTime.Now.AddMinutes(1);

        notification.SmallIcon = "app_icon_id";
        notification.LargeIcon = "app_large_icon_id";

        notification.IntentData = "{\"title\":\"Notification:1\",\"data\":\"200\"}";

        AndroidNotificationCenter.SendNotification(notification,channderID);
    }
}
