using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed;
    private WaitForSeconds deactivate = new WaitForSeconds(4f);
    public GameObject rocket_explosion;
    public GameObject tempExplosion;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = 20f;
    }

    private void Start()
    {
        if (this.tag == TagManager.ROCKET_MISSILE_TAG)
        {
            speed = 10f;
        }
    }

    private void OnEnable()
    {        
        StartCoroutine(Deactivate());
    }

    private void FixedUpdate()
    {
        MoveBullets();
    }

    void MoveBullets()
    {

        if (transform.root.localScale.x > 0f)
        {
            rb.velocity = new Vector2(-speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(speed, 0);
        }
    }

    IEnumerator Deactivate()
    {
        yield return deactivate;
        gameObject.SetActive(false);
    }

    public void ExplosionFX()
    {
        AudioManager.instance.FenceExplosionSound();
        tempExplosion = Instantiate(rocket_explosion,transform.position, Quaternion.identity);  
        Invoke("DeactivateExplosionFX",.5f);
    }
    void DeactivateExplosionFX()
    {
        tempExplosion.SetActive(false);
    }
}
