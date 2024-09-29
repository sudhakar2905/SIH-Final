using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Loader : MonoBehaviour
{
    public string sceneToLoad; // The name of the scene to load
    public Slider progressBar; // Reference to the UI progress bar

    void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        // Start loading the scene asynchronously
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);

        // Optional: Prevent the scene from activating until it's fully loaded
        operation.allowSceneActivation = false;

        // Update the progress bar
        while (!operation.isDone)
        {
            // The progress is a value between 0 and 0.9 (90%)
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.value = progress;

            // Check if the scene is fully loaded (90%)
            if (operation.progress >= 0.9f)
            {
                // Allow the scene to activate
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
