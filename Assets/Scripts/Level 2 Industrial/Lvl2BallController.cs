using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class Lvl2BallController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public VariableJoystick joystick;
    public Camera cam;
    public TextMeshProUGUI scoreText;
    public Lvl2HealthBar healthManager;
    public GameObject FinishPanel;
    public TextMeshProUGUI FinishPanelText;
    public int score = 0;

    private Rigidbody rb;
    private bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevents tumbling
        UpdateScoreUI();
        FinishPanel.SetActive(false);
    }

    void Update()
    {
        HandleMovement();
        if (joystick.Vertical > 0.8f && isGrounded)
        {
            HandleJump();
        }

    }

    void HandleMovement()
    {
        Vector3 moveDirection = Vector3.zero;

        float h = joystick.Horizontal;
        float v = joystick.Vertical;

        if (Mathf.Abs(h) > 0.2f || Mathf.Abs(v) > 0.2f)
        {
            moveDirection = new Vector3(h, 0, v).normalized;
            moveDirection = transform.TransformDirection(moveDirection); // Use local rotation
            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);
        }
    }

    void HandleJump()
    {
        // if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        // {
        //     rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        //     isGrounded = false;
        // }

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].point.y < transform.position.y - 0.4f)
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Obstacle hit: " + collision.gameObject.name);
            score -= 2;
            UpdateScoreUI();
            healthManager.TakeDamage(30);
        } else if (collision.gameObject.CompareTag("Finish"))
        {
            Debug.Log("Reached the ending: " + collision.gameObject.name);
            Time.timeScale = 0f;
            FinishPanel.SetActive(true);
            FinishPanelText.text = "LEVEL FINISHED\nYOUR SCORE : 0" + score;
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

    public void TurnLeft()
    {
        transform.Rotate(0, -90, 0);
    }

    public void TurnRight()
    {
        transform.Rotate(0, 90, 0);
    }
}
