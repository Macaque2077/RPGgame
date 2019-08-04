using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class characterAnimator : MonoBehaviour
{

    const float locomotionAnimationSmoothTime = .1f;

    Animator animator;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>(); // search through all of the child objects for the animator component

    }

    // Update is called once per frame
    void Update()
    {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime); // adjusts speed and takes 0.1 tiem to smooth between values
    }
}
