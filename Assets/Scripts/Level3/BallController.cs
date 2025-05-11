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

    private bool isGrounded = true;

    void Start()
    {
        // Auto-assign Rigidbody if not set in Inspector
        if (rb == null)
            rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 move = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        rb.AddForce(move * moveSpeed);
    }

    void Update()
    {
        // Jump if joystick is pushed up and grounded
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
}