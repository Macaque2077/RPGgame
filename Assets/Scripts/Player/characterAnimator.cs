using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class characterAnimator : MonoBehaviour
{
    //holds the atatck that can be replaced
    public AnimationClip replaceableAttackAnim;

    //public array of attack anims
    public AnimationClip[] defaultAttackAnimSet;

    //array of our attack animations that can be swapped out for the player depending on their weapon
    protected AnimationClip[] currentAttackAnimSet;

    const float locomotionAnimationSmoothTime = .1f;

    NavMeshAgent agent;
    protected Animator animator;
    protected CharacterCombat combat;
    //for determine the kind of attack to animate
    protected AnimatorOverrideController overrideController;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>(); // search through all of the child objects for the animator component
        combat = GetComponent<CharacterCombat>();

        //provide the method to swap out animations so is used to swap between gun or sword attacks
        overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = overrideController;

        currentAttackAnimSet = defaultAttackAnimSet;
        combat.OnAttack += OnAttack;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime); // adjusts speed and takes 0.1 tiem to smooth between values

        //set the combat on
        animator.SetBool("inCombat", combat.InCombat);
    }

    protected virtual void OnAttack()
    {
        animator.SetTrigger("attack");
        int attackIndex = Random.Range(0, currentAttackAnimSet.Length);
        overrideController[replaceableAttackAnim.name] = currentAttackAnimSet[attackIndex];
    }
}
