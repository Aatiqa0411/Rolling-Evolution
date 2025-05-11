using UnityEngine;

public class PlayerCoinCollector : MonoBehaviour
{
    private int score = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            score += 1;
            Debug.Log("Coins Collected: " + score + "/15");
            Destroy(other.gameObject);
        }
    }
}
