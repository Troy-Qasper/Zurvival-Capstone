using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isWalkingBackwardsHash;
    int isStrafingLeftHash;
    int isStrafingRightHash;
    int isPressingADSHash;
    int isPressingFireHash;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isWalkingBackwardsHash = Animator.StringToHash("isWalkingBackwards");
        isStrafingLeftHash = Animator.StringToHash("isStrafingLeft");
        isStrafingRightHash = Animator.StringToHash("isStrafingRight");
        isPressingADSHash = Animator.StringToHash("isADSFiring");
        isPressingFireHash = Animator.StringToHash("isADSIdle");
    }

    // Update is called once per frame
    void Update()
    {

        bool forwardPressed = Input.GetKey("w");
        bool backwardPressed = Input.GetKey("s");
        bool leftStrafePressed = Input.GetKey("a");
        bool rightStrafePressed = Input.GetKey("d");
        bool sprintPressed = Input.GetKey("left shift");

        bool pressingADS = Input.GetKey(KeyCode.Mouse1);
        bool pressingFire = Input.GetKey(KeyCode.Mouse0);

        bool isWalking = animator.GetBool(isWalkingHash);
        bool isWalkingBackwards = animator.GetBool(isWalkingBackwardsHash);
        bool isStrafingLeft = animator.GetBool(isStrafingLeftHash);
        bool isStrafingRight = animator.GetBool(isStrafingRightHash);
        bool isMoving = isWalking || isWalkingBackwards || isStrafingLeft || isStrafingRight;

        bool isADSIdle = animator.GetBool(isPressingADSHash);
        bool isADSFiring = animator.GetBool(isPressingFireHash);

        if (!isWalking && forwardPressed)
        {
            animator.SetBool(isWalkingHash, true);
        }
        if (isWalking && !forwardPressed)
        {
            animator.SetBool(isWalkingHash, false);
        }

        if (!isWalkingBackwards && backwardPressed)
        {
            animator.SetBool(isWalkingBackwardsHash, true);
        }
        if (isWalkingBackwards && !backwardPressed)
        {
            animator.SetBool(isWalkingBackwardsHash, false);
        }

        if (!isStrafingLeft && leftStrafePressed)
        {
            animator.SetBool(isWalkingHash, true);
            //animator.SetBool(isStrafingLeftHash, true);
        }
        if (isStrafingLeft && !leftStrafePressed)
        {
            animator.SetBool(isWalkingHash, false);
            //animator.SetBool(isStrafingLeftHash, false);
        }

        if (!isStrafingRight && rightStrafePressed)
        {
            animator.SetBool(isWalkingHash, true);
            //animator.SetBool(isStrafingRightHash, true);
        }
        if (isStrafingRight && !rightStrafePressed)
        {
            animator.SetBool(isWalkingHash, false);
            //animator.SetBool(isStrafingRightHash, false);
        }

        if (Input.GetMouseButtonDown(1))
        {
            animator.SetBool("isADSIdle", true);

            if (Input.GetMouseButtonDown(0))
            {
                animator.SetBool("isADSFiring", false);
            }
            if (Input.GetMouseButtonUp(0))
            {
                animator.SetBool("isADSFiring", true);
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            animator.SetBool("isADSIdle", false);
        }

        if (sprintPressed)
        {
            animator.SetBool("isSprinting", true);
        }
        else
        {
            animator.SetBool("isSprinting", false);
        }
    }
}
