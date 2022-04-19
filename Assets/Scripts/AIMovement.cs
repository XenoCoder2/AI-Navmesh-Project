using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    public int waypointValue;
    public Transform endOfMaze;
    public List<GameObject> collectables;
    public List<GameObject> keys;
    public List<GameObject> doors;
    public List<Transform> guides;
    protected NavMeshAgent _navAgent;
}
