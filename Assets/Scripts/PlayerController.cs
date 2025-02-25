using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject GameManager; // Game Manager
    [SerializeField] private Rigidbody playerRB; // Player Rigidbody
    [SerializeField] private float playerSpeed = 5f; // Movement speed
    [SerializeField] private float jumpForce = 10f; // Jump force
    [SerializeField] private Transform playerCamera; // Camera Transform

    [SerializeField] private Transform freeLookCamera; // Free look camera  

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
            playerSpeed = 2f;
            isGrounded = false;
        }
    }

    // Move the player
    public void MovePlayer(Vector3 input)
    {
        // Project the camera’s forward and right vectors onto the XZ plane (ignore Y-axis)
        Vector3 cameraForward = Vector3.Scale(playerCamera.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 cameraRight = Vector3.Scale(playerCamera.right, new Vector3(1, 0, 1)).normalized;

        // Convert input to world space relative to the camera
        Vector3 moveDirection = (cameraForward * input.z + cameraRight * input.x).normalized;

        playerRB.AddForce(moveDirection * playerSpeed); // Apply movement
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

    void Update()
    {

        // Rotate the player to face the same direction as the free look camera
        transform.forward = freeLookCamera.transform.forward;
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
}