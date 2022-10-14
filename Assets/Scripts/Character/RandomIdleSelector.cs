using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomIdleSelector : StateMachineBehaviour
{


    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!animator.IsInTransition(0))
            animator.SetInteger("IdleSelect", RandomeSelect());
    }

    int RandomeSelect()
    {
        int randomIdle;
        int select;
        randomIdle = Random.Range(0, 101);
        if (randomIdle <= 3)
        {
            select = 5;
        }
        else if (randomIdle <= 6)
        {
            select = 4;
        }
        else if (randomIdle <= 10)
        {
            select = 3;
        }
        else if (randomIdle <= 30)
        {
            select = 2;
        }
        else
        {
            select = 1;
        }

        return select;
    }


}
