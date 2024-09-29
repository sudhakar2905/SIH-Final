using UnityEngine;
using TMPro;

public class MovementTutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel; // Reference to the entire tutorial panel
    public TextMeshProUGUI tutorialText; // Reference to the TextMeshPro UI element to provide instructions
    public GameObject[] buildingPrefabs; // Array of building prefabs
    private GameObject selectedBuilding; // Currently selected building

    private enum TutorialStep
    {
        MoveForward,
        MoveLeft,
        MoveBackward,
        MoveRight,
        SelectBuilding,
        PlaceBuilding,
        Completed
    }

    private TutorialStep currentStep = TutorialStep.MoveForward;
    private bool buildingPlaced = false;

    void Start()
    {
        // Show the tutorial panel at the start
        tutorialPanel.SetActive(true);
        tutorialText.text = "Press 'W' to move Forward";
    }

    void Update()
    {
        switch (currentStep)
        {
            case TutorialStep.MoveForward:
                CheckMoveForward();
                break;
            case TutorialStep.MoveLeft:
                CheckMoveLeft();
                break;
            case TutorialStep.MoveBackward:
                CheckMoveBackward();
                break;
            case TutorialStep.MoveRight:
                CheckMoveRight();
                break;
            case TutorialStep.SelectBuilding:
                // Waiting for the player to select a building
                break;
            case TutorialStep.PlaceBuilding:
                CheckPlaceBuilding();
                break;
            case TutorialStep.Completed:
                // Tutorial is completed
                break;
        }
    }

    private void CheckMoveForward()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            currentStep = TutorialStep.MoveLeft;
            tutorialText.text = "Good! Now press 'A' to move Left";
        }
    }

    private void CheckMoveLeft()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            currentStep = TutorialStep.MoveBackward;
            tutorialText.text = "Great! Now press 'S' to move Backward";
        }
    }

    private void CheckMoveBackward()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            currentStep = TutorialStep.MoveRight;
            tutorialText.text = "Nice! Now press 'D' to move Right";
        }
    }

    private void CheckMoveRight()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            currentStep = TutorialStep.SelectBuilding;
            tutorialText.text = "Awesome! Now choose an asset from a category.";
        }
    }

    // Call this method to select a building when an asset is chosen from the UI
    public void SelectBuilding(int buildingIndex)
    {
        if (selectedBuilding != null)
        {
            Destroy(selectedBuilding); // Destroy the previously selected building
        }

        selectedBuilding = Instantiate(buildingPrefabs[buildingIndex]);
        selectedBuilding.SetActive(false); // Hide until placed
        currentStep = TutorialStep.PlaceBuilding; // Move to place building step
        tutorialText.text = "Good! Now click on the ground to place the building.";
    }

    void CheckPlaceBuilding()
    {
        // Place the selected building at the mouse position when the player clicks
        if (Input.GetMouseButtonDown(0) && selectedBuilding != null)
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the object being hit is a valid ground or placement area
                if (hit.collider.CompareTag("Ground")) // Assuming the ground has a tag named "Ground"
                {
                    selectedBuilding.transform.position = hit.point;
                    selectedBuilding.SetActive(true);
                    buildingPlaced = true;

                    // You can add a confirmation message here or change the tutorial text
                    tutorialText.text = "Building placed successfully!";
                    Invoke("HideTutorial", 2f); // Hide the tutorial after 2 seconds
                }
                else
                {
                    // Notify the player that the placement area is invalid
                    tutorialText.text = "Invalid area! Please click on the ground to place the building.";
                }
            }
        }
    }


    void HideTutorial()
    {
        // Hide the tutorial panel
        tutorialPanel.SetActive(false);
    }
}
