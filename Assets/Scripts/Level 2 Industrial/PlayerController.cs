using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float moveForce = 10f;
    public float jumpForce = 5f;
    public Camera cam;
    public TextMeshProUGUI scoreText;

    private Rigidbody rb;
    private bool isGrounded = true;
    private int score = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        UpdateScoreUI();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void Update()
    {
        HandleJump();
    }

    void HandleMovement()
    {
        Vector3 moveInput = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow))
            moveInput += Vector3.forward;
        if (Input.GetKey(KeyCode.DownArrow))
            moveInput += Vector3.back;
        if (Input.GetKey(KeyCode.LeftArrow))
            moveInput += Vector3.left;
        if (Input.GetKey(KeyCode.RightArrow))
            moveInput += Vector3.right;

        // Convert input to camera-relative movement
        Vector3 camForward = cam.transform.forward;
        Vector3 camRight = cam.transform.right;
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 move = (camForward * moveInput.z + camRight * moveInput.x).normalized;
        rb.AddForce(move * moveForce);
    }


    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Basic grounded check
        if (collision.contacts[0].point.y < transform.position.y - 0.4f)
        {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            score += 5;
            UpdateScoreUI();
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            score -= 5;
            UpdateScoreUI();
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}
