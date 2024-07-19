using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private MobileJoystick mobileJoystick;
    public CharacterController characterController;
    [SerializeField] PlayerAnimation playerAnimation;
    [SerializeField] private float moveSpeed;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.right;
    }
    private void Update()
    {
        ManageMovement();

    }
    void ManageMovement()
    {
        Vector3 moveVector = mobileJoystick.GetMoveVector() * moveSpeed * Time.deltaTime / Screen.width;
        moveVector.z = moveVector.y;
        moveVector.y = 0;

        characterController.Move(moveVector);
        playerAnimation.ManageAnimations(moveVector);

    }
}
