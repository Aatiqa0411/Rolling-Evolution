using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 100f;
    private float rotationInput = 0f;
    private Rigidbody rb;
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public VariableJoystick joystick;

    public HealthBar healthManager;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
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
            score += 5;
            UpdateScoreUI();
            Destroy(other.gameObject);
        }
        if(other.CompareTag("Finish"))
        {
            Debug.Log("You win!");
            // Add your win logic here
        }
        if(other.CompareTag("Obstacle"))
        {
            score-=2;
            UpdateScoreUI();
            healthManager.TakeDamage(30);
        
            // Add your lose logic here
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

    public void TurnLeft()
    {
        transform.Rotate(0, -90, 0); // Turn left (counter-clockwise)
    }

    public void TurnRight()
    {
        transform.Rotate(0, 90, 0); // Turn right (clockwise)
    }


}
