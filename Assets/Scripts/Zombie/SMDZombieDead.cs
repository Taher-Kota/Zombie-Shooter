using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMDZombieDead : StateMachineBehaviour
{
    public int NumberRandomDead;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int rand = Random.Range(1, NumberRandomDead + 1);
        animator.SetInteger(TagManager.RANDOM_PARAMETER, rand);
    }

}
