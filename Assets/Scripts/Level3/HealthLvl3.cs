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

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        gameOverPanel.SetActive(false); // Hide game over UI at start
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("water"))
        {
             Debug.Log("Ball entered the water trigger!");
            TakeDamage(20); // Reduce 35 health on hitting water
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

        // Show score from ScoreManager
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
}
