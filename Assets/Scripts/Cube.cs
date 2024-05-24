using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    // Rotation speed
    public float rotationSpeed = 0.5f;

    // Random rotation axis
    private Vector3 randomRotationAxis;

    // Start is called before the first frame update
    void Start()
    {
        SetRandomRotationAxis();
    }

    // Update is called once per frame
    void Update()
    {
        RotateCube();
    }

    private void SetRandomRotationAxis()
    {
        // Generate a random rotation axis
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);

        randomRotationAxis = new Vector3(randomX, randomY, randomZ).normalized;
    }

    private void RotateCube()
    {
        // Rotate the cube around the random axis
        transform.Rotate(randomRotationAxis, rotationSpeed * Time.deltaTime);
    }
}
