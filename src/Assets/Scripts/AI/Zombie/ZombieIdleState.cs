using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieIdleState : StateMachineBehaviour
{
    float timer;
    public float idleTime = 0f;

    Transform player;

    public float detectionArea = 18f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if(timer > idleTime)
        {
            animator.SetBool("isPatrolling", true);
        }

        float distancefromPlayer = Vector3.Distance(player.position, animator.transform.position);
        if(distancefromPlayer < detectionArea)
        {
            animator.SetBool("isChasing", true);
        }
    }
}

