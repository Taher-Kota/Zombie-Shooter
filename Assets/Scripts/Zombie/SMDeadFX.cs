using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMDeadFX : StateMachineBehaviour
{
    public int index;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<ZombieController>().DeadFX(index);
    }
}
