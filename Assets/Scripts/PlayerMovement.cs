using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Maximum movement speed
    public float accelerationTime = 1f; // Time to reach max speed

    private Vector3 moveDirection; // Direction to move
    private bool isMoving = false; // Is the player currently moving?
    private Vector3 initialTouchPosition; // Initial touch position
    private Vector3 currentTouchPosition; // Current touch position
    private float currentSpeed = 0f; // Current movement speed
    private float speedTimer = 0f; // Timer to control the speed lerping

    void Update()
    {
        HandleInput();

        if (isMoving)
        {
            // Gradually increase speed over time (Lerp from 0 to max speed)
            speedTimer += Time.deltaTime / accelerationTime;
            currentSpeed = Mathf.Lerp(0, moveSpeed, speedTimer);

            // Move the player in the direction on the XZ plane
            Vector3 movement = new Vector3(moveDirection.x, 0, moveDirection.z);
            transform.Translate(movement * currentSpeed * Time.deltaTime, Space.World);
        }
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0)) // When mouse or touch begins
        {
            // Capture the initial touch position
            initialTouchPosition = GetWorldPositionFromInput();
            isMoving = true; // Start moving
            speedTimer = 0f; // Reset the speed timer for smooth acceleration
        }

        if (Input.GetMouseButton(0)) // While mouse or touch is held down
        {
            // Continuously update the current touch position
            currentTouchPosition = GetWorldPositionFromInput();

            // Calculate the new direction to move based on the drag
            moveDirection = (new Vector3(currentTouchPosition.x, 0, currentTouchPosition.z) - new Vector3(transform.position.x, 0, transform.position.z)).normalized;
        }

        if (Input.GetMouseButtonUp(0)) // When the touch or mouse is released
        {
            isMoving = false; // Stop moving
            currentSpeed = 0f; // Reset speed when movement stops
        }
    }

    // Convert screen position (mouse/touch) to world coordinates on the XZ plane
    Vector3 GetWorldPositionFromInput()
    {
        Vector3 screenPos = Input.mousePosition;
        screenPos.z = Camera.main.WorldToScreenPoint(transform.position).z; // Maintain Z value for depth
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        return new Vector3(worldPos.x, 0, worldPos.z); // Ignore Y-axis for XZ movement
    }
}
