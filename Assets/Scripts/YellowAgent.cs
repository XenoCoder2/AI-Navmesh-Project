using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class YellowAgent : AIMovement
{
    #region Variables
    [Header("Total Blue Collectable Value")]
    //The score of all the collectables picked up by the yellow agent.
    public int yellowValue;
    //The search value for the next collectable.
    private int yellowSearch;
    //The key count used to check whether the agent should seek a door.
    public static int yellowKeyCount;
    [Header("Key Searcher")]
    //The keySearch variable for finding what key is associated to a found door.
    public int keySearch;
    //The distance between the agent and a waypoint.
    private float _navDist;
    //The distance between the agent and a collectable.
    private float _collectDist;
    #endregion

    #region Start Method
    //The Start method initialises all values and grabs needed components.
    public void Start()
    {
        //Get the NavMeshAgent and attach it to _navAgent.
        _navAgent = GetComponent<NavMeshAgent>();
        //Add all GameObjects tagged with BlueCollect to collectables.
        collectables.AddRange(GameObject.FindGameObjectsWithTag("YellowCollect"));
        //Set the initial state to NavigatingToEnd.
        agentStates = States.NavigatingToEnd;
        //Run the NextState method.
        NextState();
    }
    #endregion

    #region NextState Method
    //Next State
    //Switches between different states.
    // - Switches depending on the current state.
    // - Runs an associated coroutine with the state.
    public void NextState()
    {
        //Switch between each state.
        switch (agentStates)
        {
            //If the current state is Collecting.
            case States.Collecting:
                //Run the CollectingState coroutine.
                StartCoroutine(CollectingState());
                break;
            //If the current state is KeySearch.
            case States.KeySearch:
                //Run the KeySearchState coroutine.
                StartCoroutine(KeySearchState());
                break;
            //If the current state is NavigatingToEnd.
            case States.NavigatingToEnd:
                //Run the EndNavState coroutine.
                StartCoroutine(EndNavState());
                break;
        }
    }
    #endregion

    #region CollectingState Coroutine
    //Collecting State
    // - Will make the agent navigate and collect a collectable.
    private IEnumerator CollectingState()
    {
        //Whilst the current state is Collecting.
        while (agentStates == States.Collecting)
        {
            //If the currently searched collectable is not equal to false.
            if (collectables[yellowSearch] != null)
            {
                //If yellowSearch is not equal to Collectables.Count.
                if (yellowSearch != collectables.Count)
                {
                    //Set the navAgent destination to the current collectable.
                    _navAgent.SetDestination(collectables[yellowSearch].transform.position);
                }
                //Else
                else
                {
                    //Set the state to NavigatingToEnd.
                    agentStates = States.NavigatingToEnd;
                }
                //If the waypointValue is not equal to guides.Count - 1.
                if (waypointValue != guides.Count - 1)
                {
                    //Set _navDist to the distance between the agent and the currently searched waypoint.
                    _navDist = Vector3.Distance(transform.position, guides[waypointValue].position);
                }
                //If the current collectable is not equal to null.
                if (collectables[yellowSearch] != null)
                {
                    //Set _collectDist to the distance between the agent and the currently searched collectable.
                    _collectDist = Vector3.Distance(transform.position, collectables[yellowSearch].transform.position);
                }
                //If _navDist is less than _collectDist and the currently searched collectable is not equal to false.
                if (_navDist < _collectDist && collectables[yellowSearch] != null)
                {
                    //Set the current state to NavigatingToEnd.
                    agentStates = States.NavigatingToEnd;
                }

            }
            //Else, switch the state to NavigatingToEnd.
            else
            {
                //Set the current state to NavigatingToEnd.
                agentStates = States.NavigatingToEnd;
            }

            //Return a null value.
            yield return null;
        }

        //Run the NextState method.
        NextState();

    }
    #endregion

    #region KeySearchState Coroutine
    //Key Search State
    // Searches for a key for a door.
    private IEnumerator KeySearchState()
    {
        //Whilst the current state is KeySearch.
        while (agentStates == States.KeySearch)
        {
            //If the yellowKeyCount is greater than 0 and the key is not false.
            if (yellowKeyCount > 0 || keys[keySearch] == null)
            {
                //Set the state to NavigatingToEnd.
                agentStates = States.NavigatingToEnd;
                //Run the NextState method.
                NextState();
            }
            //Else if the yellowKeyCount is 0 and the currently searched key is not null.
            else if (yellowKeyCount == 0 && keys[keySearch] != null)
            {
                //Set the destination to the currently searched key.
                _navAgent.SetDestination(keys[keySearch].transform.position);
            }

            //Return a null value.
            yield return null;
        }

        //Run the NextState method.
        NextState();
    }
    #endregion

    #region EndNavState Coroutine
    //End Nav State
    // Navigates the AI through waypoints to find the end of a maze.
    private IEnumerator EndNavState()
    {
        //While the current state is NavigatingToEnd.
        while (agentStates == States.NavigatingToEnd)
        {
            //If the waypointValue is not equal to guides.Count.
            if (waypointValue != guides.Count)
            {
                //Set the destination to the currently searched waypoint.
                _navAgent.SetDestination(guides[waypointValue].position);

                //Evaluate the distance between the agent and the waypoint.
                if (Vector3.Distance(transform.position, guides[waypointValue].position) <= 1.5f && waypointValue != guides.Count - 1)
                {
                    //Increase waypointValue by 1.
                    waypointValue++;
                }
            }

            //If waypointValue is not equal to guides.Count - 1.
            if (waypointValue != guides.Count - 1)
            {
                //Set _navDist to the distance between the agent and the currently searched waypoint.
                _navDist = Vector3.Distance(transform.position, guides[waypointValue].position);
            }
            //If the currently searched collectable is not equal to null.
            if (collectables[yellowSearch] != null)
            {
                //Set _collectDist to the distance betwen the agent and the currently searched collectable.
                _collectDist = Vector3.Distance(transform.position, collectables[yellowSearch].transform.position);
            }
            //If the _navDist is greater than _collectDist and the currently searched collectable is not equal to null.
            if (_navDist > _collectDist && collectables[yellowSearch] != null)
            {
                //Set the current state to collecting.
                agentStates = States.Collecting;
            }
            //Return a null value.
            yield return null;
        }

        //Run the NextState method.
        NextState();
    }
    #endregion

    #region OnTriggerStay Method
    //Checks between different tags to run certain functions.
    private void OnTriggerStay(Collider other)
    {
        //If the other tag is BlueCollect.
        if (other.CompareTag("YellowCollect"))
        {
            //Increase yellowValue by the value variable on the colliders Collectable script.
            yellowValue += other.gameObject.GetComponent<Collectable>().value;
            //If yellowSearch is not equal to collectables.Count - 1.
            if (yellowSearch != collectables.Count - 1)
            {
                //Increase yellowSearch by the value variable on the colliders Collectable script.
                yellowSearch += other.gameObject.GetComponent<Collectable>().value;
            }

            //Destroy the colliding object.
            Destroy(other.gameObject);
        }

        //If the other tag is YellowKey.
        if (other.CompareTag("YellowKey"))
        {
            //Increase keysCollected by 1.
            keysCollected++;
            //Increase yellowKeyCount by 1.
            yellowKeyCount += 1;
            //Activate the door from the KeyDoor script on the colliding door.
            doors[keySearch].GetComponent<KeyDoor>().doorActive = true;
            //If keySearch is equal to 1.
            if (keySearch == 1)
            {
                //Set keySearch to 2 so it looks for the next key immediately.
                keySearch = 2;
            }
            //Destroy the collding key.
            Destroy(other.gameObject);
        }

        //If the other tag is BD.
        if (other.CompareTag("YD"))
        {
            //If the yellowKeyCount is 0 and the colliding object is not equal to null.
            if (yellowKeyCount == 0 && other.gameObject != null)
            {
                //Set keySearch to the doorValue on the colliding door's KeyDoor script.
                keySearch = other.gameObject.GetComponent<KeyDoor>().doorValue;
                //Set the current state to KeySearch.
                agentStates = States.KeySearch;
                //Run the NextState method.
                NextState();
            }
        }

        //If the other tag is End.
        if (other.CompareTag("End"))
        {
            //Set finishedMaze to true.
            finishedMaze = true;
        }
    }
    #endregion
}
