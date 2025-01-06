using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToPlayer : MonoBehaviour
{
    private float radius = 1f;
    public LayerMask PlayerLayer;
    private float damage = 1f;

    
    void FixedUpdate()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position,radius,PlayerLayer);
        if(hit != null)
        {
            if (hit.gameObject.tag == TagManager.PLAYER_TAG || hit.gameObject.tag == TagManager.PLAYER_HEALTH_TAG)
            {
                hit.gameObject.GetComponentInParent<PlayerHealth>().Health -= damage;
                GameController.instance.PlayerLifeCount(damage);
                hit.gameObject.GetComponentInParent<PlayerHealth>().CheckHealth();
            }
            else
            {
                hit.gameObject.GetComponent<fenceHealth>().Health -= damage;
                hit.gameObject.GetComponent<fenceHealth>().CheckHealth();
            }
        }

    }
}
