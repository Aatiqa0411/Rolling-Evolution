using UnityEngine;
using TMPro; // Required for TextMeshPro

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // TMP UI component
    public int score = 0;

    public static ScoreManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void AddScore(int value)
    {
        score += value;
        scoreText.text = "Score: " + score;
    }

    public void SubtractPoints(int amount)
    {
        score -= amount;
        scoreText.text = "Score: " + score;
    }
}
