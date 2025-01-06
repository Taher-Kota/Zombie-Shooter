using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArmController : MonoBehaviour
{
    public Sprite One_Hand_Sprite, Two_Hand_Sprite;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    
    public void ChangeToOneHand()
    {
        sr.sprite = One_Hand_Sprite;
    }

    public void ChangeToTwoHand()
    {                
       sr.sprite = Two_Hand_Sprite;        
    }
}
