using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    // Changed to Vector 3 to support jump 
    public UnityEvent<Vector3> OnMove = new UnityEvent<Vector3>();

    void Update()
    {
        // Zeroing input
        Vector3 inputVector = Vector3.zero;

        // Forward
        if (Input.GetKey(KeyCode.W))
        {
            inputVector += Vector3.forward;
        }

        // Backward
        if (Input.GetKey(KeyCode.S))
        {
            inputVector += Vector3.back;
        }

        // Left
        if (Input.GetKey(KeyCode.A))
        {
            inputVector += Vector3.left;
        }

        // Right
        if (Input.GetKey(KeyCode.D))
        {
            inputVector += Vector3.right;
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inputVector += Vector3.up;
        }

        // Invoking Ball Controller
        OnMove?.Invoke(inputVector);
    }
}
 