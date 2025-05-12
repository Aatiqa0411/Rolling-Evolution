using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int Score { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep between scenes
        }
        else
        {
            Destroy(gameObject); // Yeet duplicates
        }
    }

    public void AddScore(int points)
    {
        Score += points;
        Debug.Log("Score Updated: " + Score);
    }

    public void ResetScore()
    {
        Score = 0;
        Debug.Log("Score Reset");
    }
}
