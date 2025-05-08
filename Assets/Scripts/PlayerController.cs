using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 100f;
    public float jumpForce = 7f;
    private float rotationInput = 0f;

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Movement input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Move the player using Rigidbody (better for collision handling)
        Vector3 move = new Vector3(horizontal, 0f, vertical).normalized * moveSpeed;
        Vector3 newPosition = transform.position + move * Time.deltaTime;
        rb.MovePosition(newPosition);  // Move with Rigidbody, respecting physics

        // Rotate the player based on input (still using local space)
        if (rotationInput != 0)
        {
            transform.Rotate(0f, rotationInput * turnSpeed * Time.deltaTime, 0f);
        }

        // Jump input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    public void TurnLeft() => rotationInput = -1f;
    public void TurnRight() => rotationInput = 1f;
    public void StopTurning() => rotationInput = 0f;

    void OnCollisionEnter(Collision collision)
    {
        // Handle ground collision (for jumping)
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        // Detect collision with obstacles
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Collided with Obstacle");
        }
    }
}
