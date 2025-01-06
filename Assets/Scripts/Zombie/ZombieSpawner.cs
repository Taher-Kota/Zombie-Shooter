using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject ZombiePrefab;
    public Transform SpawnPoint;
    private Transform Player;
    public GameObject FX_ShredFall;
    [HideInInspector]
    public GameObject zombie;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).transform;
        if (Random.value > .5) { AudioManager.instance.ZombieRiseSound(); }
        FX_ShredFall.SetActive(true);
        zombie = Instantiate(ZombiePrefab,SpawnPoint.position, Quaternion.identity);
        StartCoroutine("DeactivateSpawner");
    }

    IEnumerator DeactivateSpawner()
    {
        yield return new WaitForSeconds(Random.Range(2f, 4f));
        gameObject.SetActive(false);
    }
}
