using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public Slider slider;
    public float duration = 5f; // Duration to gradually increase slider value

    private bool isJoinedLobby = false;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Connecting to Photon...");
        StartCoroutine(MoveSlider());
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master Server.");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby.");
        isJoinedLobby = true; // Mark that the lobby has been joined
        slider.value = 1f; // Set the slider value to 1 immediately
        StartCoroutine(DelayLoadScene()); // Start delay before loading the next scene
    }

    IEnumerator MoveSlider()
    {
        float elapsedTime = 0f;

        // Gradually increase the slider value over the specified duration
        while (!isJoinedLobby && elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            slider.value = Mathf.Clamp01(elapsedTime / duration);
            yield return null; // Wait until the next frame
        }

        // Ensure slider value is set to 1 if the lobby has been joined
        if (isJoinedLobby)
        {
            slider.value = 1f;
        }
    }

    IEnumerator DelayLoadScene()
    {
        // Wait for 1 second to allow the user to see that the loader is completed
        yield return new WaitForSeconds(1f);

        // Load the "Lobby" scene
        SceneManager.LoadScene("Lobby");
    }
}
