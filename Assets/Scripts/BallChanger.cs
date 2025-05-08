using UnityEngine;

public class BallChanger : MonoBehaviour
{
    public GameObject balls_red;
    public GameObject balls_blue;
    public GameObject balls_acidgreen;
    public GameObject balls_orange;
    public GameObject balls_purple;
    public GameObject balls_pink;
    public GameObject balls_seagreen;

    private void OnTriggerEnter2D(Collider2D other)
    {
        string tag = other.tag;

        switch (tag)
        {
            case "RedPotion":
                SwapBall(balls_red);
                break;

            case "Blue":
                SwapBall(balls_blue);
                break;

            case "AcidGreen":
                SwapBall(balls_acidgreen);
                break;
            case "Orange":
                SwapBall(balls_orange);
                break;
            case "Purple":
                SwapBall(balls_purple);
                break;  
            case "Pink":  
                SwapBall(balls_pink);
                break;
            case "SeaGreen":  
                SwapBall(balls_seagreen);
                break;
            default:    
                Debug.Log("Unknown potion type: " + tag);
                break;  

            
        }

        Destroy(other.gameObject); // destroy potion after use
    }

    void SwapBall(GameObject newBallPrefab)
    {
        Vector3 currentPosition = transform.position;
        Quaternion currentRotation = transform.rotation;

        Instantiate(newBallPrefab, currentPosition, currentRotation);
        Destroy(gameObject); // destroy current ball
    }
}
