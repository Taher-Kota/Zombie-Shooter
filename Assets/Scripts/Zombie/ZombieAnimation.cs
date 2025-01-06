using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimation : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Attack()
    {
        anim.SetTrigger(TagManager.ATTACK_PARAMETER);
    }

    public void Hurt()
    {
        anim.SetTrigger(TagManager.GET_HURT_PARAMETER);
    }

    public void Dead()
    {
        anim.SetTrigger (TagManager.DEAD_PARAMETER);
    }
    public void MeeleeDead()
    {
        anim.Play("Dead1");
    }

    public void Idle()
    {
        anim.SetTrigger(TagManager.ZOMBIE_Idle_ANIMATION);
    }

}
