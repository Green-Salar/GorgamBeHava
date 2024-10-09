using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMove : MonoBehaviourPun
{
    [SerializeField] private Joystick movementJoystick;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private Button jumpbtn;
    private CharacterController controller;
    private Vector3 velocity;
    private bool isJumping = false;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        movementJoystick = FindFirstObjectByType<JoystickFinder>().gameObject.GetComponent<FixedJoystick>();
        FindObjectOfType<JumpBtn>().gameObject.GetComponent<Button>().onClick.AddListener(() => Jump());
        moveSpeed = 5f;
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            MovePlayer();
        }
    }
    void MovePlayer()
    {
        // Check if the player is grounded
        bool isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // A small downward force to keep grounded
        }

        // Get input from joystick
        float horizontalInput = movementJoystick.Horizontal;
        float verticalInput = movementJoystick.Vertical;

        // Calculate move direction
        Vector3 move = transform.right * horizontalInput + transform.forward * verticalInput;

        // Apply movement
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Handle jumping
        if (isJumping && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * 2f * gravity);
            isJumping = false;
        }

        // Apply gravity
        velocity.y -= gravity * Time.deltaTime;

        // Move the controller
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
        }
    }

    public void Jump()
    {
        isJumping = true;
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
}
