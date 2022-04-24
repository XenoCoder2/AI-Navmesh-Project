using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class YellowAgent : AIMovement
{
    public enum States
    {
        Collecting,
        KeySearch,
        NavigatingToEnd,
    }
    public States agentStates;

    public int yellowValue;
    private int yellowSearch;
    public static int yellowKeyCount;
    public int keySearch;
    private float _navDist;
    private float _collectDist;


    // Start is called before the first frame update
    public void Start()
    {
        Application.targetFrameRate = 165;
        _navAgent = GetComponent<NavMeshAgent>();
        collectables.AddRange(GameObject.FindGameObjectsWithTag("YellowCollect"));
        agentStates = States.NavigatingToEnd;
        NextState();

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
            if (collectables[yellowSearch] != null)
            {
                if (yellowValue != collectables.Count)
                {
                    _navAgent.SetDestination(collectables[yellowSearch].transform.position);
                }
                else
                {
                    agentStates = States.NavigatingToEnd;
                }

                if (waypointValue != guides.Count - 1)
                {
                    _navDist = Vector3.Distance(transform.position, guides[waypointValue].position);
                }
                if (collectables[yellowSearch] != null)
                {
                    _collectDist = Vector3.Distance(transform.position, collectables[yellowSearch].transform.position);
                }

                if (_navDist < _collectDist && collectables[yellowSearch] != null)
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
            if (yellowKeyCount > 0 || keys[keySearch] == null)
            {
                agentStates = States.NavigatingToEnd;
                NextState();
            }
            else if (yellowKeyCount == 0 && keys[keySearch] != null)
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
            if (collectables[yellowSearch] != null)
            {
                _collectDist = Vector3.Distance(transform.position, collectables[yellowSearch].transform.position);
            }

            if (_navDist > _collectDist && collectables[yellowSearch] != null)
            {
                agentStates = States.Collecting;
            }

            yield return null;
        }

        NextState();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("YellowCollect"))
        {
            yellowValue += other.gameObject.GetComponent<Collectable>().value;
            if (yellowSearch != collectables.Count - 1)
            {
                yellowSearch += other.gameObject.GetComponent<Collectable>().value;
            }
            Destroy(other.gameObject);
        }

        if (other.CompareTag("YellowKey"))
        {
            yellowKeyCount += 1;
            doors[keySearch].GetComponent<KeyDoor>().doorActive = true;
            if (keySearch == 1)
            {
                keySearch = 2;
            }
            Destroy(other.gameObject);
        }

        if (other.CompareTag("YD"))
        {
            if (yellowKeyCount == 0 && other.gameObject != null)
            {
                keySearch = other.gameObject.GetComponent<KeyDoor>().doorValue;
                agentStates = States.KeySearch;
                NextState();
            }
        }
    }
}
