using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    [SerializeField] float ballSpeed = 50f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Get the mouse click position in world space
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));

            // Calculate the direction towards the mouse position
            Vector3 direction = (clickPosition - transform.position).normalized;

            // Instantiate the ball at the player's position
            GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);

            // Add force to the ball in the calculated direction
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(direction * ballSpeed, ForceMode.Impulse);
            }
            else
            {
                Debug.LogWarning("Rigidbody component not found on the ball prefab!");
            }
        }
    }
}
