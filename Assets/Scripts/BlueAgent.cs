using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlueAgent : AIMovement
{
    public enum States
    {
        Collecting,
        KeySearch,
        NavigatingToEnd,
    }
    public States agentStates;

    public int blueValue;
    private int blueSearch;
    public static int blueKeyCount;
    public int keySearch;

    // Start is called before the first frame update
    public void Start()
    {
        Application.targetFrameRate = 165;
        _navAgent = GetComponent<NavMeshAgent>();
        collectables.AddRange(GameObject.FindGameObjectsWithTag("BlueCollect"));
        agentStates = States.NavigatingToEnd;
        NextState();

    }

    // Update is called once per frame
    public void Update()
    {

    }

    public void NextState()
    {
        switch (agentStates)
        {
            case States.Collecting:
                StartCoroutine(CollectingState());
                break;
            case States.KeySearch:
                StartCoroutine(KeySearchState());
                break;
            case States.NavigatingToEnd:
                StartCoroutine(EndNavState());
                break;
            default:
                break;
        }
    }

    private IEnumerator CollectingState()
    {
        while (agentStates == States.Collecting)
        {
            if (collectables[blueSearch] != null)
            {
                if (blueValue != collectables.Count)
                {
                    _navAgent.SetDestination(collectables[blueSearch].transform.position);
                }
                else
                {
                    agentStates = States.NavigatingToEnd;
                }

                if (Vector2.Distance(transform.position, collectables[blueSearch].transform.position) > 30f || blueValue == collectables.Count)
                {
                    agentStates = States.NavigatingToEnd;
                }

            }
            else
            {
                agentStates = States.NavigatingToEnd;
            }

            yield return null;
        }

        NextState();

    }

    private IEnumerator KeySearchState()
    {
        while (agentStates == States.KeySearch)
        {
            if (blueKeyCount > 0 || keys[keySearch] == null)
            {
                agentStates = States.NavigatingToEnd;
                NextState();
            }
            else if (blueKeyCount == 0 && keys[keySearch] != null)
            {
                _navAgent.SetDestination(keys[keySearch].transform.position);
            }

            yield return null;
        }

        NextState();
    }

    private IEnumerator EndNavState()
    {
        while (agentStates == States.NavigatingToEnd)
        {
            if (waypointValue != guides.Count)
            {
                _navAgent.SetDestination(guides[waypointValue].position);

                if (Vector3.Distance(transform.position, guides[waypointValue].position) <= 1.5f)
                {
                    waypointValue++;
                }
            }

            if (collectables[blueSearch] != null)
            {
                if (Vector2.Distance(transform.position, collectables[blueSearch].transform.position) <= 30f && agentStates != States.KeySearch)
                {
                    agentStates = States.Collecting;
                }
            }
            

            yield return null;
        }

        NextState();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("BlueCollect"))
        {
            blueValue += other.gameObject.GetComponent<Collectable>().value;
            if (blueSearch != collectables.Count - 1)
            {
                blueSearch += other.gameObject.GetComponent<Collectable>().value;
            }
            Destroy(other.gameObject);
        }

        if (other.CompareTag("BlueKey"))
        {
            blueKeyCount += 1;
            doors[keySearch].GetComponent<KeyDoor>().doorActive = true;
            Destroy(other.gameObject);
        }

        if (other.CompareTag("BD"))
        {
            if (blueKeyCount == 0 && other.gameObject != null)
            {
                keySearch = other.gameObject.GetComponent<KeyDoor>().doorValue;
                agentStates = States.KeySearch;
                NextState();
            }
        }
    }
}
