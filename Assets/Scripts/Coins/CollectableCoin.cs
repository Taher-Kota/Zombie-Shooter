using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCoin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == TagManager.PLAYER_TAG)
        {            
            gameObject.SetActive(false);
            AudioManager.instance.CoinCollectedSound();
            GameController.instance.CoinCount++;
        }
    }
}
