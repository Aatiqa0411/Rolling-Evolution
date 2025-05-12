using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // For TextMeshProUGUI

public class LevelComplete : MonoBehaviour
{
    public Transform ball;                    // Reference to the ball
    public GameObject levelCompletePanel;     // Assign Level Complete UI Panel
    public TextMeshProUGUI scoreText;         // Assign TMP text for final score

    private bool levelCompleted = false;

    void Start()
    {
        levelCompletePanel.SetActive(false); // Hide panel at start
    }

    void OnTriggerEnter(Collider other)
    {
        if (!levelCompleted && other.transform == ball)
        {
            levelCompleted = true;
            TriggerLevelComplete();
        }
    }

    void TriggerLevelComplete()
    {
        levelCompletePanel.SetActive(true);

        if (ScoreManager.instance != null)
        {
            scoreText.SetText("LEVEL COMPLETE\nYour Score: {0}", ScoreManager.instance.score);
        }

        Time.timeScale = 0f; // Pause the game
    }

    public void NextLevel()
    {
        Time.timeScale = 1f; // Resume time
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No next level. End of game?");
        }
    }

    public void GoHome()
    {
        Time.timeScale = 1f; // Unpause the game if it's paused
        SceneManager.LoadScene("MainMenu"); // Replace "MainMenu" with your scene name
    }
}
