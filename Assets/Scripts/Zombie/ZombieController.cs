using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public static ZombieController instance;
    private WeaponManager weaponManager;
    private Movement move;
    private ZombieAnimation anim;
    public float Health;

    private Transform target;

    private bool Zombie_Alive,CanAttack=false,gettingAttack;

    public GameObject[] FxDead;

    private WaitForSeconds zombiedead = new WaitForSeconds(2f);

    public GameObject damagePoint;

    public GameObject HealthCollider;
    public GameObject Coins;
    public float timerAttack;
    private void Awake()
    {
        instance = this;
        move = GetComponent<Movement>();
        anim = GetComponent<ZombieAnimation>();        
        Health = 15f;
        Zombie_Alive = true;
        weaponManager = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).GetComponent<WeaponManager>();
    }
    private void Start()
    {
        if (GameController.instance.zombieGoal == ZombieGoal.Player)
        {
            target = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).transform;
        }
        else
        {
            GameObject[] fences = GameObject.FindGameObjectsWithTag(TagManager.FENCE_TAG);
            target = fences[Random.Range(0, fences.Length)].gameObject.transform;
        }
    }
    void Update()
    {        
        if (Zombie_Alive && GameController.instance.PlayerAlive)
        {
            CheckDistance();
        }
        else
        {
            anim.Idle();
        }
    }

    void CheckDistance()
    {
        if(Vector3.Distance(target.position, transform.position) > 1.65f && !gettingAttack)
        {
            move.Move(target);         
        }
        else
        {    
            if (CanAttack && GameController.instance.PlayerAlive) {
                anim.Attack();
                timerAttack += Time.deltaTime;
                if (timerAttack > .65f)
                {
                    timerAttack = 0f;
                    AudioManager.instance.ZombieAttack();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == TagManager.PLAYER_HEALTH_TAG || target.tag == TagManager.PLAYER_TAG || target.tag == TagManager.FENCE_TAG)
        {
            CanAttack = true;                      
        }

        if(target.tag == TagManager.BULLET_TAG || target.tag == TagManager.ROCKET_MISSILE_TAG)
        {
            gettingAttack = true;
            anim.Hurt();
            Health -= weaponManager.Current_Weapon.GunConfig.Damage;          
            CheckHealth(false);
            if (target.tag == TagManager.ROCKET_MISSILE_TAG)
            {
                target.GetComponent<BulletController>().ExplosionFX();
            }
           target.gameObject.SetActive(false);
        }

        if(target.tag == TagManager.Flamer_TAG)
        {
            gettingAttack = true;
            anim.Hurt();
            Health -= weaponManager.Current_Weapon.GunConfig.Damage;           
            CheckHealth(false);
        }
        
    }

    private void OnTriggerExit2D(Collider2D target)
    {
        if (target.tag == TagManager.PLAYER_HEALTH_TAG || target.tag == TagManager.PLAYER_TAG || target.tag == TagManager.FENCE_TAG)
        {
            CanAttack = false;
        }
    }

    public void SetValue()
    {
        gettingAttack = false;
    }

    public void PlayerTransform()
    {
        target = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).transform;
    }
    public void DeadFX(int index)
    {
        FxDead[index].SetActive(true);
        if (FxDead[index].GetComponent<ParticleSystem>())
        {
            FxDead[index].GetComponent<ParticleSystem>().Play();
        }
    }

    IEnumerator DeactivateZombie()
    {
        int rand = Random.Range(0, 30);
        AudioManager.instance.ZombieDieSound();
        yield return zombiedead;
        if (GameController.instance.gameGoal == GameGoal.KILL_ZOMBIES)
        {
            GameController.instance.Zombie_Count();
        }
        if(GameController.instance.gameGoal != GameGoal.DEFEND_FENCE)
        {
            Instantiate(Coins, transform.position,Quaternion.identity);
        }
        if(rand == 10 || rand == 6 || rand == 27)
        {
            SmartPool.instance.SpawnAmmo(transform.position);
        }
            gameObject.SetActive(false);
    }

    public void CheckHealth(bool meelee)
    {
        if (Health <= 0 && !meelee)
        {           
            Health = 0;
            Zombie_Alive = false;
            HealthCollider.GetComponent<BoxCollider2D>().enabled = false;
            anim.Dead();
            StartCoroutine("DeactivateZombie");
        }
        else if(Health <= 0 && meelee)
        {
            Health = 0;
            Zombie_Alive = false;
            HealthCollider.GetComponent<BoxCollider2D>().enabled = false;
            anim.MeeleeDead();
            StartCoroutine("DeactivateZombie");
        }
    }

    public void ActivateDamagePoint()
    {
        damagePoint.SetActive(true);
    }

    public void DeActivateDamagePoint()
    {
        damagePoint.SetActive(false);
    }

}
