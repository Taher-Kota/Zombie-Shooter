using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeeleeDamage : MonoBehaviour
{
    private int damage;
    private float radius = .3f;
    public LayerMask ZombieLayer;
    void Start()
    {
        damage = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).GetComponent<WeaponManager>().Current_Weapon.GunConfig.Damage; 
    }
    void Update()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, ZombieLayer);
        if (hit != null)
        {
            if (hit.gameObject.tag == TagManager.ZOMBIE_HEALTH_TAG)
            {
                hit.gameObject.GetComponentInParent<ZombieController>().Health -= damage;
                hit.gameObject.GetComponentInParent<ZombieAnimation>().Hurt();
                hit.gameObject.GetComponentInParent<ZombieController>().CheckHealth(true);
            }
        }
    }
}
