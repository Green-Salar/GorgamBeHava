using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Joystick movementJoystick;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = 9.81f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isJumping = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {

        bool isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Get input from joystick
        float horizontalInput = movementJoystick.Horizontal;
        float verticalInput = movementJoystick.Vertical;

        // Calculate move direction
        Vector3 move = transform.right * horizontalInput + transform.forward * verticalInput;
        controller.Move(move * moveSpeed * Time.deltaTime);

        if (isJumping && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * 2f * gravity);
            isJumping = false;
        }

        velocity.y -= gravity * Time.deltaTime;

        // Move the controller
        controller.Move(velocity * Time.deltaTime);
    }

    public void Jump()
    {
        isJumping = true;
    }
}
