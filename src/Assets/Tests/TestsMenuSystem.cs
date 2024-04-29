using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuTests
{
    [Test]
    public void MainMenu_PlayGame_LoadsScenes()
    {
        // Arrange
        var mainMenu = new GameObject().AddComponent<MainMenu>();

        // Act
        mainMenu.PlayGame();

        // Assert
        Assert.IsTrue(SceneManager.sceneCount > 1);
    }
}

public class PauseMenuTests
{
    [Test]
    public void PauseMenu_PauseGame_SetsTimeScaleAndCursor()
    {
        // Arrange
        var pauseMenu = new GameObject().AddComponent<PauseMenu>();
        pauseMenu.pauseMenu = new GameObject();

        // Act
        pauseMenu.PauseGame();

        // Assert
        Assert.AreEqual(0f, Time.timeScale);
        Assert.IsTrue(PauseMenu.isPaused);
        Assert.AreEqual(CursorLockMode.None, Cursor.lockState);
    }

    [Test]
    public void PauseMenu_ResumeGame_SetsTimeScaleAndCursor()
    {
        // Arrange
        var pauseMenu = new GameObject().AddComponent<PauseMenu>();
        pauseMenu.pauseMenu = new GameObject();

        // Act
        pauseMenu.ResumeGame();

        // Assert
        Assert.AreEqual(1f, Time.timeScale);
        Assert.IsFalse(PauseMenu.isPaused);
        Assert.AreEqual(CursorLockMode.Locked, Cursor.lockState);
    }
}