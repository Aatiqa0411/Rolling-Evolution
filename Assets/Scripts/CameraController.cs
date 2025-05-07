using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 moveInput;

    private PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.CameraControls.MoveCamera.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.CameraControls.MoveCamera.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnEnable()
    {
        controls.CameraControls.Enable();
    }

    private void OnDisable()
    {
        controls.CameraControls.Disable();
    }

    private void Update()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed * Time.deltaTime;
        transform.Translate(move, Space.World);
    }
}
