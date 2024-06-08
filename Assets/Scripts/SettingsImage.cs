using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsImage : MonoBehaviour
{
    public float zoomOutTime = 2f;

    private Vector3 initialScale;
    private Vector3 finalScale;

    public void Initialize()
    {
        finalScale = transform.localScale;
        initialScale = new Vector3(0.1f, 0.1f, 0.1f);
        transform.localScale = initialScale;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator ZoomToFinalScale()
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < zoomOutTime)
        {
            // Interpolate between initial and final scale
            transform.localScale = Vector3.Lerp(initialScale, finalScale, elapsedTime / zoomOutTime);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure final scale is set exactly
        transform.localScale = finalScale;
    }
}
