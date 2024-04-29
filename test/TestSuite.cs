using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class TestSuite
{
    [UnityTest]
    public IEnumerator MainMenu_Start_SetsUIRoundValueCursorLockState()
    {
        SceneManager.LoadScene("MainMenu");
        yield return new WaitForSeconds(1f);

        GameObject mainMenuObject = GameObject.Find("MainMenu");
        MainMenu mainMenu = mainMenuObject.GetComponent<MainMenu>();

        Assert.NotNull(mainMenu.highRoundUI);

        // Simulate the call to LoadHighestRounds() and LoadHighScore() to test UI values
        SaveLoadManager.Instance.SaveHighestRound(10);


        //mainMenu.PlayGame();

        Assert.AreEqual("Highest Round Achieved: 10", mainMenu.highRoundUI.text);


        Assert.AreEqual(CursorLockMode.None, Cursor.lockState);
    }
    [UnityTest]
    public IEnumerator MainMenu_Start_SetsUIScoreValueCursorLockState()
    {
        SceneManager.LoadScene("MainMenu");
        yield return new WaitForSeconds(1f);

        GameObject mainMenuObject = GameObject.Find("MainMenu");
        MainMenu mainMenu = mainMenuObject.GetComponent<MainMenu>();

        Assert.NotNull(mainMenu.highScoreUI);


        // Simulate the call to LoadHighestRounds() and LoadHighScore() to test UI values
        SaveLoadManager.Instance.SaveHighestScore(1000);


        //mainMenu.PlayGame();

        Assert.AreEqual("Highest Score Achieved: 1000", mainMenu.highScoreUI.text);


        Assert.AreEqual(CursorLockMode.None, Cursor.lockState);
    }
    /*[UnityTest]
    public IEnumerator MainMenu_PlayGame_LoadsScenes()
    {
        SceneManager.LoadScene("MainMenu");
        yield return new WaitForSeconds(1f);

        GameObject mainMenuObject = GameObject.Find("MainMenu");
        MainMenu mainMenu = mainMenuObject.GetComponent<MainMenu>();

        mainMenu.PlayGame();

        yield return new WaitForSeconds(1); // Wait for scenes to load

        Scene firstScene = SceneManager.GetSceneByName("Level 0");

        Assert.IsTrue(firstScene.isLoaded);
        GameObject.Destroy(mainMenuObject);
    }*/
}

