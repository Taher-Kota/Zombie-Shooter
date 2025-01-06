using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void RunAnimation(bool run)
    {
        anim.SetBool(TagManager.RUN_PARAMETER, run);
    }

    public void AttackAnimation() 
    {
        anim.SetTrigger(TagManager.ATTACK_PARAMETER);
    }

    public void SwitchWeaponAnimation(int TypeWeapon)
    {
        anim.SetInteger(TagManager.TYPE_WEAPON_PARAMETER, TypeWeapon);
        anim.SetTrigger(TagManager.SWITCH_PARAMETER);
    }

    public void HurtAnimation()
    {
        anim.SetTrigger(TagManager.GET_HURT_PARAMETER);
    }

    public void DeadAnimation()
    {
        anim.SetTrigger(TagManager.DEAD_PARAMETER);
    }
    public void MarryMove()
    {
        anim.SetBool(TagManager.MarryMove, true);
    }
    public void MarryIdle()
    {
        anim.Play(TagManager.MarryIdle);
    } 
    public void Idle()
    {
        anim.Play(TagManager.Idle);
    }
    public void MarryAttack()
    {
        anim.SetTrigger(TagManager.MarryATTACK_PARAMETER);
    }
}
