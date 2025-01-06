using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject[] Character;
    [HideInInspector]
    public int Character_Index;
    private Transform Tommy;

    void Awake()
    {
        MakeSingleton();
    }
    void Start()
    {
        Character_Index = 0;
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }
    void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void OnSceneLoad(Scene scene, LoadSceneMode SceneMode)
    {
        if (scene.name != TagManager.MainMenu)
        {
            AudioManager.instance.StopMainMenuSound();
            AudioManager.instance.GamePlaySound();

            if (Character_Index != 0)
            {
                Tommy = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).transform;
                Instantiate(Character[Character_Index], Tommy.position, Quaternion.identity);
                Tommy.gameObject.SetActive(false);
            }
        }
    }
}
