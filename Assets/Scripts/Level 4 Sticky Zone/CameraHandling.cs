using UnityEngine;
using System.Collections;

public class CameraFollowAndRotate : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0f, 1f, -2f); // Follow offset (used after transition)
    public float rotationSpeed = 3f;

    public PlayerMovement4 playerMovementScript;

    public Vector3 finalPosition = new Vector3(-2f, 5.527f, -16.58f); // Fixed target world position
    public float targetYRotation = 0f;                                // Fixed target rotation
    public float transitionDuration = 2f;

    private float currentYAngle = 0f;
    private bool cameraSettled = false;

    void Start()
    {
        if (playerMovementScript != null)
            playerMovementScript.enabled = false;

        StartCoroutine(TransitionToFixedPosition());
    }

    void LateUpdate()
    {
        if (!cameraSettled) return;

        float inputX = Input.GetAxis("Mouse X");
        currentYAngle += inputX * rotationSpeed;

        Quaternion rotation = Quaternion.Euler(0, currentYAngle, 0);
        transform.position = player.position + (rotation) * offset;
        transform.rotation = rotation;
    }

    IEnumerator TransitionToFixedPosition()
    {
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;
        Quaternion targetRot = Quaternion.Euler(0, targetYRotation, 0);

        float elapsed = 0f;

        while (elapsed < transitionDuration)
        {
            float t = elapsed / transitionDuration;

            transform.position = Vector3.Lerp(startPos, finalPosition, t);
            transform.rotation = Quaternion.Slerp(startRot, targetRot, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Snap exactly to final position and rotation
        transform.position = finalPosition;
        transform.rotation = targetRot;
        currentYAngle = targetYRotation;

        cameraSettled = true;

        if (playerMovementScript != null)
            playerMovementScript.enabled = true;
    }
}
