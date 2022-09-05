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

    // iOS 알림 허용 권한 체크 코루틴
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

        // 모든 작업이 끝난후 푸쉬 전송
        SendNotification();
    }

    // 푸쉬 알림을 전송하는 코드

    private void SendNotification()
    {
        // 딜레이를 위한 트리거 생성
        var timeTrigger = new iOSNotificationTimeIntervalTrigger()
        {
            // 30초뒤
            TimeInterval = new TimeSpan(0, 0, 30),

            // 반복하지 않음
            Repeats = false
        };

        // 전송할 푸쉬 알림을 생성 및 데이터 입력
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

        // 알림 전송 (실제 알림은 트리거의 딜레이 만큼 지난후 전송)
        iOSNotificationCenter.ScheduleNotification(notification);
    }
}
