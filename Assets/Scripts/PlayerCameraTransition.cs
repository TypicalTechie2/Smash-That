using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCameraTransition : MonoBehaviour
{
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private Quaternion initialRotation;
    private Quaternion targetRotation;
    public float speed = 1f;
    public bool cameraMoved = false;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
    }

    public IEnumerator MovePlayerCamera()
    {
        targetPosition = new Vector3(100, 15, -10);
        targetRotation = Quaternion.Euler(0, 270, 0);

        float elapsedTime = 0.0f;

        while (elapsedTime < speed)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / speed);
            transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, elapsedTime / speed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = targetPosition;
        transform.localRotation = targetRotation;

        // Camera has reached its target position and rotation
        cameraMoved = true;

        // Now switch to the game scene
        SceneManager.LoadScene("Main Menu Scene");
    }
}
