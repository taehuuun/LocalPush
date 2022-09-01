using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulator : MonoBehaviour
{
    [SerializeField] private float maxFeedDay = 1;
    [SerializeField] private float maxCleanDay = 0.5f;
    [SerializeField] private float maxSizeDay = 15f;

    [SerializeField] private float feed = 100f;
    [SerializeField] private float decFeed;
    [SerializeField] private float clean = 100f;
    [SerializeField] private float decClean;
    [SerializeField] private float size = 20f;    
    [SerializeField] private float incSize;

    private const int DAY2MIN = 1440;
    private WaitForSecondsRealtime delay = new WaitForSecondsRealtime(60f);

    private void Start()
    {
        SettingValue();
        StartSimulator();
    }

    // max�Ͽ� ���� �д� ���Ұ� ���
    private void SettingValue()
    {
        // dec = (�ۼ�Ʈ) / (�ƽ� ���� * 1440 (24 * 60))
        decFeed = 100 / (maxFeedDay * DAY2MIN);
        decClean = 100 / (maxCleanDay * DAY2MIN);
        incSize = 80 / (maxSizeDay * DAY2MIN);
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
            Debug.Log($"���� FEED : {feed}");
            yield return delay;
            feed -= decFeed;
        }
    }

    private IEnumerator DecClean()
    {
        while(clean > 0)
        {
            Debug.Log($"���� CLEAN : {clean}");
            yield return delay;
            clean -= decClean;
        }
    }

    private IEnumerator IncSize()
    {
        while(size < 100)
        {
            Debug.Log($"���� SIZE : {size}");
            yield return delay;
            size += incSize;
        }
    }
}
