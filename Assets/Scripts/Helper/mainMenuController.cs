using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuController : MonoBehaviour
{
    public GameObject CharacterSelectPanel;
    public GameObject MenuPanel;
    public GameObject PlayBtn;
    public GameObject title;

    public void StartMissionOne()
    {
        SceneManager.LoadScene(TagManager.Level_1);
    }
    public void StartMissionTwo()
    {
        SceneManager.LoadScene(TagManager.Level_2);
    }
    public void StartMissionThree()
    {
        SceneManager.LoadScene(TagManager.Level_3);
    }
    public void StartMissionFour()
    {
        SceneManager.LoadScene(TagManager.Level_4);
    }

    public void OpenCharacterPanel()
    {
        CharacterSelectPanel.SetActive(true);
        MenuPanel.SetActive(false);
    }
    public void CloseCharacterPanel()
    {
        CharacterSelectPanel.SetActive(false);
        MenuPanel.SetActive(true);
    }

    public void CharacterTommySelect()
    {
        GameManager.instance.Character_Index = 0;
    }
    public void CharacterMarrySelect()
    {
        GameManager.instance.Character_Index = 1;
    }

    public void PlayButton()
    {
        PlayBtn.SetActive(false);
        title.SetActive(false);
        MenuPanel.SetActive(true);
    }
}
