using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject GameManager; // Game Manager
    [SerializeField] private Rigidbody playerRB; // Player Rigidbody
    [SerializeField] private float playerSpeed = 5f; // Movement speed
    [SerializeField] private float dashSpeed = 50f; // Dash speed
    [SerializeField] private float gravity = 9.81f; // Gravity
    [SerializeField] private float jumpForce = 20f; // Jump force
    [SerializeField] private Transform playerCamera; // Camera Transform

    private bool isDoubleJump = false; // Double jump state
    private bool isGrounded = false; // Grounded state

    // Detect collision when landing
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            playerSpeed = 5f;
            isGrounded = true;
            isDoubleJump = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            GameManager.GetComponent<GameManager>().IncrementScore();
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    // Move the player
    public void MovePlayer(Vector3 input)
    {
        Vector3 moveDirection = GetMoveDirection();

        Vector3 velocity = moveDirection * playerSpeed;
        playerRB.linearVelocity = new Vector3(velocity.x, playerRB.linearVelocity.y, velocity.z);

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
        // Debug.Log("Linear Velocity: " + playerRB.linearVelocity);
        ApplyGravity();
        JumpCheck(input); // Jump check
    }

    private void JumpCheck(Vector3 input)
    {
        if (input.y > 0 && isGrounded)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        else if (input.y > 0 && !isGrounded && !isDoubleJump)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isDoubleJump = true;
        }
    }

    private void ApplyGravity()
    {
        playerRB.AddForce(Vector3.down * gravity * Time.deltaTime, ForceMode.Acceleration);
    }

    private Vector3 GetMoveDirection()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Project the camera’s forward and right vectors onto the XZ plane (ignore Y-axis)
        Vector3 cameraForward = playerCamera.forward;
        Vector3 cameraRight = playerCamera.right;
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Convert input to world space relative to the camera
        Vector3 moveDirection = (cameraForward * moveZ + cameraRight * moveX).normalized;
        return moveDirection;
    }

    public void Dash()
    {
        Vector3 moveDirection = GetMoveDirection(); // Get move direction
        Vector3 velocity = moveDirection * dashSpeed; // Calculate velocity
        playerRB.AddForce(velocity, ForceMode.VelocityChange); // Apply velocity

    }

}