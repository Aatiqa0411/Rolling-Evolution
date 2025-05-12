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
    public ParticleSystem dustTrail;

    // public int score = 0;

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
        HandleDustTrail();
    }

    void HandleDustTrail()
{
    Vector3 rayOrigin = transform.position + Vector3.down * 0.5f;
    float rayLength = 1f;

    if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, rayLength))
    {
        //Debug.Log("Is active: " + dustTrail.gameObject.activeInHierarchy); // should be true

        if (hit.collider.CompareTag("Ground"))
        {
            //Debug.Log("Raycast hit ground");

            //Debug.Log("Velocity: " + rb.velocity.magnitude);

            if (rb.velocity.magnitude > 0f)
            {
                if (!dustTrail.isPlaying)
                {
                    // //Debug.Log("Playing trail...");
                    if (!dustTrail.gameObject.activeInHierarchy)
                    {
                        dustTrail.gameObject.SetActive(true);
                    }

                    Vector3 direction = rb.velocity.normalized;
                    Quaternion rotation = Quaternion.LookRotation(-direction); // Trail goes opposite to movement
                    dustTrail.transform.rotation = rotation;
                    
                    dustTrail.Play();
                    //Debug.Log("Now playing: " + dustTrail.isPlaying);
                }
            }
            else
            {
                if (dustTrail.isPlaying)
                {
                    //Debug.Log("Velocity too low, stopping trail...");
                    dustTrail.Stop();
                }
            }
        }
        else
        {
            if (dustTrail.isPlaying)
            {
                //Debug.Log("Not ground, stopping trail...");
                dustTrail.Stop();
            }
        }
    }
    else
    {
        if (dustTrail.isPlaying)
        {
            //Debug.Log("Raycast missed, stopping trail...");
            dustTrail.Stop();
        }
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

        // if (rb.velocity.magnitude > 0.5f && !dustTrail.isPlaying)
        // {
        //     dustTrail.Play();
        //     Debug.Log("Movement: " + dustTrail.isPlaying);
        // }
    }

    void HandleJump()
    {

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;

        // if (dustTrail.isPlaying)
        // {    
        //     dustTrail.Stop();
        //     Debug.Log("Jumping: " + dustTrail.isPlaying);
        // }

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
            // score -= 2;
            GameManager.Instance.AddScore(-2);
            UpdateScoreUI();
            healthManager.TakeDamage(20); //can take damage 5 times
        } else if (collision.gameObject.CompareTag("Finish"))
        {
            Debug.Log("Reached the ending: " + collision.gameObject.name);
            Time.timeScale = 0f;
            FinishPanel.SetActive(true);
            FinishPanelText.text = "LEVEL FINISHED\nYOUR SCORE : " + GameManager.Instance.Score;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            // score += 5;
            GameManager.Instance.AddScore(5);
            UpdateScoreUI();
            Destroy(other.gameObject);
        }
    }

    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.CompareTag("Coin"))
    //     {
    //         // score -= 5;
    //         GameManager.Instance.AddScore(-5);
    //         UpdateScoreUI();
    //     }
    // }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + GameManager.Instance.Score;
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
