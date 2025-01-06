using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float Move_Speed;

    private void Awake()
    {
        Move_Speed = .5f;
    }

    public void Move(Transform target)
    {
        ChangeDirection(target);
        transform.position = Vector3.MoveTowards(transform.position,target.position, Move_Speed * Time.smoothDeltaTime);
        
    }

    void ChangeDirection(Transform target)
    {
        Vector3 tempscale = transform.localScale;
        if (target.position.x > transform.position.x) 
        {
            tempscale.x = -1f;
        }
        else
        {
            tempscale.x = 1f;
        }
        transform.localScale = tempscale;
    }
}
