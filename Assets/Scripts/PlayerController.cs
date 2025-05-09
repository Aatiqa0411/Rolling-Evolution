using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 100f;

    private float rotationInput = 0f;

   void Update()
{
    // Move only when Up Arrow is held
    if (Input.GetKey(KeyCode.UpArrow))
    {
        Vector3 moveDirection = transform.forward;
       transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World);
    }

    // Apply rotation (from button click)
    if (rotationInput != 0)
    {
        transform.Rotate(0f, rotationInput * turnSpeed * Time.deltaTime, 0f);
    }
}



    // Call these from UI Button clicks
    public void TurnLeft()
    {
        rotationInput = -1f;
    }

    public void TurnRight()
    {
        rotationInput = 1f;
    }

    public void StopTurning()
    {
        rotationInput = 0f;
    }
}
