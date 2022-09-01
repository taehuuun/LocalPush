using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulator : MonoBehaviour
{
    [SerializeField] private float maxFeedHour = 1;
    [SerializeField] private float maxCleanHour = 12f;
    [SerializeField] private float maxSizeHour = 360f;

    [SerializeField] private float feed = 100f;
    [SerializeField] private float decFeed;
    [SerializeField] private float clean = 100f;
    [SerializeField] private float decClean;
    [SerializeField] private float size = 20f;    
    [SerializeField] private float incSize;

    private const int HOUR2SEC = 3600;

    private void Start()
    {
        SettingValue();
        StartSimulator();
    }

    private void OnApplicationQuit()
    {

    }

    // max일에 따른 분당 감소값 계산
    private void SettingValue()
    {
        // dec = (퍼센트) / (맥스 데이 * 1440 (24 * 60 * 60))
        decFeed = 100 / (maxFeedHour * HOUR2SEC);
        decClean = 100 / (maxCleanHour * HOUR2SEC);
        incSize = 80 / (maxSizeHour * HOUR2SEC);
    }

    private void StartSimulator()
    {
        StartCoroutine(DecFood());
        StartCoroutine(DecClean());
        StartCoroutine(IncSize());
    }

    private IEnumerator DecFood()
    {
        while(feed > 0)
        {
            Debug.Log($"현재 FEED : {feed}");
            yield return new WaitForSecondsRealtime(1f);
            feed -= decFeed;
        }
    }

    private IEnumerator DecClean()
    {
        while(clean > 0)
        {
            Debug.Log($"현재 CLEAN : {clean}");
            yield return new WaitForSecondsRealtime(1f);
            clean -= decClean;
        }
    }

    private IEnumerator IncSize()
    {
        while(size < 100)
        {
            Debug.Log($"현재 SIZE : {size}");
            yield return new WaitForSecondsRealtime(1f);
            size += incSize;
        }
    }
}
