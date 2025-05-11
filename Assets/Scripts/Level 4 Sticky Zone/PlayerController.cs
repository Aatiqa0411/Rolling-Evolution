using UnityEngine;
using TMPro;

public class PlayerController4 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 100f;
    private float rotationInput = 0f;
    private Rigidbody rb;
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public VariableJoystick joystick;
    public int score_add = 5;
    public int score_sub = 2;
    public HealthBar healthManager;
    public Renderer ballRenderer;
    public GameObject FinishPanel;
    public TextMeshProUGUI Text;
    void Start()
    {
        ballRenderer = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        ballRenderer = GetComponent<Renderer>();
        FinishPanel.SetActive(false);
        score = 0;
        UpdateScoreUI();
    }

    void Update()
    {
        HandleMovement();

    }

    void HandleMovement()
    {
        Vector3 moveDirection = Vector3.zero;

        // Forward/backward
        if (joystick.Vertical > 0.5f)
        {
            moveDirection += transform.forward;
        }
        else if (joystick.Vertical < -0.5f)
        {
            moveDirection -= transform.forward;
        }

        else if (joystick.Horizontal > 0.5f)
        {
            moveDirection += transform.right;
        }
        else if (joystick.Horizontal < -0.5f)
        {
            moveDirection -= transform.right;
        }


        // Apply movement
        if (moveDirection != Vector3.zero)
        {
            rb.MovePosition(rb.position + moveDirection.normalized * moveSpeed * Time.deltaTime);
        }

        // Rotation from UI buttons
        if (rotationInput != 0)
        {
            transform.Rotate(0f, rotationInput * turnSpeed * Time.deltaTime, 0f);
        }
    }


    bool IsGround()
    {
        // Check if the player is grounded
        return Physics.Raycast(transform.position, Vector3.down, 0.6f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{name} triggered with {other.name}");

        if (other.CompareTag("Coin"))
        {
            score = score + score_add;
            UpdateScoreUI();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Finish"))
        {
            Debug.Log("You win!");
            // Add your win logic here
        }
        if (other.CompareTag("Obstacle"))
        {
            score = score - score_sub;
            UpdateScoreUI();
            healthManager.TakeDamage(30);

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            score += 5;
            UpdateScoreUI();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Obstacle"))
        {
            score -= 5;
            UpdateScoreUI();
            healthManager.TakeDamage(30);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Finish"))
        {
            FinishPanel.SetActive(true);
            Time.timeScale = 0f; // Pause game
            FinishPanel.SetActive(true);
            Text.text = "GAME OVER\nYour Score: " + score;
        }
    }

    public void ChangeBallColor(Color newColor)
    {
        if (ballRenderer != null)
        {
            ballRenderer.material.color = newColor;
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