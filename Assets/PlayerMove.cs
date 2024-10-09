using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Joystick movementJoystick;
    [SerializeField] private float moveSpeed = 5f;

    private CharacterController controller;
    private Vector3 moveDirection;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Get input from joystick
        float horizontalInput = movementJoystick.Horizontal;
        float verticalInput = movementJoystick.Vertical;

        // Calculate move direction
        moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Rotate the player to face the movement direction
        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
    }

    void FixedUpdate()
    {
        // Apply movement
        if (moveDirection.magnitude >= 0.1f)
        {
            controller.Move(moveDirection * moveSpeed * Time.fixedDeltaTime);
        }
    }
}
