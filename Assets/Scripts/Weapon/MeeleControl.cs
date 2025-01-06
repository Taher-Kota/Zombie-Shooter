using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleControl : WeaponControl
{
    public override void ProcessAttack()
    {
        //base.ProcessAttack();
        AudioManager.instance.MeleeSound();
    }
}
