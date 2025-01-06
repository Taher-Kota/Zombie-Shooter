using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootControl : WeaponControl
{    
    public Transform SpawnBullet;
    public GameObject BulletPrefab;
    public ParticleSystem FX_Shot;
    public GameObject Fx_BulletFall;

    private BoxCollider2D fire_Collider;
    private WaitForSeconds wait_time = new WaitForSeconds(.01f);
    //private WaitForSeconds fireColliderWait = new WaitForSeconds(.02f);

    public static int CreatingBullet = 0;
    public static int CreatingRocket = 0;

    public GameObject Ammo;

    private void Start()
    {
        if (weapon != WeaponName.ROCKET && weapon != WeaponName.FLAMER)
        {
            if (CreatingBullet == 0)
            {
                SmartPool.instance.CreateAmmo(Ammo, 10);
                SmartPool.instance.CreateBulletsAndBulletFall(BulletPrefab, Fx_BulletFall, 100);
                CreatingBullet = 1;
            }
        }
        else if (weapon == WeaponName.ROCKET)
        {
            if (CreatingRocket == 0)
            {
                SmartPool.instance.CreateRockets(BulletPrefab, 100);
                CreatingRocket = 1;
            }
        }
    }
    public override void ProcessAttack()
    {
        //base.ProcessAttack();
        switch (weapon)
        {
            case WeaponName.PISTOL:
                AudioManager.instance.GunSound(0);
                break;
            case WeaponName.MP5:
                AudioManager.instance.GunSound(1);
                break;
            case WeaponName.M3:
                AudioManager.instance.GunSound(2);
                break;
            case WeaponName.AK47:
                AudioManager.instance.GunSound(3);
                break;
            case WeaponName.AWP:
                AudioManager.instance.GunSound(4);
                break;
            case WeaponName.FLAMER:
                AudioManager.instance.GunSound(5);
                break;
            case WeaponName.ROCKET:
                AudioManager.instance.GunSound(6);
                break;
        }

        if (transform != null && weapon != WeaponName.FLAMER)
        {
            if (weapon != WeaponName.ROCKET)
            {    
                GameObject BulletFallFx = SmartPool.instance.SpawnBulletFallfx(SpawnBullet.position,Quaternion.identity);
                //BulletFallFx.transform.localScale = transform.root.eulerAngles.y > 1f ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
                StartCoroutine(waitForShoot());
            }
            SmartPool.instance.SpawnBullet(SpawnBullet.position,new Vector3((transform.root.localScale.x > 0 ? 1f : -1f),1f,1f),SpawnBullet.rotation,weapon);
        }
        else if(weapon == WeaponName.FLAMER)
        {
            fire_Collider = SpawnBullet.GetComponent<BoxCollider2D>();
            StartCoroutine(ActiveFireCollider());
        }
    }

    IEnumerator waitForShoot()
    {
        yield return wait_time;
        FX_Shot.Play();
    }

    IEnumerator ActiveFireCollider()
    {
        fire_Collider.enabled = true;
        FX_Shot.Play();
        yield return fire_Collider;
        fire_Collider.enabled = false;
    }

}
