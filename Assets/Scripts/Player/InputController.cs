using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private WeaponManager weapon_manager;
    [HideInInspector]
    public bool CanShoot;
    public bool isHold;

    private void Awake()
    {
        weapon_manager = GetComponent<WeaponManager>();
        CanShoot = true;
    }


    void Update()
    {
        InputCheck();
    }

    void InputCheck()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            weapon_manager.SwitchWeapon();           
        }


        if (Input.GetKey(KeyCode.F))
        {
            isHold = true;
        }
        else
        {
            weapon_manager.ResetAttack();
            isHold = false;
        }
        if (isHold && CanShoot)
        {
            weapon_manager.Attack();
        }

    }
}
