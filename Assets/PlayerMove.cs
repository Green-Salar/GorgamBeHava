using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviourPun
{
    [SerializeField] private Joystick movementJoystick;
    [SerializeField] private float moveSpeed = 5f;

    private CharacterController controller;
    private Vector3 moveDirection;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        movementJoystick = FindFirstObjectByType<JoystickFinder>().gameObject.GetComponent<FixedJoystick>();
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
    {
        // Apply movement
        if (moveDirection.magnitude >= 0.1f)
        {
            controller.Move(moveDirection * moveSpeed * Time.fixedDeltaTime);
        }
    }
}
