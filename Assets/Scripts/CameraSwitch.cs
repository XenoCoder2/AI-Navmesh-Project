using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject[] aiCameras;
    public GameObject[] otsCameras;
    public int curCamera;
    private int _viewType;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            int otsCam = 0;

            if (curCamera != 0)
            {
                curCamera--;
            }
            else
            {
                curCamera = aiCameras.Length - 1;
            }

            for (int i = 0; i < otsCameras.Length; i++)
            {
                if (otsCameras[i].activeInHierarchy)
                {
                    otsCam = i;
                }
            }

            for (int i = 0; i < aiCameras.Length; i++)
            {
                if (_viewType == 0)
                {
                    if (i == curCamera)
                    {
                        aiCameras[curCamera].SetActive(true);
                    }
                    else
                    {
                        aiCameras[i].SetActive(false);
                    }
                }
                else
                {
                    if (i == curCamera)
                    {
                        otsCameras[curCamera].SetActive(true);
                    }
                    else
                    {
                        otsCameras[otsCam].SetActive(false);

                    }
                }

            }

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            int otsCam = 0;

            if (curCamera != aiCameras.Length - 1)
            {
                curCamera++;
            }
            else
            {
                curCamera = 0;
            }

            for (int i = 0; i < otsCameras.Length; i++)
            {
                if (otsCameras[i].activeInHierarchy)
                {
                    otsCam = i;
                }
            }

            for (int i = 0; i < aiCameras.Length; i++)
            {
                if (_viewType == 0)
                {
                    if (i == curCamera)
                    {
                        aiCameras[curCamera].SetActive(true);
                    }
                    else
                    {
                        aiCameras[i].SetActive(false);
                    }
                }
                else
                {
                    if (i == curCamera)
                    {
                        otsCameras[curCamera].SetActive(true);
                    }
                    else
                    {
                        otsCameras[otsCam].SetActive(false);

                    }
                }

            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (_viewType != 1)
            {
                _viewType++;
            }
            else
            {
                _viewType = 0;
            }

            for (int i = 0; i < otsCameras.Length; i++)
            {
                if (_viewType == 1)
                {
                    otsCameras[curCamera].SetActive(true);
                    aiCameras[curCamera].SetActive(false);
                }
                else
                {
                    aiCameras[curCamera].SetActive(true);
                    otsCameras[curCamera].SetActive(false);
                }
            }
        }
    }
}
