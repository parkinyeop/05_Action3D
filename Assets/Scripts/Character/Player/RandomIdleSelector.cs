using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomIdleSelector : StateMachineBehaviour
{
    

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!animator.IsInTransition(0))
        //animator.SetInteger("IdleSelect", RandomeSelect());
        animator.SetInteger("IdleSelect", RandomeSelect());
    }

    int RandomeSelect()
    {
        float num = Random.Range(0, 1f);
        int select ;
        if(num < 0.25f)
        {
            select = 1;
        }
        else if (num < 0.50f)
        {
            select = 2;
        }
        else if (num < 0.75)
        {
            select = 3;
        }
        else if (num < 0.99)
        {
            select = 4;
        }
        else
        {
            select = 0;
        }

        return select;
    }


}
