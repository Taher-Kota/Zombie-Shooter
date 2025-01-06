using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponName
{
    Bat,
    PISTOL,
    MP5,
    M3,
    AK47,
    AWP,
    FLAMER,
    ROCKET
}

public class WeaponControl : MonoBehaviour
{
    public static WeaponControl instance;
    public DefaultConfig GunConfig;
    public WeaponName weapon;

    protected PlayerAnimation playeranim;
    protected float LastShot;
    public int CurrentBullets;
    public int TotalBullets;

    void Awake()
    {
        instance = this;
        playeranim = GetComponentInParent<PlayerAnimation>();
        CurrentBullets = TotalBullets;
    }

    public void CallAttack()
    {
        
        if (Time.time > LastShot + GunConfig.FireRate)
        {
            if (CurrentBullets > 0)
            {
                ProcessAttack();               
                playeranim.AttackAnimation();               
                LastShot = Time.time;
                CurrentBullets--;
            }
            else
            {
                AudioManager.instance.NoAmmoSound();
            }
        }
    }

    public virtual void ProcessAttack()
    {

    }
}
