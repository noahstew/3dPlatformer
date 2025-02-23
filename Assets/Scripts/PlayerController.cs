using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private GameObject GameManager; // Game Manager
    [SerializeField] private Rigidbody playerRB; // Player Rigidbody
    [SerializeField] private float playerSpeed = 4f; // Movement speed
    [SerializeField] private float jumpForce = 10f; // Jump force
    private bool isGrounded = false; // Grounded state

    // Detect collision when landing
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Detects if ground is touched
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Coin")) // Detects if obstacle is touched
        {
            GameManager.GetComponent<GameManager>().IncrementScore(); // Increment score
            Destroy(collision.gameObject); // Destroy obstacle
        }
    }

    // Detect collision when leaping
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Detects if ground is left
        {
            isGrounded = false;
        }
    }

    // Move the player
    public void MovePlayer(Vector3 input)
    {
        Vector3 inputXZPlane = new Vector3(input.x, 0, input.z); // Vector 3 representation of input
        playerRB.AddForce(inputXZPlane * playerSpeed); // Moving player
        JumpCheck(input); // Jump check
    }

    private void JumpCheck(Vector3 input)
    {
        if (input.y > 0 && isGrounded) // Jump check
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Instant velocity change in Y direction
        }
    }


}
