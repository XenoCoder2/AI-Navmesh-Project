using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject[] aiCameras;
    public GameObject[] otsCameras;
    private int _curCamera;
    private int _viewType;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            int otsCam = 0;

            if (_curCamera != 0)
            {
                _curCamera--;
            }
            else
            {
                _curCamera = aiCameras.Length - 1;
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
                    if (i == _curCamera)
                    {
                        aiCameras[_curCamera].SetActive(true);
                    }
                    else
                    {
                        aiCameras[i].SetActive(false);
                    }
                }
                else
                {
                    if (i == _curCamera)
                    {
                        otsCameras[_curCamera].SetActive(true);
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

            if (_curCamera != aiCameras.Length - 1)
            {
                _curCamera++;
            }
            else
            {
                _curCamera = 0;
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
                    if (i == _curCamera)
                    {
                        aiCameras[_curCamera].SetActive(true);
                    }
                    else
                    {
                        aiCameras[i].SetActive(false);
                    }
                }
                else
                {
                    if (i == _curCamera)
                    {
                        otsCameras[_curCamera].SetActive(true);
                    }
                    else
                    {
                        otsCameras[otsCam].SetActive(false);

                    }
                }

            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
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
                    otsCameras[_curCamera].SetActive(true);
                    aiCameras[_curCamera].SetActive(false);
                }
                else
                {
                    aiCameras[_curCamera].SetActive(true);
                    otsCameras[_curCamera].SetActive(false);
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (_viewType != 0)
            {
                _viewType--;
            }
            else
            {
                _viewType = otsCameras.Length - 1;
            }

            for (int i = 0; i < otsCameras.Length; i++)
            {
                if (_viewType == 0)
                {
                    aiCameras[_curCamera].SetActive(true);
                    otsCameras[_curCamera].SetActive(false);
                }
                else
                {
                    otsCameras[_curCamera].SetActive(true);
                    aiCameras[i].SetActive(false);
                }
            }
        }
    }
}
