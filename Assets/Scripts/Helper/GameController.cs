using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum ZombieGoal
{
    Player,
    Fence
}

public enum GameGoal
{
    KILL_ZOMBIES,
    DEFEND_FENCE,
    TIMER_COUNTDOWN,
    WALK_TO_GOAL_STEPS,
    GAME_OVER
}

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public bool PlayerAlive = true,fenceDestroyed;
    public ZombieGoal zombieGoal = ZombieGoal.Player;
    public GameGoal gameGoal = GameGoal.KILL_ZOMBIES;
    public GameObject PausePanel,GameOverPanel;
    private int ZombieCount=150,Step_Count=250 , initial_Step_Count=250,Timer = 450;
    private Transform Player;
    private Vector3 PlayerPreviousPosition,TempPlayerPos;
    private TextMeshProUGUI Timer_Text, Zombie_Kill_Text, Step_Count_Text;
    public TextMeshProUGUI GameOverText;
    private Image PlayerLife;
    private PlayerHealth playerHealth;

    [HideInInspector]
    public int CoinCount;

    void Awake()
    {
        MakeSingleton();
        playerHealth = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).GetComponent<PlayerHealth>();
    }
    void MakeSingleton()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        if(gameGoal == GameGoal.WALK_TO_GOAL_STEPS)
        {
            Player = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).transform;
            PlayerPreviousPosition = Player.position;
            
            initial_Step_Count = Step_Count;
            Step_Count_Text = GameObject.Find("Steps Count txt").GetComponent<TextMeshProUGUI>();  
            Step_Count_Text.text = Step_Count.ToString();
        }

        if(gameGoal == GameGoal.TIMER_COUNTDOWN || gameGoal == GameGoal.DEFEND_FENCE)
        {
            Timer_Text = GameObject.Find("Timer Counter Txt").GetComponent<TextMeshProUGUI>();
            Timer_Text.text = Timer.ToString();

            InvokeRepeating("Count_Time", 1f, 1f);
        }

        if(gameGoal == GameGoal.KILL_ZOMBIES)
        {
            Zombie_Kill_Text = GameObject.Find("ZombieCount").GetComponent<TextMeshProUGUI>();
            Zombie_Kill_Text.text = ZombieCount.ToString();
        }
        PlayerLife = GameObject.Find("Life full").GetComponent<Image>();
    }

    private void Update()
    {
        if(gameGoal == GameGoal.WALK_TO_GOAL_STEPS)
        {            
            Checksteps();
        }      
    }
    public void PauseGame()
    {
        AudioManager.instance.PauseGamePlaySound();
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        AudioManager.instance.UnPauseGamePlaySound();
        Time.timeScale = 1f;
        PausePanel.SetActive(false);
    }

    public void QuitGame()
    {
        AudioManager.instance.StopGamePlaySound();
        AudioManager.instance.MainMenuSound();
        Time.timeScale = 1f;
        ShootControl.CreatingBullet = 0;
        ShootControl.CreatingRocket = 0;
        SceneManager.LoadScene(TagManager.MainMenu);
    }

    public void GameOver()
    {
        AudioManager.instance.StopGamePlaySound();
        GameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    void Count_Time()
    {
        Timer--;
        Timer_Text.text = Timer.ToString();
        if(Timer <= 0)
        {
            CancelInvoke("Count_Time");
            GameOver();
        }
    }

    public void Zombie_Count()
    {
        ZombieCount--;
        Zombie_Kill_Text.text = ZombieCount.ToString();

        if (ZombieCount <= 0)
        {
            GameOver();
        }
    }

    public void PlayerLifeCount(float damage)
    {
        damage = (playerHealth.Health - damage)/100f;
        PlayerLife.fillAmount = damage;
    }

    public void Checksteps()
    {
        Vector3 PlayerCurrentPos = Player.position;
        float distance = Vector3.Distance(new Vector3(PlayerCurrentPos.x,0f,0f), new Vector3(PlayerPreviousPosition.x,0f,0f));
        if (PlayerCurrentPos.x > PlayerPreviousPosition.x)
        {
            if (distance > 1f)
            {
                PlayerPreviousPosition = PlayerCurrentPos;
                Step_Count--;
                if(Step_Count <= 0)
                {
                    GameOver();
                }
            }
        }
        else if (PlayerCurrentPos.x < PlayerPreviousPosition.x)
        {
            if (distance > 1f)
            {
                Step_Count++;
                PlayerPreviousPosition = PlayerCurrentPos;
                if(Step_Count >= initial_Step_Count)
                {
                    Step_Count = initial_Step_Count;
                }
            }

        }
        Step_Count_Text.text = Step_Count.ToString();
    }
}
