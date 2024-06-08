using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphearObstacle : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 50f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }
}
