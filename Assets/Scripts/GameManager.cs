using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IncrementScore()
    {
        score++;
        Debug.Log($"Score: {score}");
        scoreText.text = $"Score: {score}";
    }

}
