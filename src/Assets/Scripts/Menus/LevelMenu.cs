using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;
    public GameObject levelButtons;



    //change, private, awake
    public void Start()
    {
        ButtonsToArray();

        //unlocks level buttons accessibility by determining if level is unlocked
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for(int i = 0; i < unlockedLevel; i++)
        {
            buttons[i].interactable = true;
        }
    }

    //Loads scene by name in build settings and stops main menu music when a level is selected
    public void OpenLevel(string levelId)
    {
        string levelName = levelId;
        SceneManager.LoadScene(levelName);
        SoundManager.Instance.musicSource.Stop();
        //SoundManager.Instance.inGameMusicChannel.Play();
    }
    public void ButtonsToArray()
    {
        int childCount = levelButtons.transform.childCount;
        //Debug.Log($"Child count: {childCount}");
        buttons = new Button[childCount];
        for (int i = 0; i < childCount; i++)
        {
            buttons[i] = levelButtons.transform.GetChild(i).gameObject.GetComponent<Button>();
            //Debug.Log($"Button {i}: {buttons[i].name}");
        }
    }
}
