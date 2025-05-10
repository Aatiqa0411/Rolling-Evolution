using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public int maxHealth = 100;
    public int currentHealth;
    public PlayerController playerController; // Reference to the PlayerController script
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;     // Text to show "Your Score: X"
   

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
}
