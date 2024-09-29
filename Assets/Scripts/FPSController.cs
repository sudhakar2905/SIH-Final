using UnityEngine;

public class FPSController : MonoBehaviour
{
    public float speed = 5.0f;                // Movement speed
    public float jumpHeight = 2.0f;           // Jump height
    public float gravity = -9.81f;            // Gravity value
    public float mouseSensitivity = 100f;     // Mouse sensitivity for looking around

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    public Transform cameraHolder;            // Reference to the camera holder

    private float xRotation = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the game window
    }

    void Update()
    {
        // Check if the player is grounded
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Reset the downward velocity when grounded
        }

        // Player movement using WASD keys
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * speed * Time.deltaTime);

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Mouse look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate the player body (left and right)
        transform.Rotate(Vector3.up * mouseX);

        // Rotate the camera (up and down)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limit vertical rotation to avoid flipping
        cameraHolder.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
