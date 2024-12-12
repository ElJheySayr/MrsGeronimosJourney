using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CyrController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    public PlayerState playerState;
    public List<Transform> destinationTransforms;
    private int destinationIndex;
    private bool patrolCooldown;

    protected virtual void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        UpdateAnimations();

        if(Level2_Manager.instance.gameOn && !patrolCooldown)
        {
            Patrol();
        }    
    }

    public enum PlayerState
    {
        Idle,
        Walking,
        Talking
    }

    private void Patrol()
    {
        if(Vector3.Distance(transform.position, destinationTransforms[destinationIndex].position) <= 3f)
        {   
            destinationIndex = (destinationIndex + 1) % destinationTransforms.Count;
            StartCoroutine(delayPatrol());
        }

        if(!patrolCooldown)
        {
            navMeshAgent.SetDestination(destinationTransforms[destinationIndex].position);
            playerState = PlayerState.Walking;   
        }      
    }

    private IEnumerator delayPatrol()
    {
        playerState = PlayerState.Idle;  
        patrolCooldown = true;   
        yield return new WaitForSeconds(3f);
        patrolCooldown = false; 
    }

    private void UpdateAnimations()
    {
        animator.SetBool("isIdle", playerState == PlayerState.Idle);
        animator.SetBool("isWalking", playerState == PlayerState.Walking);
        animator.SetBool("isTalking", playerState == PlayerState.Talking);
    }
}
