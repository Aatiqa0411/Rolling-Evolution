using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    [Header("References")]
    public Rigidbody rb;
    public VariableJoystick joystick;
    public GameObject mud;

    private bool isGrounded = true;
    private float currentYRotation = 0f;

    void Start()
    {
        // Auto-assign Rigidbody if not set in Inspector
        if (rb == null)
            rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 inputDirection = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        Vector3 rotatedDirection = Quaternion.Euler(0, currentYRotation, 0) * inputDirection;

        rb.AddForce(rotatedDirection * moveSpeed);
    }

    void Update()
    {
        // Jump if joystick pushed up and grounded
        if (joystick.Vertical > 0.8f && isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if touching the ground
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == mud)
        {
            ScoreManager.instance.SubtractPoints(2);
        }
    }

    // Turn Functions
    public void MoveLeft()
    {
        transform.Rotate(0, -90, 0);
        currentYRotation -= 90f;
    }

    public void MoveRight()
    {
        transform.Rotate(0, 90, 0);
        currentYRotation += 90f;
    }
}
