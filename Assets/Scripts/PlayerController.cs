using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 100f;
    public float jumpHeight = 2f;
    public float gravity = 9.81f;
    public float jumpSpeed = 5f;
    private float rotationInput = 0f;
    private float verticalVelocity = 0f;
    private bool isJumping = false;
    private Vector3 moveDirection;
    
    private  int score = 0;
    public TextMeshProUGUI scoreText;
    void Start()
    {
        score=0;
        UpdateScoreUI();
    }
    void Update()
    {
        HandleMovement();
        HandleJump();
    }

    void HandleMovement()
    {
        Vector3 moveDirection = Vector3.zero;

        // Forward/backward
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveDirection += transform.forward;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            moveDirection -= transform.forward;
        }

        // Left/right swipe (strafe)
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveDirection -= transform.right;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDirection += transform.right;
        }

        // Apply movement
        if (moveDirection != Vector3.zero)
        {
            transform.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime, Space.World);
        }

        // Rotation from UI buttons
        if (rotationInput != 0)
        {
            transform.Rotate(0f, rotationInput * turnSpeed * Time.deltaTime, 0f);
        }
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            verticalVelocity = jumpSpeed;
            isJumping = true;
        }

        if (isJumping)
        {
            verticalVelocity -= gravity * Time.deltaTime;
            transform.position += Vector3.up * verticalVelocity * Time.deltaTime;

            // Check if player has landed
            if (transform.position.y <= 0.5f) // assume ground is at y = 0
            {
                Vector3 pos = transform.position;
                pos.y = 0.5f;
                transform.position = pos;
                verticalVelocity = 0f;
                isJumping = false;
            }
        }
    }

private void OnTriggerEnter(Collider other)
{
    Debug.Log($"{name} triggered with {other.name}");

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
