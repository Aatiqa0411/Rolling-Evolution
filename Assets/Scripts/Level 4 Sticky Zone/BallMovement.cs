using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public VariableJoystick joystick;  // Optional — assign for mobile UI
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 moveDirection = Vector3.zero;

        if (joystick != null)
        {
            // Joystick-based movement
            float h = joystick.Horizontal;
            float v = joystick.Vertical;

            moveDirection = transform.forward * v + transform.right * h;
        }
        else
        {
            Console.WriteLine("Using keyboard\n");
            // Keyboard-based movement
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {

                Console.WriteLine("W, Up pressed!");
                moveDirection += transform.forward;
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                moveDirection -= transform.forward;
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                moveDirection += transform.right;
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                moveDirection -= transform.right;
        }

        if (moveDirection != Vector3.zero)
        {
            rb.MovePosition(rb.position + moveDirection.normalized * moveSpeed * Time.deltaTime);
        }
    }
}
