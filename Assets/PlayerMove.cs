using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviourPun
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
        movementJoystick = FindFirstObjectByType<JoystickFinder>().gameObject.GetComponent<FixedJoystick>();
    }

    void Update()
    {
<<<<<<< HEAD

        bool isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

=======
        if (photonView.IsMine)
        {
            MovePlayer();
        }
    }
    void MovePlayer()
    {
>>>>>>> main
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
<<<<<<< HEAD

        velocity.y -= gravity * Time.deltaTime;

        // Move the controller
        controller.Move(velocity * Time.deltaTime);
    }

    public void Jump()
=======

    }
    public void SpeedBoost()
    {
        StartCoroutine(SpeedBoostCoroutine());
    }
    IEnumerator SpeedBoostCoroutine()
    {
        moveSpeed = 20f;
        yield return new WaitForSeconds(5);
        moveSpeed = 5f;
    }
    void FixedUpdate()
>>>>>>> main
    {
        isJumping = true;
    }
}
