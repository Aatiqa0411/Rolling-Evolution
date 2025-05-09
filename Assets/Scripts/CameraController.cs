using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float followDistance = 5f;
    public float heightOffset = 1f;  // Change this value to set camera height
    public float followSpeed = 5f;
    public float rotationSpeed = 5f;

    void LateUpdate()
    {
        if (player == null) return;

        // Calculate position behind the player
        Vector3 behindPosition = player.position - player.forward * followDistance;

        // Set camera height
        behindPosition.y = player.position.y + heightOffset;

        // Smooth follow
        transform.position = Vector3.Lerp(transform.position, behindPosition, Time.deltaTime * followSpeed);

        // Rotate to look at the player
        Quaternion lookRotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
}
