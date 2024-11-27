using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of camera movement
    public float rotationSpeed = 100f; // Speed of camera rotation
    private bool canMove = true; // Control variable for camera movement

    void Update()
    {
        // Check for toggle input
        if (Input.GetKeyDown(KeyCode.E))
        {
            canMove = !canMove; // Toggle the movement state

            // Lock or unlock cursor based on the movement state
            if (canMove)
            {
                Cursor.lockState = CursorLockMode.Locked; // Lock the cursor when moving
                Cursor.visible = false; // Hide the cursor
            }
            else
            {
                Cursor.lockState = CursorLockMode.None; // Unlock the cursor when not moving
                Cursor.visible = true; // Show the cursor
            }
        }

        // Only move the camera if canMove is true
        if (canMove)
        {
            MoveCamera();
        }
    }

    private void MoveCamera()
    {
        // Camera movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical);
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);

        // Camera rotation (optional)
        if (Input.GetMouseButton(1)) // Right mouse button for rotation
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            transform.Rotate(Vector3.up * mouseX);
            Camera.main.transform.Rotate(Vector3.left * mouseY);
        }
    }
}
