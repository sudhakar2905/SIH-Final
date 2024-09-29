using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AssetPlacer : MonoBehaviour
{
    public Transform placementArea;               // Reference to the area where assets will be placed
    public GameObject buttonPrefab;               // Button prefab for dynamically creating buttons
    public Transform buttonContainer;             // The container (like a panel or content area) to hold the buttons
    public GameObject[] assets;                   // Array of prefabs for assets
    public GameObject[] buildingPrefabs;          // Array of building prefabs for key-based selection
    public GameObject[] buildingHologramPrefabs;  // Array of hologram prefabs corresponding to each building prefab
    public GameObject scrollView;                 // Reference to the scroll view containing the buttons

    private GameObject selectedAsset = null;      // Track the currently selected asset
    private GameObject selectedBuilding = null;   // Track the currently selected building
    private GameObject buildingHologram = null;   // Track the hologram of the selected building
    private bool isBuildMode = false;             // Track if in build mode
    private bool buildingSelected = false;        // Track if a building has been selected

    // Plane for positioning holograms
    private Plane placementPlane;

    void Start()
    {
        // Initialize the placement plane
        placementPlane = new Plane(Vector3.up, Vector3.zero); // Plane aligned with the ground

        // Generate buttons dynamically
        GenerateButtons();
    }

    // Function to generate buttons dynamically based on assets and buildings
    void GenerateButtons()
    {
        // Generate buttons for assets
        for (int i = 0; i < assets.Length; i++)
        {
            GameObject button = Instantiate(buttonPrefab, buttonContainer);
            button.GetComponentInChildren<TMP_Text>().text = assets[i].name; // Set button label
            int index = i; // Capture index to use in the button click event
            button.GetComponent<Button>().onClick.AddListener(() => SelectAsset(index));
        }

        // Generate buttons for buildings
        for (int i = 0; i < buildingPrefabs.Length; i++)
        {
            GameObject button = Instantiate(buttonPrefab, buttonContainer);
            button.GetComponentInChildren<TMP_Text>().text = buildingPrefabs[i].name; // Set button label
            int index = i; // Capture index to use in the button click event
            button.GetComponent<Button>().onClick.AddListener(() => SelectBuilding(index));
        }
    }

    // Function to select an asset based on button click
    void SelectAsset(int index)
    {
        selectedAsset = assets[index]; // Set the selected asset based on the clicked button
        Debug.Log("Selected Asset: " + selectedAsset.name);
        isBuildMode = true; // Enable build mode after selecting an asset
        buildingSelected = false; // Deselect any selected building
        DestroyHologram(); // Destroy any existing hologram
    }

    // Function to select a building based on button click
    void SelectBuilding(int index)
    {
        if (buildingHologram != null)
        {
            Destroy(buildingHologram); // Destroy existing hologram if any
        }

        selectedBuilding = buildingPrefabs[index];
        // Instantiate the corresponding hologram prefab
        buildingHologram = Instantiate(buildingHologramPrefabs[index]);
        buildingHologram.SetActive(false); // Hide initially until placement
        buildingSelected = true; // Mark a building as selected
        isBuildMode = true; // Enable build mode
        selectedAsset = null; // Deselect any selected asset
    }

    // Update is called once per frame
    void Update()
    {
        // Toggle build mode when pressing 'E'
        if (Input.GetKeyDown(KeyCode.E))
        {
            SetBuildMode(!isBuildMode); // Toggle build mode on and off
            scrollView.SetActive(isBuildMode); // Show or hide the scroll view based on build mode
        }

        // Check for mouse position and update hologram placement
        if (isBuildMode && buildingSelected && buildingHologram != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (placementPlane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter); // Get the point on the plane
                // Update hologram position
                buildingHologram.transform.position = hitPoint; // Place the hologram at the hit point
                buildingHologram.SetActive(true); // Show the hologram
            }
            else
            {
                buildingHologram.SetActive(false); // Hide if not over valid ground
            }
        }

        // Place the selected asset or building when clicking on the scene in build mode
        if (isBuildMode && Input.GetMouseButtonDown(0))
        {
            if (selectedAsset != null)
            {
                // Raycast to determine the placement position in the scene
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (placementPlane.Raycast(ray, out float enter))
                {
                    // Instantiate the selected asset at the hit position
                    Vector3 hitPoint = ray.GetPoint(enter); // Get the hit position on the plane
                    Instantiate(selectedAsset, hitPoint, Quaternion.identity, placementArea); // Keep asset upright
                }
            }
            else if (buildingSelected && selectedBuilding != null)
            {
                // Raycast to determine the placement position in the scene
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (placementPlane.Raycast(ray, out float enter))
                {
                    // Instantiate the selected building at the hit position
                    Vector3 hitPoint = ray.GetPoint(enter); // Get the hit position on the plane
                    Instantiate(selectedBuilding, hitPoint, selectedBuilding.transform.rotation, placementArea); // Use default rotation of the building
                    DestroyHologram(); // Remove the hologram after placement
                    selectedBuilding = null; // Deselect the building after placement
                    buildingSelected = false; // Disable building selection
                }
            }
        }
    }

    // Function to toggle build mode
    public void SetBuildMode(bool enable)
    {
        isBuildMode = enable;
        if (!enable)
        {
            selectedAsset = null; // Deselect the asset when exiting build mode
            DestroyHologram(); // Destroy the hologram when exiting build mode
            buildingSelected = false; // Disable building selection when exiting build mode
        }
        Debug.Log("Build Mode: " + isBuildMode);
    }

    // Helper function to destroy the hologram
    private void DestroyHologram()
    {
        if (buildingHologram != null)
        {
            Destroy(buildingHologram);
            buildingHologram = null; // Clear reference
        }
    }
}
