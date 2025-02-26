using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField] private Transform coin;
    [SerializeField] private float rotationSpeed = 50f;

    void Update()
    {
        RotateCoin();
    }

    private void RotateCoin()
    {
        // Making coin rotate 
        coin.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
