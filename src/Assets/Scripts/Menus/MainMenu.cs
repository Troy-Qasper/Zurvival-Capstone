using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TMP_Text highRoundUI;
    public TMP_Text highScoreUI;

    //public AudioSource main_channel;

    //change
    public void Start()
    {
        SoundManager.Instance.PlayMenuMusic(SoundManager.Instance.menuMusic);

        int highestRound = SaveLoadManager.Instance.LoadHighestRounds();
        int highestScore = SaveLoadManager.Instance.LoadHighestScore();

        highRoundUI.text = $"Highest Round Achieved: {highestRound}";
        highScoreUI.text = $"Highest Score Achieved: {highestScore}";

        Cursor.lockState = CursorLockMode.None;
    }

    public void PlayGame()
    {
        //main_channel.Stop();

        SceneManager.LoadScene("Level 0");
        SceneManager.LoadScene("Level 1");
        SceneManager.LoadScene("Level 2");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
     Application.Quit();
#endif

    }

    public void OnContinueGameClicked()
    {
        Debug.Log("loaded game");
    }

    public void FullScreenOnOption()
    {
        Screen.fullScreen = true;
    }
    public void FullScreenOffOption()
    {
        Screen.fullScreen = false;
    }
    
}
