using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BallGameplay : MonoBehaviour
{
    public int score = 0;
    public int coinsToUnlock = 10;
    public float health = 100f;
    public float healthDrainRate = 10f;
    public float checkInterval = 1f;
    public PlayerMovement4 movementScript;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinMessageText;
    public GameObject finishPanel;
    public GameObject gameOverPanel;
    public GameObject exitObstacle;
    public HealthBar4 healthBar;

    private float originalSpeed;
    private float nextHealthDrainTime = 0f;

    void Start()
    {
        if (movementScript != null)
            originalSpeed = movementScript.moveSpeed;

        UpdateUI();
        if (finishPanel != null)
            finishPanel.SetActive(false);
    }

    void Update()
    {
        if (IsOnLiquid() && Time.time >= nextHealthDrainTime)
        {
            nextHealthDrainTime = Time.time + checkInterval;
            health -= healthDrainRate;

            if (healthBar != null)
                healthBar.SetHealth(health);

            if (movementScript != null)
                movementScript.moveSpeed = originalSpeed / 3f;

            if (health <= 0)
                GameOver();
        }

        else if (!IsOnLiquid() && movementScript != null)
        {
            movementScript.moveSpeed = originalSpeed;
        }
    }

    bool IsOnLiquid()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 1f))
            return hit.collider.CompareTag("LVL4-Liquid");

        return false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            score++;
            Destroy(other.gameObject);
            UpdateUI();

            if (score >= coinsToUnlock && exitObstacle != null)
                Destroy(exitObstacle);
        }

        if (other.CompareTag("Finish"))
        {
            Time.timeScale = 0f;
            if (finishPanel != null)
                finishPanel.SetActive(true);
        }
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;

        if (coinMessageText != null)
        {
            int remaining = Mathf.Max(0, coinsToUnlock - score);
            coinMessageText.text = remaining > 0 ?
                $"Collect {remaining} more coins to unlock gate!" : "";
        }
    }

    void GameOver()
    {
        Time.timeScale = 0f;
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
        if (coinMessageText != null)
            coinMessageText.text = "GAME OVER\nYou ran out of health.";
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f; // Unpause if paused
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

}
