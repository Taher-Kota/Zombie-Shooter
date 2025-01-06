using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoController : MonoBehaviour
{
    private WeaponManager weaponManager;

    void Start()
    {
        weaponManager = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).GetComponent<WeaponManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == TagManager.PLAYER_HEALTH_TAG || collision.tag == TagManager.PLAYER_TAG)
        {
            gameObject.SetActive(false);
            weaponManager.Current_Weapon.CurrentBullets += 50;
        }
    }

}
