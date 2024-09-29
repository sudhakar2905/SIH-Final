using UnityEngine;
using UnityEngine.SceneManagement; // Required for SceneManager

public class LoadNextScene : MonoBehaviour
{
    public void OnPlayButtonClicked()
    {
        // Get the current active scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Load the next scene by incrementing the index
        // Ensure that the next scene index is within bounds
        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            Debug.LogWarning("No more scenes to load.");
        }
    }
}
