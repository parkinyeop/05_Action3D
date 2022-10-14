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
        float num = Random.Range(0, 1f);
        int select ;
        if(num < 0.7f)
        {
            select = 0;
        }
        else if (num < 0.9f)
        {
            select = 1;
        }
        else if (num < 0.94)
        {
            select = 2;
        }
        else if (num < 0.97)
        {
            select = 3;
        }
        else
        {
            select = 4;
        }

        return select;
    }


}
