using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//This class controls all NavAgents on the main menu screen.
public class MainMenuNavAgents : MonoBehaviour
{
    #region Variables
    //A private reference to the NavMeshAgent.
    private NavMeshAgent _navAgent;
    [Header("Radius to Move")]
    //The radius that the agent will travel at a time.
    [SerializeField] float _randomDistanceRadius = 5f;
    //A reference to the animator component.
    private Animator _anim;
    #endregion
    
    #region Start Method
    // Start is used to get the variable references from the agent gameObject.
    void Start()
    {
        //Attach the NavMeshAgent to _navAgent.
        _navAgent = GetComponent<NavMeshAgent>();
        //Attach the Animator from the child component to _anim.
        _anim = GetComponentInChildren<Animator>();
    }
    #endregion

    #region Update Method
    //The Update method will run the RandomMove and UpdateAnim methods continuously. 
    private void Update()
    {
        //Run the RandomMove method.
        RandomMove();
        //Run the UpdateAnim method.
        UpdateAnim();
    }
    #endregion

    #region UpdateAnim Method
    public void UpdateAnim()
    {
        //If the velocity magnitude of the _navAgent is less than 0.01f.
        if (_navAgent.velocity.magnitude < 0.01f)
        {
            //Set the animator's IsWalking bool to false.
            _anim.SetBool("IsWalking", false);
        }
        //Else
        else
        {
            //Set the animator's IsWalking bool to true.
            _anim.SetBool("IsWalking", true);
        }
    }
    #endregion

    #region RandomMove Method
    //Random Move
    // - Checks whether the agent has finished its path.
    // - Finds a random distance between two points and sets the agent's destination to the point chosen.
    void RandomMove()
    {
        //If the remaining distance of the path is less than 0.1f and the agent has finished the path.
        if (_navAgent.remainingDistance < 0.1f && _navAgent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            //Create a Vector3 variable randomOffset and set it to a random x and y value contained by _randomDistanceRadius.
            Vector3 randomOffset = new Vector3(Random.Range(-_randomDistanceRadius, _randomDistanceRadius), 0, Random.Range(-_randomDistanceRadius, _randomDistanceRadius));
            //Create a Vector3 variable newPosition and set it the current transform.position of the agent increased by the randomOffset variable.
            Vector3 newPosition = _navAgent.transform.position + randomOffset;
            //Set the agent's destination to newPosition.
            _navAgent.SetDestination(newPosition);
        }
        
        
    }
    #endregion
}
