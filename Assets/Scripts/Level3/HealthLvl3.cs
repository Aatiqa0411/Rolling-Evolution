using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthLvl3 : MonoBehaviour
{
    public Slider healthSlider;
    public int maxHealth = 100;
    public int currentHealth;
    public GameObject gameOverPanel;       // Reference to the GameOver UI panel
    public TextMeshProUGUI scoreText;      // Reference to TextMeshPro for displaying score
    public GameObject PausePanel;
    private bool isOnBridge = false;

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        gameOverPanel.SetActive(false); // Hide game over UI at start
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bridge"))
        {
            isOnBridge = true;
        }
        else if (other.CompareTag("water") && !isOnBridge)
        {
            Debug.Log("Ball entered the water trigger!");
            TakeDamage(20); // Only take damage if not on bridge
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bridge"))
        {
            isOnBridge = false;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        Time.timeScale = 0f; // Pause game
        gameOverPanel.SetActive(true);

        if (ScoreManager.instance != null)
        {
            scoreText.SetText("GAME OVER\nYour Score: {0}", ScoreManager.instance.score);
        }
    }

    public void ReplayGame()
    {
        Time.timeScale = 1f; // Resume time before restarting
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Pause()
    {
        PausePanel.SetActive(true);  // Show pause menu
        Time.timeScale = 0f;          // Freeze the game
    }
     public void Resume()
    {
        PausePanel.SetActive(false); // Hide pause menu
        Time.timeScale = 1f;          // Resume the game
    }
}
