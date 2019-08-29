using UnityEngine;
using UnityEngine.AI;

//for controlling enemy movement
public class EnemyController : MonoBehaviour
{

    public float lookRadius = 10f;

    //to check whether the player is in range we need a transform
    Transform target;
    //reference to the nav mesh agent required to move the enemy
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        //gets the player from the player manager and sets the target as such
        target = PlayerManager.instance.player.transform;


        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //identifies the distance from the player
        float distance = Vector3.Distance(target.position, transform.position);

        //if the distance is less than the look radius follow the player
        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                faceTarget();
                //attack 
            }
        }
    }

    //to face the player
    void faceTarget()
    {
        //get direction to target
        Vector3 direction = (target.position - transform.position).normalized;
        //get rotatio nto point to target
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        //look at the target
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime *5f);

    }


    //to visualize the look radius of the enemy
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
