using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MainMenuNavAgents : MonoBehaviour
{
    private NavMeshAgent _navAgent;
    [SerializeField] float _randomDistanceRadius = 5f;
    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        _anim = GetComponentInChildren<Animator>();
    }

    public void UpdateAnim()
    {
        if (_navAgent.velocity.magnitude < 0.01f)
        {
            _anim.SetBool("IsWalking", false);
        }
        else
        {
            _anim.SetBool("IsWalking", true);
        }
    }

    private void Update()
    {
        RandomMove();
        UpdateAnim();
    }

    void RandomMove()
    {
        if (_navAgent.remainingDistance < 0.1f && _navAgent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-_randomDistanceRadius, _randomDistanceRadius), 0, Random.Range(-_randomDistanceRadius, _randomDistanceRadius));
            Vector3 newPosition = _navAgent.transform.position + randomOffset;
            _navAgent.SetDestination(newPosition);
        }
        
        
    }
}
