using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomIdleSelector : StateMachineBehaviour
{
    

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!animator.IsInTransition(0))
        animator.SetInteger("IdleSelect", RandomeSelect());
    }

    int RandomeSelect()
    {
        int selelct = Random.Range(0, 5);

        return selelct;
    }


}
