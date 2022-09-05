using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.iOS;

public class LocalPush_IOS : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(CheckRequestAuth());
    }

    // iOS �˸� ��� ���� üũ �ڷ�ƾ
    private IEnumerator CheckRequestAuth()
    {
        var authorizationOption = AuthorizationOption.Alert | AuthorizationOption.Badge;

        using (var req = new AuthorizationRequest(authorizationOption, true))
        {
            while (!req.IsFinished)
            {
                yield return null;
            };

            string res = "\n RequestAuthorization:";
            res += "\n finished: " + req.IsFinished;
            res += "\n granted :  " + req.Granted;
            res += "\n error:  " + req.Error;
            res += "\n deviceToken:  " + req.DeviceToken;
            Debug.Log(res);
        }

        // ��� �۾��� ������ Ǫ�� ����
        SendNotification();
    }

    // Ǫ�� �˸��� �����ϴ� �ڵ�

    private void SendNotification()
    {
        // �����̸� ���� Ʈ���� ����
        var timeTrigger = new iOSNotificationTimeIntervalTrigger()
        {
            // 30�ʵ�
            TimeInterval = new TimeSpan(0, 0, 30),

            // �ݺ����� ����
            Repeats = false
        };

        // ������ Ǫ�� �˸��� ���� �� ������ �Է�
        var notification = new iOSNotification()
        {
            // You can specify a custom identifier which can be used to manage the notification later.
            // If you don't provide one, a unique string will be generated automatically.
            Identifier = "_notification_01",
            Title = "Title",
            Body = "Scheduled at: " + DateTime.Now.ToShortDateString() + " triggered in 5 seconds",
            Subtitle = "This is a subtitle, something, something important...",
            ShowInForeground = true,
            ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
            CategoryIdentifier = "category_a",
            ThreadIdentifier = "thread1",
            Trigger = timeTrigger,
        };

        // �˸� ���� (���� �˸��� Ʈ������ ������ ��ŭ ������ ����)
        iOSNotificationCenter.ScheduleNotification(notification);
    }
}
