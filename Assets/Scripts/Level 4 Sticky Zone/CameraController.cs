using UnityEngine;

public class CameraFollow4 : MonoBehaviour
{
    public Transform player;
    public float followDistance = 5f;
    public float heightOffset = 1f;
    public float followSpeed = 5f;
    public float rotationSpeed = 5f;

    private float currentAngle = 0f;        // current horizontal rotation around player
    private float targetAngle = 0f;         // where we want the camera to rotate to

    void Start()
    {
        if (player != null)
        {
            // Initial calculation to get the camera's starting position and facing direction
            Vector3 direction = player.position - transform.position; // Corrected direction to the player
            direction.y = 0; // Ignore vertical axis (height)
            currentAngle = targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg; // Calculate angle
        }
    }

    void LateUpdate()
    {
        if (player == null) return;

        // Update the target angle to rotate based on player's rotation
        targetAngle = player.eulerAngles.y;

        // Smooth the rotation of the camera towards the target angle
        currentAngle = Mathf.LerpAngle(currentAngle, targetAngle, Time.deltaTime * rotationSpeed);

        // Calculate the camera's position relative to the player
        Quaternion rotation = Quaternion.Euler(0, currentAngle, 0);
        Vector3 offset = rotation * new Vector3(0, 0, -followDistance);
        Vector3 desiredPosition = player.position + offset;
        desiredPosition.y = player.position.y + heightOffset;

        // Smooth camera movement
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * followSpeed);

        // Always look at the player from eye level
        transform.LookAt(player.position + Vector3.up * heightOffset);
    }
}
