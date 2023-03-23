using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomizer : StateMachineBehaviour
{
    readonly float MinTime = 1;
    readonly float MaxTime = 3;

    float timer = 0;
    string[] fidgetTriggers = {"Fidgeting", "talking", "looking around updated"};

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0)
        {
            randomAnimation(animator);
            timer = Random.Range(MinTime, MaxTime);
        }
        else 
        {
            timer -= Time.deltaTime;
        }
    }

    void randomAnimation(Animator animator)
    {
        System.Random rnd = new System.Random();
        int animPosition = rnd.Next(fidgetTriggers.Length);
        string fidgetTrigger = fidgetTriggers[animPosition];
        animator.SetTrigger(fidgetTrigger);
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
