using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody playerBody; // player Rigidbody
    [SerializeField] private float playerSpeed = 4f; // Movement speed
    [SerializeField] private float jumpForce = 10f; // Jump force
    [SerializeField] private float maxSpeed = 10f; // Max speed by player
    [SerializeField] private Transform cameraRotation;
    private bool isGrounded = false; // Grounded state

    // Detect collision when landing
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground") // Detects if ground is touched
        {
            isGrounded = true;
        }
    }

    // Detect collision when leaping
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground") // Detects if ground is touched
        {
            isGrounded = false;
        }
    }

    // Move the player
    public void movePlayer(Vector3 input)
    {
        // Get camera's forward and right vectors
        Vector3 cameraForward = cameraRotation.forward;
        Vector3 cameraRight = cameraRotation.right;

        // Ignore vertical rotation
        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 moveDirection = (cameraForward * input.z + cameraRight * input.x).normalized; // Rotated position

        // Move the player with a capped speed
        Vector3 clampedDirection = Vector3.ClampMagnitude(moveDirection * playerSpeed, maxSpeed);
        playerBody.AddForce(clampedDirection);

        if (input.y > 0 && isGrounded) // Jump check
        {
            playerBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Instant velocity change in Y direction
        }
    }
}
