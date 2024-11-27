using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel; // Reference to the entire tutorial panel
    public TextMeshProUGUI tutorialText; // Reference to the TextMeshPro UI element to provide instructions

    private enum TutorialStep
    {
        MoveForward,
        MoveLeft,
        MoveBackward,
        MoveRight,
        ChooseCategory,
        ChooseAsset,
        PlaceObject,
        Completed
    }

    private TutorialStep currentStep = TutorialStep.MoveForward;

    void Start()
    {
        // Show the tutorial panel at the start
        tutorialPanel.SetActive(true);
        tutorialText.text = "Press 'W' to move forward.";
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
            case TutorialStep.ChooseCategory:
                CheckChooseCategory();
                break;
            case TutorialStep.ChooseAsset:
                CheckChooseAsset();
                break;
            case TutorialStep.PlaceObject:
                CheckPlaceObject();
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
            tutorialText.text = "Good! Now press 'A' to move left.";
        }
    }

    private void CheckMoveLeft()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            currentStep = TutorialStep.MoveBackward;
            tutorialText.text = "Great! Now press 'S' to move backward.";
        }
    }

    private void CheckMoveBackward()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            currentStep = TutorialStep.MoveRight;
            tutorialText.text = "Nice! Now press 'D' to move right.";
        }
    }

    private void CheckMoveRight()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            currentStep = TutorialStep.ChooseCategory;
            tutorialText.text = "Awesome! Now choose a category.";
        }
    }

    private void CheckChooseCategory()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentStep = TutorialStep.ChooseAsset;
            tutorialText.text = "Good! Now choose an asset from the category.";
        }
    }

    private void CheckChooseAsset()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentStep = TutorialStep.PlaceObject;
            tutorialText.text = "Great! Now place the object on the ground.";
        }
    }

    private void CheckPlaceObject()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // You can implement logic here to check if the object was successfully placed
            // For now, we will simply complete the tutorial
            tutorialText.text = "Congratulations! You've completed the tutorial!";
            CompleteTutorial();
        }
    }

    void CompleteTutorial()
    {
        currentStep = TutorialStep.Completed;
        tutorialPanel.SetActive(false); // Hide the tutorial panel after the tutorial is complete
    }
}
