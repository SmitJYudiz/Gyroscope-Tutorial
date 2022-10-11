using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GpsBehaviour : MonoBehaviour
{
    //smit's work
    public float latitude;
    public float longitude;

    public static GpsBehaviour instance;


    public TextMeshProUGUI longInfoTxt;
    public TextMeshProUGUI latInfoTxt;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(StartLocationService());
    }


    public void UpdateLocation()
    {
        StartCoroutine(StartLocationService());
    }

    private IEnumerator StartLocationService()
    {
        Debug.Log("step 1");
        if (Input.location.isEnabledByUser == false)
        {
            Debug.Log("User has not enabled the gps");
            yield break;
        }
        Debug.Log("step 1");
        Input.location.Start();

        int maxWait = 20;
        Debug.Log("step 1");
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }
        Debug.Log("step 2");
        if (maxWait <= 0)
        {
            Debug.Log("Timed Out");
            yield break;
        }
        Debug.Log("step 3");
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine location");
            yield break;
        }
        Debug.Log("step 4");
        //all above tests successfully passed...

        latitude = Input.location.lastData.latitude;
        latInfoTxt.text = latitude.ToString();
        longitude = Input.location.lastData.longitude;
        longInfoTxt.text = longitude.ToString();
        Debug.Log("step 5");
        yield break;
    }
}
