using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroControl : MonoBehaviour
{
    //smit's work

    private bool gyroEnabled;

    private Gyroscope gyro;

    private GameObject cameraContainer;
    private Quaternion rot;
    int currentPositionIndex;

    public List<Transform> setOfPositionsOfDifferentViews;

    private void Start()
    {
        OnChangeView();

        cameraContainer = new GameObject("Camera Container");

        cameraContainer.transform.position = transform.position;
        transform.SetParent(cameraContainer.transform);

        gyroEnabled = EnableGyro();

        currentPositionIndex  = 0;
    }

    bool EnableGyro()
    {
        if(SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            cameraContainer.transform.rotation = Quaternion.Euler(90f, 90f, 0f);
            rot = new Quaternion(0, 0, 1, 0);

            return true;
        }
        return false;
    }

    private void Update()
    {
        if(gyroEnabled)
        {
            transform.localRotation = gyro.attitude * rot;
        }
    }

    public void OnChangeView()
    {
        Debug.Log("succ 1");
        if (setOfPositionsOfDifferentViews.Count >0 && setOfPositionsOfDifferentViews!=null)
        {
            transform.position = setOfPositionsOfDifferentViews[currentPositionIndex].position;
            transform.rotation = setOfPositionsOfDifferentViews[currentPositionIndex].rotation;
            currentPositionIndex++;
            if(currentPositionIndex >= setOfPositionsOfDifferentViews.Count)
            {
                currentPositionIndex = 0;
            }
            Debug.Log("Changed Position successfully");
        }
    }
}
