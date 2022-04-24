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
    public Animator anim;
    protected NavMeshAgent _navAgent;
    protected float moveThreshold = 0.01f;

    protected virtual void Update()
    {
        UpdateAnim();
        ChangeAreaSpeed();
    }

    public void UpdateAnim()
    {
        if (_navAgent.velocity.magnitude < moveThreshold)
        {
            anim.SetBool("IsWalking", false);
        }
        else
        {
            anim.SetBool("IsWalking", true);
        }
    }

    public void ChangeAreaSpeed()
    {
        NavMeshHit hitNav;

        _navAgent.SamplePathPosition(-1, 0.0f, out hitNav);

        int grass = NavMesh.GetAreaFromName("High Cost Grass");

        if (hitNav.mask == grass)
        {
            _navAgent.speed = 1.5f;
        }
        else
        {
            _navAgent.speed = 3.5f;
        }
    }

}
