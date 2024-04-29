using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using NUnit.Framework.Constraints;
using System;
using static UnityEngine.InputSystem.DefaultInputActions;

public class PlayerMotorTests : InputTestFixture
{
    //private PlayerMotor playerMotor;
    //private GameObject playerGameObject;

    private GameObject character = Resources.Load<GameObject>("Player");
    Keyboard keyboard;
    Mouse mouse;
    CharacterController controller;

    [SetUp]
    public override void Setup()
    {
        SceneManager.LoadScene("Scenes/TestingScene");
        controller = new CharacterController();
        base.Setup();
        keyboard = InputSystem.AddDevice<Keyboard>();

        mouse = InputSystem.AddDevice<Mouse>();
        Press(mouse.rightButton);
        Release(mouse.rightButton);
    }

    [TearDown]
    public void Teardown()
    {
        // Clean up by destroying the GameObject after each test
        //Object.Destroy(character);
    }

    [Test]
    public void TestPlayerInstantiation()
    {
        GameObject characterInstance = GameObject.Instantiate(character, Vector3.zero, Quaternion.identity);
        Assert.That(characterInstance, !Is.Null);
    }

    [UnityTest]
    public IEnumerator ProcessMove_InputMovesPlayer()
    {
        GameObject characterInstance = GameObject.Instantiate(character, Vector3.zero, Quaternion.identity);
        var playerMotor = characterInstance.GetComponent<PlayerMotor>();
        playerMotor.getController = characterInstance.AddComponent<CharacterController>();

        //characterInstance.gameObject.AddComponent<CharacterController>();
        var speed = characterInstance.GetComponent<PlayerMotor>().speed;
        Debug.Log("preparing to move");

        characterInstance.GetComponent<PlayerMotor>().ProcessMove(new Vector2(1f, 1f) * speed);
        Debug.Log("moving");
        yield return new WaitForSeconds(3f);
        // Check if the player has moved in the correct direction

        Assert.That(characterInstance.transform.GetChild(0).transform.position.z, Is.GreaterThan(1f));
    }

    [UnityTest]
    public IEnumerator TestPlayerJump()
    {
        GameObject characterInstance = GameObject.Instantiate(character, Vector3.zero, Quaternion.identity);
        Press(keyboard.spaceKey);
        yield return new WaitForSeconds(1f);
        Assert.That(characterInstance.transform.GetChild(0).transform.position.y, Is.GreaterThan(0f));
    }


    // Add more tests as needed for other methods and functionalities
}

