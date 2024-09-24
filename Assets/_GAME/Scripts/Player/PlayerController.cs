using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private MobileJoystick mobileJoystick;
    public CharacterController characterController;
    [SerializeField] private PlayerAnimation playerAnimation;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float gravity = -9.81f; // Gravity value
    [SerializeField] private float jumpHeight = 1.5f; // Jump height
    private Vector3 velocity; // To store gravity and vertical movement

    private void Update()
    {
        ManageMovement();
    }

    void ManageMovement()
    {
        // Get movement vector from the joystick
        Vector3 moveVector = mobileJoystick.GetMoveVector();

        // Map axes if needed
        moveVector.z = moveVector.y;
        moveVector.y = 0; // Ensure y movement is not controlled by the joystick

        // Apply the move speed
        moveVector *= moveSpeed;

        // Apply the movement to the character controller with deltaTime
        characterController.Move(moveVector * Time.deltaTime);

        // Check if the player is grounded
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Slight negative value to keep the player grounded
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;

        // Apply the gravity-induced velocity to the character controller
        characterController.Move(velocity * Time.deltaTime);

        // Manage animations based on movement
        playerAnimation.ManageAnimations(moveVector);
    }

    public void Jump()
    {
        if (characterController.isGrounded)
        {
            // Calculate the velocity required to reach the desired jump height
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}



