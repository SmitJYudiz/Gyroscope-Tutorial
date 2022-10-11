using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroControl : MonoBehaviour
{
    //smit's work

    #region Variables

    private bool gyroEnabled;
    private Gyroscope gyro;
    private GameObject cameraContainer;
    private Quaternion rot;

    //additional variables for changing views feature,
    //if you do not want to have that feature, skip the below two variables
    int currentPositionIndex;
    public List<Transform> setOfPositionsOfDifferentViews;

    #endregion

    #region Unity Methods

    private void Start()
    {
        OnChangeView();

        cameraContainer = new GameObject("Camera Container");

        cameraContainer.transform.position = transform.position;
        transform.SetParent(cameraContainer.transform);

        gyroEnabled = EnableGyro();

        currentPositionIndex  = 0;
    }

    private void Update()
    {
        if(gyroEnabled)
        {
            transform.localRotation = gyro.attitude * rot;
        }        
    }

    #endregion

    #region Public Methods

    bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            cameraContainer.transform.rotation = Quaternion.Euler(90f, 90f, 0f);
            rot = new Quaternion(0, 0, 1, 0);

            return true;
        }
        return false;
    }


    //This methods helps in achieving changing views in game, If you do not want that feature
    //you may skip the method defined below and the usages of it.
    public void OnChangeView()
    {
        if (setOfPositionsOfDifferentViews.Count >0 && setOfPositionsOfDifferentViews!=null)
        {
            transform.position = setOfPositionsOfDifferentViews[currentPositionIndex].position;
            transform.rotation = setOfPositionsOfDifferentViews[currentPositionIndex].rotation;
         
            currentPositionIndex++;
            if(currentPositionIndex >= setOfPositionsOfDifferentViews.Count)
            {
                currentPositionIndex = 0;
            }           
        }
    }

    #endregion
}

//If you have any doubts or any suggestions you may ping me at
//my linkedin: https://www.linkedin.com/in/smit-j-a270a71b7

