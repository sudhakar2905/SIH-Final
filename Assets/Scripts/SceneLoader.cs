using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject loadingScreen;

    public void LoadScene(string sceneName)
    {
        loadingScreen.SetActive(true); // Show loading screen
        StartCoroutine(LoadAsync(sceneName));
    }

    private IEnumerator LoadAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            yield return null; // Wait for next frame
        }

        loadingScreen.SetActive(false); // Hide loading screen
    }
}
