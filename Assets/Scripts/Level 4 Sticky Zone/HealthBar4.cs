using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthBar4 : MonoBehaviour
{
    public Slider healthSlider;
    public int maxHealth = 100;
    public int currentHealth;
    public GameObject PausePanel;

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void SetHealth(float newHealth)
    {
        currentHealth = Mathf.Clamp((int)newHealth, 0, maxHealth);
        healthSlider.value = currentHealth;
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


    public void ReplayGame()
    {
        Time.timeScale = 1f; // Resume time before restarting
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
