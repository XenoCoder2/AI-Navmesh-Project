using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used to switch between different perspectives and cameras on agents.
public class CameraSwitch : MonoBehaviour
{
    #region Variables
    [Header("Base AI Cameras (Bird's eye)")]
    //The base AI cameras.
    public GameObject[] aiCameras;
    [Header("Over the Shoulder AI Cameras")]
    //The Over The Shoulder Cameras.
    public GameObject[] otsCameras;
    [Header("Currently Active Camera")]
    //The current camera.
    public int curCamera;
    //The perspective, either 0 or 1. 
    // 0 = Bird's Eye Camera
    // 1 = Over the Shoulder Camera.
    private int _viewType;
    #endregion

    #region Update Method
    // Update is called once per frame
    void Update()
    {
        #region Left and Right Arrow Key If Statements
        //If the Left Arrow key was pressed. 
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //Create a variable otsCam and set it to 0.
            int otsCam = 0;

            //If curCamera is not equal to 0.
            if (curCamera != 0)
            {
                //Take away 1 from curCamera.
                curCamera--;
            }
            //else 
            else
            {
                //Set the curCamera variable to the length of aiCameras - 1.
                curCamera = aiCameras.Length - 1;
            }

            //Iterate for each value of otsCameras.
            for (int i = 0; i < otsCameras.Length; i++)
            {
                //If the currently iterated otsCamera is activeInHierarchy.
                if (otsCameras[i].activeInHierarchy)
                {
                    //Set otsCam to the active camera value.
                    otsCam = i;
                }
            }

            //Iterate for each value of aiCameras.
            for (int i = 0; i < aiCameras.Length; i++)
            {
                //If _viewType is equal to 0.
                if (_viewType == 0)
                {
                    //If the currently iterated camera is equal to curCamera.
                    if (i == curCamera)
                    {
                        //Activate the currently selected camera.
                        aiCameras[curCamera].SetActive(true);
                    }
                    //Else
                    else
                    {
                        //Deactivate the currently iterated camera.
                        aiCameras[i].SetActive(false);
                    }
                }
                //Else _viewType is equal to 1.
                else
                {
                    //If the currently iterated value is equal to curCamera.
                    if (i == curCamera)
                    {
                        //Set the otsCamera associated with the curCamera value to active.
                        otsCameras[curCamera].SetActive(true);
                    }
                    else
                    {
                        //Deactivate the otsCamera associated with the otsCam value.
                        otsCameras[otsCam].SetActive(false);

                    }
                }

            }

        }
        //Else if the Right Arrow key is pressed.
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //Create a variable otsCam and set it to 0.
            int otsCam = 0;

            //If curCamera is not equal to the length of aiCameras - 1.
            if (curCamera != aiCameras.Length - 1)
            {
                //Increase curCamera by 1.
                curCamera++;
            }
            //Else
            else
            {
                //Set curCamera is equal to 0.
                curCamera = 0;
            }
            //Iterate for each value of otsCameras.
            for (int i = 0; i < otsCameras.Length; i++)
            {
                //If the currently iterated otsCamera is active in the hierarchy.
                if (otsCameras[i].activeInHierarchy)
                {
                    //Set otsCam to the value being currently iterated.
                    otsCam = i;
                }
            }

            //Iterate for each value of aiCameras.
            for (int i = 0; i < aiCameras.Length; i++)
            {
                //If _viewType is equal to 0.
                if (_viewType == 0)
                {
                    //If the currently iterated camera is equal to curCamera.
                    if (i == curCamera)
                    {
                        //Activate the currently selected camera.
                        aiCameras[curCamera].SetActive(true);
                    }
                    //Else the currently iterated camera is not equal to curCamera.
                    else
                    {
                        //Deactivate the currently iterated camera.
                        aiCameras[i].SetActive(false);
                    }
                }
                //Else _viewType is equal to 1.
                else
                {
                    //If the currently iterated value is equal to curCamera.
                    if (i == curCamera)
                    {
                        //Activate the currently selected otsCamera.
                        otsCameras[curCamera].SetActive(true);
                    }
                    else
                    {
                        //Deactivate the otsCamera associated with the otsCam value.
                        otsCameras[otsCam].SetActive(false);

                    }
                }

            }
        }
        #endregion
        #region Up and Down Arrow Key If Statement
        //Else if the Up or Down arrow keys are pressed.
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            //If the _viewType is not equal to 1.
            if (_viewType != 1)
            {
                //Increase _viewType by 1.
                _viewType++;
            }
            //Else it is equal to 1.
            else
            {
                //Set _viewType to 0.
                _viewType = 0;
            }
            //Iterate for each value of otsCameras.
            for (int i = 0; i < otsCameras.Length; i++)
            {
                //If the _viewType is equal to 1.
                if (_viewType == 1)
                {
                    //Enable the otsCamera associated with the curCamera variable.
                    otsCameras[curCamera].SetActive(true);
                    //Disable the base aiCamera associated with the curCamera variable.
                    aiCameras[curCamera].SetActive(false);
                }
                //Else _viewType is not equal to 1.
                else
                {
                    //Enable the base aiCamera associated with the curCamera variable.
                    aiCameras[curCamera].SetActive(true);
                    //Disable the otsCamera associated with the curCamera variable.
                    otsCameras[curCamera].SetActive(false);
                }
            }
        }
        #endregion
    }
    #endregion
}
