using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Only if you're using TextMeshPro

public class GameOverHandler : MonoBehaviour
{
    public Transform ball;                 // Reference to the ball
    public float fallThreshold = -5f;      // Y position at which the ball is considered "fallen"

    public GameObject gameOverPanel;       // Assign in Inspector
    public TextMeshProUGUI scoreText;      // Assign if using TMP
    // public Text scoreText;              // Use this instead if not using TMP

    private bool isGameOver = false;

    void Start()
    {
        gameOverPanel.SetActive(false); // Hide at start
    }

    void Update()
    {
        if (!isGameOver && ball.position.y < fallThreshold)
        {
            TriggerGameOver();
        }
    }

    void TriggerGameOver()
    {
        isGameOver = true;

        // Show Game Over UI
        gameOverPanel.SetActive(true);

        // Show score if available
        if (ScoreManager.instance != null)
        {
            scoreText.SetText("GAME OVER\nYour Score: {0}", ScoreManager.instance.score);

        }

        // Optional: pause the game
        Time.timeScale = 0f;
    }

    public void ReplayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
