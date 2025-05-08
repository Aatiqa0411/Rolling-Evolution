using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Optional: Add to score or play sound here
            Debug.Log("Gold Coin Collected!");

            Destroy(gameObject); // Remove the coin
        }
    }
}
