using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    public CharacterController getController { get => _controller; set => _controller = GetComponent<CharacterController>(); }
    private CharacterController _controller;
    private Vector3 _playerVel;
    public Vector3 PlayerVel { get; set; } //change to private
    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 1.5f;
    public float crouchTimer = 1f;

    public bool isGrounded;
    private bool isCrouching;
    private bool isSprinting;
    private bool lerpCrouch;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    public void Update()
    {
        isGrounded = _controller.isGrounded;
        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if (isCrouching)
            {
                _controller.height = Mathf.Lerp(_controller.height, 1, p);
            }
            else
            {
                _controller.height = Mathf.Lerp(_controller.height, 2, p);
            }

            if(p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
    }

    //receives input for inputmanager.cs and applies to controller
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDir = Vector3.zero;
        moveDir.x = input.x;
        moveDir.z = input.y;
        _controller.Move(transform.TransformDirection(moveDir) * speed * Time.deltaTime);
        _playerVel.y += gravity * Time.deltaTime;

        if (isGrounded && _playerVel.y < 0)
            _playerVel.y = -2f;
        _controller.Move(_playerVel * Time.deltaTime);
    }

    public void Jump()
    {
        if(isGrounded)
        {
            _playerVel.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void Crouch()
    {
        isCrouching = !isCrouching;
        crouchTimer = 0f;
        lerpCrouch = true;
    }

    public void Sprint()
    {
        isSprinting = !isSprinting;
        if(isSprinting)
        {
            speed = 8f;
        }
        else
        {
            speed = 5f;
        }
    }
}
