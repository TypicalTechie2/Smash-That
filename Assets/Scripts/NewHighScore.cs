using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewHighScore : MonoBehaviour
{
    [SerializeField] Vector3 initialScale;
    [SerializeField] Vector3 maxScale;

    public float zoomSpeed = 10f;

    public void Initialize()
    {
        maxScale = new Vector3(10, 10, 10);
        initialScale = new Vector3(0.1f, 0.1f, 0.1f);
        transform.localScale = initialScale;
    }

    //Method to smoothly transition the HighScore Image + Text
    public IEnumerator ZoomInHighScore()
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < zoomSpeed)
        {
            transform.localScale = Vector3.Lerp(initialScale, maxScale, elapsedTime / zoomSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = initialScale;
    }
}
