using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fenceHealth : MonoBehaviour
{
    public float Health;
    public ParticleSystem WoodenBreak, WoodenExplosion;
 
    void Start()
    {
        Health = 10f;
    }

    public void CheckHealth()
    {
        if (Health <= 0 && !GameController.instance.fenceDestroyed)
        {
            GameController.instance.fenceDestroyed = true;
            WoodenExplosion.Play();
            AudioManager.instance.FenceExplosionSound();
            Health = 0;
            GameController.instance.zombieGoal = ZombieGoal.Player;
            ZombieController.instance.PlayerTransform();
            StartCoroutine("Dead");
        }
        else
        {
         WoodenBreak.Play();
        }
    }

    IEnumerator Dead()
    {
        yield return new WaitForSeconds(.2f);
        gameObject.SetActive(false);
        GameController.instance.GameOverText.text = "Game Over";
        GameController.instance.GameOver();
    }
}
