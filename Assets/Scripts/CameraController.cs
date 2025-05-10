using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;          // drag the ball here
    public float distance  = 10f;     // how far behind
    public float height    = 5f;      // how high above
    public float posSmooth = 2f;    // smaller = snappier
    public float rotSmooth = 10f;      // camera turn speed

    private Vector3 velocity = Vector3.zero;
    private Vector3 lastMoveDir = Vector3.forward;   // fallback when ball stops
    private Rigidbody targetRb;

    void Awake()
    {
        if (target) targetRb = target.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!target) return;

        UpdateMoveDirection();                       // figure out “forward”

        // Desired camera position = behind movement dir + height offset
        Vector3 desiredPos = target.position
                             - lastMoveDir * distance
                             + Vector3.up * height;

        // Smooth damp to that position (no jitter)
        transform.position = Vector3.SmoothDamp(
            transform.position, desiredPos, ref velocity, posSmooth);

        // Smoothly rotate to look at the ball
        Quaternion lookRot = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot,
                                              rotSmooth * Time.fixedDeltaTime);
    }

    // -- helpers -----------------------------------------------------------
    void UpdateMoveDirection()
    {
        if (!targetRb) return;

        Vector3 vel = targetRb.velocity;
        vel.y = 0f;                                   // ignore vertical
        if (vel.sqrMagnitude > 0.05f)                 // moving enough?
            lastMoveDir = vel.normalized;             // update look-ahead
    }
}
