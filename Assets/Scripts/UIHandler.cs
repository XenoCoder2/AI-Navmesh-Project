using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    #region Variables
    //The gameobjects controlling the scenes of the pathfinding project (Main Menu and Pathfinding)
    public GameObject[] scenes;
    //The different uiPanels of the pathfinding project.
    public GameObject[] uiPanels;
    //The CameraSwitch script used in the scene.
    [SerializeField] CameraSwitch _camSwitch;
    //The BlueAgent script used in the scene.
    [SerializeField] BlueAgent _agentBlue;
    //The GreenAgent script used in the scene.
    [SerializeField] GreenAgent _agentGreen;
    //The YellowAgent script used in the scene.
    [SerializeField] YellowAgent _agentYellow;
    //The Text that refers to the current state of the agent.
    [SerializeField] Text _stateText;
    //The Text that refers to the amount of collectables an agent has picked up.
    [SerializeField] Text _collectablesText;
    //The Text that refers to the amount of keys an agent has picked up.
    [SerializeField] Text _keysText;
    //The Text that refers to whether or not an agent has finished the maze or not.
    [SerializeField] Text _finishedText;
    //The Text that refers to what type of agent the AI is.
    [SerializeField] Text _agentTypeText;
    //A bool to check if the pathfinder is paused or not.
    private bool _paused;
    #endregion

    #region Start Method
    //Start will ensure that the timeScale is correctly set.
    void Start()
    {
        //Set the timeScale to 1.
        Time.timeScale = 1;
    }
    #endregion

    #region Update Method
    // Update is used to check whether certain scenes and panels are active and to enable certain button prompts depending on them.
    void Update()
    {
        //If the Pathfinding scene is active in the hierarchy and the pause menu is not active.
        if (scenes[1].activeInHierarchy && !uiPanels[3].activeInHierarchy)
        {
            //If the Escape key is pressed and _paused is false.
            if (Input.GetKeyDown(KeyCode.Escape) && _paused == false)
            {
                //Run the Pause method.
                Pause();
            }
            //Else if the Escape key is pressed and _paused is true.
            else if (Input.GetKeyDown(KeyCode.Escape) && _paused == true)
            {
                //Run the Unpause method.
                Unpause();
            }
            //If the H key was pressed.
            if (Input.GetKeyDown(KeyCode.H))
            {
                //If the HUD panel is active in the hierarchy.
                if (uiPanels[1].activeInHierarchy)
                {
                    //Disable the HUD.
                    uiPanels[1].SetActive(false);
                }
                //Else it is not active.
                else
                {
                    //Enable the HUD.
                    uiPanels[1].SetActive(true);
                }

            }

            //Run the ChangeUIInfo method.
            ChangeUIInfo();

        }

        //If the PostPathFind panel is active in the hierarchy.
        if (uiPanels[3].activeInHierarchy)
        {
            //If the Space key is pressed.
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Reload the current scene.
                SceneManager.LoadScene(0);
                //Enable the Main Menu scene.
                scenes[0].SetActive(true);
                //Enable the Main Menu panel.
                uiPanels[0].SetActive(true);
                //Disable the Pause panel.
                uiPanels[2].SetActive(false);
                //Disable the HUD.
                uiPanels[1].SetActive(false);
                //Disable the pathfinding scene.
                scenes[1].SetActive(false);
                //Set the timeScale back to 1.
                Time.timeScale = 1;
                //Set _paused to false.
                _paused = false;
            }
        }
    }
    #endregion

    #region ChangeUIInfo Method
    //ChangeUIInfo
    // - Switches the displayed information on the HUD depending on what camera is active.
    // - Checks if all agents have finished the maze and displays the PostPathFind panel.
    void ChangeUIInfo()
    {
        //Switch between the values of curCamera.
        switch (_camSwitch.curCamera)
        {
            //If curCamera is equal to 0 (Blue Agent).
            case 0:
                //Set the _agentTypeText to the Platformer type.
                _agentTypeText.text = "Agent Type: Platformer";
                //Set the _stateText to the agent's current state.
                _stateText.text = "Current State: " + _agentBlue.agentStates;
                //Set the _collectablesText to the amount of collectables obtained against the total collectables that are obtainable.
                _collectablesText.text = "Collectables: " + _agentBlue.blueValue + " / " + _agentBlue.collectables.Count;
                //Set the _keysText to the total amount of keys the agent has obtained.
                _keysText.text = "Keys: " + _agentBlue.keysCollected + " / 3";

                //If the Blue Agent has finished the maze.
                if (_agentBlue.finishedMaze)
                {
                    //Set the _finishedText to indicate that the agent has finished.
                    _finishedText.text = "Finished: Yes";
                }
                //Else
                else
                {
                    //Set the _finishedText to indicate that the agent hasn't finished.
                    _finishedText.text = "Finished: No";
                }
                break;
            //If curCamera is equal to 1 (Green Agent).
            case 1:
                //Set the _agentTypeText to the Ogre type.
                _agentTypeText.text = "Agent Type: Ogre";
                //Set the _stateText to the agent's current state.
                _stateText.text = "Current State: " + _agentGreen.agentStates;
                //Set the _collectablesText to the amount of collectables obtained against the total collectables that are obtainable.
                _collectablesText.text = "Collectables: " + _agentGreen.greenValue + " / " + _agentGreen.collectables.Count;
                //Set the _keysText to the total amount of keys the agent has obtained.
                _keysText.text = "Keys: " + _agentGreen.keysCollected + " / 3";

                //If the Blue Agent has finished the maze.
                if (_agentGreen.finishedMaze)
                {
                    //Set the _finishedText to indicate that the agent has finished.
                    _finishedText.text = "Finished: Yes";
                }
                //Else
                else
                {
                    //Set the _finishedText to indicate that the agent hasn't finished.
                    _finishedText.text = "Finished: No";
                }

                break;
            //If curCamera is equal to 2 (Yellow Agent).
            case 2:
                //Set the _agentTypeText to the Human type.
                _agentTypeText.text = "Agent Type: Human";
                //Set the _stateText to the agent's current state.
                _stateText.text = "Current State: " + _agentYellow.agentStates;
                //Set the _collectablesText to the amount of collectables obtained against the total collectables that are obtainable.
                _collectablesText.text = "Collectables: " + _agentYellow.yellowValue + " / " + _agentYellow.collectables.Count;
                //Set the _keysText to the total amount of keys the agent has obtained.
                _keysText.text = "Keys: " + _agentYellow.keysCollected + " / 3";

                //If the Blue Agent has finished the maze.
                if (_agentYellow.finishedMaze)
                {
                    //Set the _finishedText to indicate that the agent has finished.
                    _finishedText.text = "Finished: Yes";
                }
                //Else
                else
                {
                    //Set the _finishedText to indicate that the agent hasn't finished.
                    _finishedText.text = "Finished: No";
                }

                break;

        }

        //If all three agents have finished the maze.
        if (_agentBlue.finishedMaze && _agentGreen.finishedMaze && _agentYellow.finishedMaze)
        {
            //Enable the PostPathFind panel.
            uiPanels[3].SetActive(true);
            //Disable the HUD panel.
            uiPanels[1].SetActive(false);

        }
    }
    #endregion

    #region StartButton Method
    //Start Button 
    // - Starts the pathfinder by enabling the Pathfinder scene and HUD panel.
    // - Subsequently disables the Main Menu scene and panel.
    public void StartButton()
    {
        //Enable the Pathfinding scene.
        scenes[1].SetActive(true);
        //Enable the HUD panel.
        uiPanels[1].SetActive(true);
        //Disable the Main Menu panel.
        uiPanels[0].SetActive(false);
        //Disable the Main Menu scene.
        scenes[0].SetActive(false);
    }
    #endregion

    #region QuitButton Method
    //Quit Button
    // - Quits the Application
    public void QuitButton()
    {
        //Quit the Application.
        Application.Quit();
    }
    #endregion

    #region Pause and Unpause Methods
    //Pause
    // - Enables the Pause Panel and pauses in game time.
    public void Pause()
    {
        //Enable the Pause panel.
        uiPanels[2].SetActive(true);
        //Set the timeScale to 0.
        Time.timeScale = 0;
        //Set the _paused bool to true.
        _paused = true;
    }
    //Unpause 
    // - Disables the Pause Panel and resumes in game time.
    public void Unpause()
    {
        //Disables the pause panel.
        uiPanels[2].SetActive(false);
        //Set the timeScale to 1.
        Time.timeScale = 1;
        //Set the _paused bool to false.
        _paused = false;
    }
    #endregion

    #region Return To Main Menu Methods
    //Return To Main Menu
    // - Reloads the scene and reopens the Main Menu.
    public void ReturnToMainMenu()
    {
        //Reload the current scene.
        SceneManager.LoadScene(0);
        //Enable the Main Menu scene.
        scenes[0].SetActive(true);
        //Enable the Main menu panel.
        uiPanels[0].SetActive(true);
        //Disable the Pause panel.
        uiPanels[2].SetActive(false);
        //Disable the HUD panel.
        uiPanels[1].SetActive(false);
        //Disable the Pathfinding scene.
        scenes[1].SetActive(false);
        //Revert the timeScale to 1.
        Time.timeScale = 1;
        //Set the _paused bool to false.
        _paused = false;
    }
    #endregion
}
