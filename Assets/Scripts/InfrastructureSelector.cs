using UnityEngine;
using UnityEngine.UI;
using TMPro; // Include the TextMesh Pro namespace

public class InfrastructureSelector : MonoBehaviour
{
    public Transform categoryParentList;  // Parent container for category buttons (Category ScrollView Content)
    public Transform assetParentList;     // Parent container for asset buttons (Asset ScrollView Content)
    public GameObject buttonPrefab;       // Button prefab to display categories and assets
    public GameObject[] buildingPrefabs;  // Array of building prefabs
    public GameObject[] streetLightPrefabs; // Array of street light prefabs
    public GameObject placementArea;      // Reference to the placement area
    public Material hologramMaterial;      // Material for the hologram

    // References to the scroll view GameObjects
    public GameObject categoryScrollView; // Reference to the category scroll view
    public GameObject assetScrollView;    // Reference to the asset scroll view

    private GameObject selectedInfrastructure = null;  // Track the currently selected infrastructure
    private GameObject hologramInstance;               // Track the hologram instance
    private Plane placementPlane;                      // Plane for positioning holograms

    void Start()
    {
        // Initialize the placement plane
        placementPlane = new Plane(Vector3.up, Vector3.zero); // Plane aligned with the ground

        // Add categories to the category scroll view
        AddCategoryButton("Buildings", buildingPrefabs);
        AddCategoryButton("Street Lights", streetLightPrefabs);

        // Hide scroll views initially
        categoryScrollView.SetActive(false);
        assetScrollView.SetActive(false);

        // Unlock the cursor initially
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        // Toggle scroll views when "E" key is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            bool isActive = categoryScrollView.activeSelf; // Check current state
            categoryScrollView.SetActive(!isActive);       // Toggle category scroll view
            assetScrollView.SetActive(false);               // Hide asset scroll view

            // Show the cursor and stop camera movement when scroll view is active
            if (!isActive)
            {
                Cursor.lockState = CursorLockMode.None; // Unlock the cursor
                Cursor.visible = true;                   // Show the cursor
                // Optionally stop camera movement here if it's in another script
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked; // Lock the cursor again when closed
                Cursor.visible = false;                     // Hide the cursor again
                // Resume camera movement here if it's in another script
            }
        }

        if (hologramInstance != null)
        {
            UpdateHologramPosition();

            // Place the original prefab when the left mouse button is clicked
            if (Input.GetMouseButtonDown(0))
            {
                PlaceInfrastructure();
            }
        }
    }

    // Function to add a category button to the category scroll view
    void AddCategoryButton(string categoryName, GameObject[] prefabs)
    {
        // Instantiate a button for the category
        GameObject buttonInstance = Instantiate(buttonPrefab, categoryParentList);
        Button button = buttonInstance.GetComponent<Button>();
        TextMeshProUGUI buttonText = buttonInstance.GetComponentInChildren<TextMeshProUGUI>(); // Use TMP component
        buttonText.text = categoryName;

        // Assign the button click event to show infrastructures in the category
        button.onClick.AddListener(() => ShowInfrastructures(prefabs));
    }

    // Function to display infrastructures of a specific category in the asset scroll view
    void ShowInfrastructures(GameObject[] infrastructures)
    {
        // Clear the current list of asset buttons in the Asset ScrollView
        foreach (Transform child in assetParentList)
        {
            Destroy(child.gameObject);
        }

        // Add a button for each infrastructure in the selected category
        foreach (GameObject infrastructure in infrastructures)
        {
            GameObject buttonInstance = Instantiate(buttonPrefab, assetParentList);
            Button button = buttonInstance.GetComponent<Button>();
            TextMeshProUGUI buttonText = buttonInstance.GetComponentInChildren<TextMeshProUGUI>(); // Use TMP component
            buttonText.text = infrastructure.name;

            // Assign the button click event to select the infrastructure
            button.onClick.AddListener(() => SelectInfrastructure(infrastructure));
        }

        // Show the asset scroll view
        assetScrollView.SetActive(true);
    }

    // Function to select an infrastructure
    void SelectInfrastructure(GameObject infrastructure)
    {
        selectedInfrastructure = infrastructure;
        Debug.Log("Selected Infrastructure: " + selectedInfrastructure.name);

        // Instantiate the hologram
        if (hologramInstance != null)
        {
            Destroy(hologramInstance); // Destroy previous hologram
        }

        // Create a new hologram instance at a default position
        hologramInstance = Instantiate(selectedInfrastructure, Vector3.zero, Quaternion.identity);
        ApplyHologramMaterial(hologramInstance);
    }

    // Update the hologram position based on mouse position
    void UpdateHologramPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (placementPlane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter); // Get the hit point on the plane
            hologramInstance.transform.position = hitPoint; // Move hologram to the hit point
            hologramInstance.transform.rotation = Quaternion.identity; // Adjust rotation if needed
        }
    }

    // Function to place the original prefab at the selected location
    void PlaceInfrastructure()
    {
        if (selectedInfrastructure != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (placementPlane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter); // Get the hit position on the plane
                Instantiate(selectedInfrastructure, hitPoint, Quaternion.identity, placementArea.transform); // Place the original prefab
                Destroy(hologramInstance); // Destroy the hologram after placement
                hologramInstance = null; // Clear hologram reference
                selectedInfrastructure = null; // Clear the selected infrastructure
            }
        }
    }

    // Function to apply the hologram material to the instance
    void ApplyHologramMaterial(GameObject obj)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            if (renderer != null)
            {
                renderer.material = hologramMaterial; // Change to hologram material
                Debug.Log("Applied hologram material to: " + renderer.gameObject.name);
            }
        }
    }

    // Function to get the currently selected infrastructure
    public GameObject GetSelectedInfrastructure()
    {
        return selectedInfrastructure;
    }
}
