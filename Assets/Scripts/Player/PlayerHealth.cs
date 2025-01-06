using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float Health;
    private PlayerAnimation playerAnim;
    public GameObject[] BloodFx;
    [HideInInspector]
    private void Awake()
    {
        playerAnim = GetComponent<PlayerAnimation>();
    }
    void Start()
    {
        Health = 100f;
    }

    public void CheckHealth()
    {
        if(Health <= 0 && GameController.instance.PlayerAlive)
        {
            GameController.instance.PlayerAlive = false;
            Health = 0;
            GetComponent<PlayerMove>().enabled = false;
            playerAnim.DeadAnimation();
            AudioManager.instance.PlayerDieSound();
            BloodFx[Random.Range(0, BloodFx.Length)].SetActive(true);
            StartCoroutine("Dead");
        }
        else
        {
            playerAnim.HurtAnimation();
        }
    }

    IEnumerator Dead()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
        GameController.instance.GameOverText.text = "Game Over";
        GameController.instance.GameOver();
    }
}
