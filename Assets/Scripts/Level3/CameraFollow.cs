using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform ball;
    public Vector3 offsetDirection = new Vector3(0, 2, -6);
    public float followSmoothness = 5f;

    private Vector3 currentOffset;

    void Start()
    {
        currentOffset = offsetDirection;
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = ball.position + currentOffset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSmoothness * Time.deltaTime);
        transform.LookAt(ball.position + Vector3.up * 1f);
    }

    public void RotateCameraRight()
    {
        currentOffset = Quaternion.Euler(0, 90, 0) * currentOffset;
    }

    public void RotateCameraLeft()
    {
        currentOffset = Quaternion.Euler(0, -90, 0) * currentOffset;
    }
}
