using UnityEngine;

public class BallMover : MonoBehaviour
{
    public float moveAmount = 1f;
    public GameObject mud;

    public void MoveLeft()
    {
        transform.position += new Vector3(-moveAmount, 0f, 0f);
    }

    public void MoveRight()
    {
        transform.position += new Vector3(moveAmount, 0f, 0f);
    }

    void OnTriggerEnter(Collider other)
{
    if (other.gameObject == mud )
    {
        ScoreManager.instance.SubtractPoints(2);
    }
}
}
