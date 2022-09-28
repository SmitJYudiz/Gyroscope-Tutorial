using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GpsBehaviour : MonoBehaviour
{
    //smit's work
    public float latitude;
    public float longitude;

    public static GpsBehaviour instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(StartLocationService());
    }

    private IEnumerator StartLocationService()
    {
        if (Input.location.isEnabledByUser)
        {
            Debug.Log("User has not enabled the gps");
            yield break;
        }

        Input.location.Start();

        int maxWait = 20;

        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait <= 0)
        {
            Debug.Log("Timed Out");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine location");
            yield break;
        }

        //all above tests successfully passed...

        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;
        yield break;
    }
}
