using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class SmartPool : MonoBehaviour
{
    public static SmartPool instance;
    private List<GameObject> Bullets = new List<GameObject>();
    private List<GameObject> Rockets = new List<GameObject>();
    private List<GameObject> FallFX = new List<GameObject>();
    public GameObject[] zombies;
    public Transform ZombiesSpawnPoint;
    private WeaponManager weaponManager;
    private float Y_min = -0.3f, Y_max = -3.7f;
    private List<GameObject> AmmoSpawner = new List<GameObject>();

    private void Awake()
    {
        MakingInstance();
    }
    void Start()
    {
        InvokeRepeating("SpawnZombies", 2f, Random.Range(1f, 2f));
    }

    public void CreateBulletsAndBulletFall(GameObject Bullet, GameObject Fallfx, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject temp_bullet = Instantiate(Bullet);
            GameObject temp_Fallfx = Instantiate(Fallfx);

            Bullets.Add(temp_bullet);
            FallFX.Add(temp_Fallfx);

            Bullets[i].SetActive(false);
            FallFX[i].SetActive(false);
        }
    }

    public void CreateRockets(GameObject Rocket, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject temp_rocket = Instantiate(Rocket);

            Rockets.Add(temp_rocket);

            Rockets[i].SetActive(false);
        }
    }
    
    public GameObject SpawnBulletFallfx(Vector3 position , Quaternion rotation)
    {
        for (int i = 0; i < FallFX.Count; i++)
        {
            if (!FallFX[i].activeInHierarchy)
            {
                FallFX[i].SetActive(true);
                FallFX[i].transform.position = position;
                FallFX[i].transform.rotation = rotation;
                return FallFX[i];
            }
        }
        return null;
    }

    public void SpawnBullet(Vector3 position , Vector3 direction , Quaternion rotation , WeaponName weapon)
    {
        if (weapon != WeaponName.ROCKET)
        {
            for (int i = 0; i < Bullets.Count; i++)
            {
                if (!Bullets[i].activeInHierarchy)
                {
                    Bullets[i].SetActive(true);
                    Bullets[i].transform.position = position;
                    Bullets[i].transform.rotation = rotation;
                    Bullets[i].transform.localScale = direction;
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < Rockets.Count; i++)
            {
                if (!Rockets[i].activeInHierarchy)
                {
                    Rockets[i].SetActive(true);
                    Rockets[i].transform.position = position;
                    Rockets[i].transform.rotation = rotation;
                    Rockets[i].transform.localScale = direction;
                    break;
                }
            }
        }
    }

    private void OnDisable()
    {
        instance = null;
    }
    void MakingInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void SpawnZombies()
    {
        if(GameController.instance.gameGoal == GameGoal.DEFEND_FENCE)
        {
            float posX = ZombiesSpawnPoint.position.x;
            posX += 15f;
            float posY = Random.Range(Y_min, Y_max);
            Instantiate(zombies[Random.Range(0, zombies.Length)], new Vector3(posX, posY, 0f),Quaternion.identity);
        }
        
        if(GameController.instance.gameGoal == GameGoal.KILL_ZOMBIES || GameController.instance.gameGoal == GameGoal.TIMER_COUNTDOWN
            || GameController.instance.gameGoal == GameGoal.WALK_TO_GOAL_STEPS)
        {
            float posX = ZombiesSpawnPoint.position.x;
            if (Random.Range(0, 2) > 0)
            {
                posX += Random.Range(5f, 10f);
            }
            else
            {
                posX -= Random.Range(5f, 10f);
            }
            float posY = Random.Range(Y_min, Y_max);
            Instantiate(zombies[Random.Range(0, zombies.Length)], new Vector3(posX, posY, 0f),Quaternion.identity);
        }

        if(GameController.instance.gameGoal == GameGoal.GAME_OVER)
        {
            CancelInvoke("SpawnZombies");
        }
    }
    public void CreateAmmo(GameObject Ammo,int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject tempAmmo = Instantiate(Ammo);
            AmmoSpawner.Add(tempAmmo);
            AmmoSpawner[i].SetActive(false);
        }
    }
    public void SpawnAmmo(Vector3 position)
    {
        for (int i = 0; i < AmmoSpawner.Count; i++) 
        {
            if (!AmmoSpawner[i].activeInHierarchy)
            {
                AmmoSpawner[i].SetActive(true);
                AmmoSpawner[i].transform.position = position;
                break;
            }
        }
    }
}
