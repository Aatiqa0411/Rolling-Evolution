using UnityEngine;

public class RotateInPlace : MonoBehaviour
{
    public float rotationSpeed = 100f; // degrees per second
    PlayerController player;

    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
 private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player == null) return; // safety check

        if (CompareTag("Blue"))
        {
            player.score_add = 15;
            player.ChangeBallColor(Color.blue);
        }
        else if (CompareTag("Red"))
        {
            player.score_sub = 10;
            player.ChangeBallColor(Color.red);
        }
        else if (CompareTag("Green"))
        {
            player.moveSpeed = 10f;
            player.ChangeBallColor(Color.green);
        }
        else if (CompareTag("Purple"))
        {
            player.moveSpeed = 2f;
            player.ChangeBallColor(new Color(0.5f, 0f, 1f));
        }

        Destroy(gameObject); // It destroy the potion, not the player
    }
}

}
