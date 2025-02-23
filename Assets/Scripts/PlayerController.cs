using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody playerBody; // player Rigidbody
    [SerializeField] private float playerSpeed = 4f; // Movement speed
    [SerializeField] private float jumpForce = 10f; // Jump force
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
        Vector3 moveDirection = new Vector3(input.x, 0, input.z); // Vector 3 representation of input
        playerBody.AddForce(moveDirection * playerSpeed); // Moving player

        if (input.y > 0 && isGrounded) // Jump check
        {
            playerBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Instant velocity change in Y direction
        }
    }
}
