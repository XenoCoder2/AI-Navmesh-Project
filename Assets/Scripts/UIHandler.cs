using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public GameObject[] scenes;
    public GameObject[] uiPanels;

    [SerializeField] CameraSwitch _camSwitch;
    [SerializeField] BlueAgent _agentBlue;
    [SerializeField] GreenAgent _agentGreen;
    [SerializeField] YellowAgent _agentYellow;
    [SerializeField] Text _stateText;
    [SerializeField] Text _collectablesText;
    [SerializeField] Text _keysText;
    [SerializeField] Text _finishedText;
    [SerializeField] Text _agentTypeText;

    private bool _paused;

    // Update is called once per frame
    void Update()
    {
        if (scenes[1].activeInHierarchy && !uiPanels[3].activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && _paused == false)
            {
                Pause();
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && _paused == true)
            {
                Unpause();
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                if (uiPanels[1].activeInHierarchy)
                {
                    uiPanels[1].SetActive(false);
                }
                else
                {
                    uiPanels[1].SetActive(true);
                }

            }

            ChangeUIInfo();

        }

        if (uiPanels[3].activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(0);
                scenes[0].SetActive(true);
                uiPanels[0].SetActive(true);
                uiPanels[2].SetActive(false);
                uiPanels[1].SetActive(false);
                scenes[1].SetActive(false);
                Time.timeScale = 1;
                _paused = false;
            }
        }
    }

    void Start()
    {
        Time.timeScale = 1;
    }

    void ChangeUIInfo()
    {
        switch (_camSwitch.curCamera)
        {
            case 0:
                _agentTypeText.text = "Agent Type: Platformer";
                _stateText.text = "Current State: " + _agentBlue.agentStates;
                _collectablesText.text = "Collectables: " + _agentBlue.blueValue + " / " + _agentBlue.collectables.Count;
                _keysText.text = "Keys: " + _agentBlue.keysCollected + " / 3";

                if (_agentBlue.finishedMaze)
                {
                    _finishedText.text = "Finished: Yes";
                }
                else
                {
                    _finishedText.text = "Finished: No";
                }

                break;
            case 1:
                _agentTypeText.text = "Agent Type: Ogre";
                _stateText.text = "Current State: " + _agentGreen.agentStates;
                _collectablesText.text = "Collectables: " + _agentGreen.greenValue + " / " + _agentGreen.collectables.Count;
                _keysText.text = "Keys: " + _agentGreen.keysCollected + " / 3";

                if (_agentGreen.finishedMaze)
                {
                    _finishedText.text = "Finished: Yes";
                }
                else
                {
                    _finishedText.text = "Finished: No";
                }

                break;
            case 2:
                _agentTypeText.text = "Agent Type: Human";
                _stateText.text = "Current State: " + _agentYellow.agentStates;
                _collectablesText.text = "Collectables: " + _agentYellow.yellowValue + " / " + _agentYellow.collectables.Count;
                _keysText.text = "Keys: " + _agentYellow.keysCollected + " / 3";

                if (_agentYellow.finishedMaze)
                {
                    _finishedText.text = "Finished: Yes";
                }
                else
                {
                    _finishedText.text = "Finished: No";
                }

                break;
            default:
                break;

        }

        if (_agentBlue.finishedMaze && _agentGreen.finishedMaze && _agentYellow.finishedMaze)
        {
            uiPanels[3].SetActive(true); 
            uiPanels[1].SetActive(false);

        }
    }

    public void StartButton()
    {
        scenes[1].SetActive(true);
        uiPanels[1].SetActive(true);
        uiPanels[0].SetActive(false);
        scenes[0].SetActive(false);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void Pause()
    {
        uiPanels[2].SetActive(true);
        Time.timeScale = 0;
        _paused = true;
    }
    public void Unpause()
    {
        uiPanels[2].SetActive(false);
        Time.timeScale = 1;
        _paused = false;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
        scenes[0].SetActive(true);
        uiPanels[0].SetActive(true);
        uiPanels[2].SetActive(false);
        uiPanels[1].SetActive(false);
        scenes[1].SetActive(false);
        Time.timeScale = 1;
        _paused = false;
    }
}
