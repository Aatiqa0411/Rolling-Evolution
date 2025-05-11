using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    public float rotationSpeed = 90f; // degrees per second
    public int scoreValue = 5;

    void Update()
    {
        // Rotate the coin around the Y-axis
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null) // Assuming ball has Rigidbody
        {
            // Add score
            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.AddScore(scoreValue);
            }

            // Destroy coin
            Destroy(gameObject);
        }
    }
}
