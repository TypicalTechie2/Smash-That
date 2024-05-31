using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCameraController : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float rotationSpeed = 1.0f;
    private bool cameraMoved = false;

    // Start is called before the first frame update
    void Start()
    {
        // Set initial position and rotation
        transform.position = new Vector3(100, 15, -10);
        transform.rotation = Quaternion.Euler(0, -90, 0);

        StartCoroutine(MoveCamera());
    }


    IEnumerator MoveCamera()
    {
        // Smoothly move towards player
        Vector3 targetPosition = new Vector3(0, 1, -10);
        Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
        while (Vector3.Distance(transform.position, targetPosition) > 0.05f || Quaternion.Angle(transform.rotation, targetRotation) > 1.0f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            yield return null;
        }

        // Camera has reached its target position and rotation
        cameraMoved = true;

        // Now switch to the game scene
        SceneManager.LoadScene("Game Scene");
    }
}
