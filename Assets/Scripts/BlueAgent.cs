using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueAgent : AIMovement
{
    public enum States
    {
        Wandering,
        Collecting,
        KeySearch,
        NavigatingToEnd,
    }
    public States agentStates;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        //collectables.AddRange(GameObject.FindGameObjectsWithTag("BlueCollect"));
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public void NextState()
    {
        switch (agentStates)
        {
            case States.Wandering:
                StartCoroutine(WanderingState());
                break;
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

    private IEnumerator WanderingState()
    {
        while (agentStates == States.Wandering)
        {

            yield return null;
        }

        NextState();
    }

    private IEnumerator CollectingState()
    {
        while (agentStates == States.Collecting)
        {


            yield return null;
        }

        NextState();
    }

    private IEnumerator KeySearchState()
    {
        while (agentStates == States.KeySearch)
        {

            yield return null;
        }

        NextState();
    }
    private IEnumerator EndNavState()
    {
        while (agentStates == States.NavigatingToEnd)
        {

            yield return null;
        }

        NextState();
    }
}
