using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, 5, -7);
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (player == null) return;

        // Match rotation
        Quaternion targetRotation = Quaternion.Euler(30f, player.eulerAngles.y, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smoothSpeed);

        // Follow position
        Vector3 desiredPosition = player.position + player.TransformDirection(offset);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
    }
}
