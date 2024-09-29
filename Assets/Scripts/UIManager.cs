using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject fpsController; // Reference to the FPS Controller
    public GameObject uiPanel;       // Reference to the UI Panel that contains the buttons
    private bool isUIActive = false; // Track whether UI is active

    void Start()
    {
        // Initially ensure UI is hidden and controls are enabled
        uiPanel.SetActive(false);
        EnableFPSControls(true);
    }

    void Update()
    {
        // Toggle UI panel with a key, e.g., 'E'
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleUI();
        }
    }

    void ToggleUI()
    {
        isUIActive = !isUIActive;
        uiPanel.SetActive(isUIActive);

        if (isUIActive)
        {
            // Unlock cursor and disable FPS controls
            EnableFPSControls(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Ensure build mode is off if UI is active
            var assetPlacer = GetComponent<AssetPlacer>();
            if (assetPlacer != null)
            {
                assetPlacer.SetBuildMode(false); // A method to explicitly turn off build mode
            }
        }
        else
        {
            // Lock cursor and enable FPS controls
            EnableFPSControls(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void EnableFPSControls(bool enable)
    {
        // Assuming the FPSController script has a method or a property to enable/disable movement
        var controller = fpsController.GetComponent<FPSController>();
        if (controller != null)
        {
            controller.enabled = enable;
        }
    }
}
