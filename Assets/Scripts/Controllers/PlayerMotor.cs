using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    
    Transform target;     //target to follow
    NavMeshAgent agent;   // reference to our target

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
            faceTarget();
        }
    }

    private void faceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized; // direction towards the target
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z)); // find out how we should rotate the  player to look in the direction avoiding changes on the y
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // smooth the interpolate toward that rotation
    }

    // Update is called once per frame
    public void MoveToPoint (Vector3 point)
    {
        agent.SetDestination(point);
    }

    //follow the players focus probably wont be needed for sdv6 as enemies wont be moving
    public void followTarget(Interactable newTarget)
    {
        agent.stoppingDistance = newTarget.radius * .8f;  //set the distance away from the target we want the player to stop at (0.8 is so that we are inside the yellow sphere around interactables)
        agent.updateRotation = false; // we want to keep player rotating if the focus moves while player is still in interaction area (yellow sphere)
        target = newTarget.interactionTransform;
    }

    public void stopFollowingTarget()
    {
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;
        target = null;
    }
}
