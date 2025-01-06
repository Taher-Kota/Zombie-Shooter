using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public List<WeaponControl> weapon_unlocked;
    public WeaponControl[] total_weapon;

    private int Current_Weapon_Index;
    [HideInInspector]
    public WeaponControl Current_Weapon;

    private PlayerAnimation playerAnimation;

    private PlayerArmController[] armControl;

    private bool isShooting = true;

    public GameObject MeeleDamagePoint;

    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        Current_Weapon_Index = 0;
        LoadActiveWeapon();
    }

    void Start()
    {
        armControl = GetComponentsInChildren<PlayerArmController>();
        ChangeWeapon(weapon_unlocked[Current_Weapon_Index]);
        playerAnimation.SwitchWeaponAnimation((int)weapon_unlocked[Current_Weapon_Index].GunConfig.typeWeapon);
        Current_Weapon = weapon_unlocked[Current_Weapon_Index];
    }

    void LoadActiveWeapon()
    {
        weapon_unlocked.Add(total_weapon[0]);

        for (int i = 1; i < total_weapon.Length; i++)
        {
            weapon_unlocked.Add(total_weapon[i]);
        }
    }
    
    public void SwitchWeapon()
    {
        Current_Weapon_Index++;

        Current_Weapon_Index = (Current_Weapon_Index >= weapon_unlocked.Count) ? 0 : Current_Weapon_Index;
        playerAnimation.SwitchWeaponAnimation((int)weapon_unlocked[Current_Weapon_Index].GunConfig.typeWeapon);
        ChangeWeapon(weapon_unlocked[Current_Weapon_Index]);
    }

    void ChangeWeapon(WeaponControl newWeapon)
    {
        if (Current_Weapon)
        {
            Current_Weapon.gameObject.SetActive(false);
        }

        Current_Weapon = newWeapon;
        Current_Weapon.gameObject.SetActive(true);
        if (Current_Weapon_Index != 0)
        {           
            if (Current_Weapon.GunConfig.typeWeapon == TypeWeapon.TwoHanded)
            {
                if (GameManager.instance.Character_Index != 1)
                {
                    for (int i = 0; i < armControl.Length; i++)
                    {
                        armControl[i].ChangeToTwoHand();
                    }
                }
                else
                {
                    playerAnimation.MarryIdle();
                    for (int i = 0; i < armControl.Length; i++)
                    {
                        armControl[i].ChangeToTwoHand();
                    }
                }
            }
            else
            {
                playerAnimation.Idle();
                for (int i = 0; i < armControl.Length; i++)
                {
                    armControl[i].ChangeToOneHand();
                }
            }
        }
    }

    public void Attack()
    {
        if(Current_Weapon.GunConfig.typeControl == TypeControl.Hold)
        {
            Current_Weapon.CallAttack();
        }
        else if(Current_Weapon.GunConfig.typeControl == TypeControl.Click)
        {
            if (!isShooting)
            {
                Current_Weapon.CallAttack();
                isShooting = true;
            }
        }
    }

    public void ResetAttack()
    {
        isShooting = false;
    }

    public void ActivateMeeleColision()
    {
        MeeleDamagePoint.SetActive(true);
    }
    public void DeActivateMeeleColision()
    {
        MeeleDamagePoint.SetActive(false);
    }
}
