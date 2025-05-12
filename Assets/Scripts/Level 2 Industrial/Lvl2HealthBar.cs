using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lvl2HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public int maxHealth = 100;
    public int currentHealth;
    public Lvl2BallController playerController; // Reference to the Ball
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;     // Text to show "Your Score: X"
    public GameObject PausePanel;

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        gameOverPanel.SetActive(false); // Hide game over UI at start
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
        gameOverText.text = "GAME OVER\nYour Score: " + playerController.score;
    }
    public void ReplayGame()
    {
        Time.timeScale = 1f; // Resume time before restarting
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f; // Unpause if paused
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void GoHome()
    {
        Time.timeScale = 1f; // Unpause the game if it's paused
        SceneManager.LoadScene("MainMenu"); // Replace "MainMenu" with your scene name
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
