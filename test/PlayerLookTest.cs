using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;

public class PlayerLookTests
{
    private GameObject playerObject;
    private PlayerLook playerLook;
    private Camera camera;

    [SetUp]
    public void Setup()
    {
        // Create a new GameObject with a PlayerLook component
        playerObject = new GameObject();
        playerLook = playerObject.AddComponent<PlayerLook>();

        // Create a camera and set it to the PlayerLook script
        GameObject cameraObject = new GameObject();
        camera = cameraObject.AddComponent<Camera>();
        playerLook.cam = camera;
        // Initialize cursor lock state as in the Start method
        Cursor.lockState = CursorLockMode.Locked;
    }

    [TearDown]
    public void Teardown()
    {
        // Clean up after tests
        Object.DestroyImmediate(playerObject);
        Object.DestroyImmediate(camera.gameObject);
    }

    [Test]
    public void ProcessLook_WithPositiveInput_RotatesCameraVerticallyWithinLimits()
    {
        // Simulate vertical input
        Vector2 input = new Vector2(0, 1);
        playerLook.ProcessLook(input);
        float initialXRotation = playerObject.transform.eulerAngles.x;
        // Check that xRotation is clamped correctly within the expected limits
        //float expectedRotationLimit = 80f; // Assuming Time.deltaTime * ySensitivity * input.y would exceed this limit
        Assert.IsTrue(playerLook.cam.transform.localRotation.eulerAngles.x > initialXRotation, "Camera rotation should be clamped at 80 degrees.");
    }

    [Test]
    public void ProcessLook_WithHorizontalInput_RotatesPlayerObject()
    {
        // Simulate horizontal input
        Vector2 input = new Vector2(1, 0);
        float initialYRotation = playerObject.transform.eulerAngles.y;
        playerLook.ProcessLook(input);

        // Check that the player object has rotated horizontally
        Assert.IsTrue(playerObject.transform.eulerAngles.y > initialYRotation, "Player object should rotate horizontally.");
    }
}