using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GreenAgent : AIMovement
{
    public int greenValue;
    private int _greenSearch;
    public static int greenKeyCount;
    public int keySearch;
    private float _navDist;
    private float _collectDist;


    // Start is called before the first frame update
    public void Start()
    {
        Application.targetFrameRate = 165;
        _navAgent = GetComponent<NavMeshAgent>();
        collectables.AddRange(GameObject.FindGameObjectsWithTag("GreenCollect"));
        agentStates = States.NavigatingToEnd;
        NextState();

    }

    protected override void Update()
    {
        base.Update();
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
            if (collectables[_greenSearch] != null)
            {
                if (greenValue != collectables.Count)
                {
                    _navAgent.SetDestination(collectables[_greenSearch].transform.position);
                }
                else
                {
                    agentStates = States.NavigatingToEnd;
                }

                if (waypointValue != guides.Count - 1)
                {
                    _navDist = Vector3.Distance(transform.position, guides[waypointValue].position);
                }
                if (collectables[_greenSearch] != null)
                {
                    _collectDist = Vector3.Distance(transform.position, collectables[_greenSearch].transform.position);
                }

                if (_navDist < _collectDist && collectables[_greenSearch] != null)
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
            if (greenKeyCount > 0 || keys[keySearch] == null)
            {
                agentStates = States.NavigatingToEnd;
                NextState();
            }
            else if (greenKeyCount == 0 && keys[keySearch] != null)
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

                if (Vector3.Distance(transform.position, guides[waypointValue].position) <= 1.5f && waypointValue != guides.Count - 1)
                {
                    waypointValue++;
                }
            }

            if (waypointValue != guides.Count - 1)
            {
                _navDist = Vector3.Distance(transform.position, guides[waypointValue].position);
            }
            if (collectables[_greenSearch] != null)
            {
                _collectDist = Vector3.Distance(transform.position, collectables[_greenSearch].transform.position);
            }
           
            if (_navDist > _collectDist && collectables[_greenSearch] != null)
            {
                agentStates = States.Collecting;
            }

            yield return null;
        }

        NextState();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("GreenCollect"))
        {
            greenValue += other.gameObject.GetComponent<Collectable>().value;
            if (_greenSearch != collectables.Count - 1)
            {
                _greenSearch += other.gameObject.GetComponent<Collectable>().value;
            }
            Destroy(other.gameObject);
        }

        if (other.CompareTag("GreenKey"))
        {
            keysCollected++;
            greenKeyCount += 1;
            doors[keySearch].GetComponent<KeyDoor>().doorActive = true; 
            Destroy(other.gameObject);
        }

        if (other.CompareTag("GD"))
        {
            if (greenKeyCount == 0 && other.gameObject != null)
            {
                keySearch = other.gameObject.GetComponent<KeyDoor>().doorValue;
                agentStates = States.KeySearch;
                NextState();
            }
        }

        if (other.CompareTag("End"))
        {
            finishedMaze = true;
        }
    }
}
