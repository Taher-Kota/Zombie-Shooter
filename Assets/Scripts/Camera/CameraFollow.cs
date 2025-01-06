using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform Player;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).transform;
    }

    void LateUpdate()
    {
        if(GameController.instance.gameGoal != GameGoal.DEFEND_FENCE && GameController.instance.gameGoal != GameGoal.GAME_OVER)
        {
            if (Player)
            {
                Vector3 tempPos = transform.position;
                tempPos.x = Player.position.x;
                float value = Mathf.Clamp(tempPos.x, -14.4f, 155f);
                tempPos.x = value;
                transform.position = tempPos;
            }
        }
    }
}
