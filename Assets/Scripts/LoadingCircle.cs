using UnityEngine;
using UnityEngine.UI;

public class LoadingCircle : MonoBehaviour
{
    public Image loadingImage; // Assign your loading circle image in the inspector
    public float rotationSpeed = 200f; // Speed of rotation

    void Update()
    {
        // Rotate the loading image
        loadingImage.fillAmount += Time.deltaTime / 2f; // Adjust speed as needed
        if (loadingImage.fillAmount >= 1f)
        {
            loadingImage.fillAmount = 0f; // Reset fill amount
        }
    }
}
