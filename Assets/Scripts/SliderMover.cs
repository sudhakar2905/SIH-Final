using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SliderMover : MonoBehaviour
{
    public Slider slider; // Reference to the UI slider
    public float duration = 3f; // Duration in seconds

    void Start()
    {
        StartCoroutine(MoveSlider());
    }

    IEnumerator MoveSlider()
    {
        float elapsedTime = 0f;

        // Gradually increase the slider value over the specified duration
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            slider.value = Mathf.Clamp01(elapsedTime / duration);
            yield return null; // Wait until the next frame
        }

        // Ensure slider value is set to 1 at the end
        slider.value = 1f;
    }
}
