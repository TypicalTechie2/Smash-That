using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 5f;
    private SpawnManager spawnManager;

    private void Awake()
    {
        spawnManager = FindAnyObjectByType<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveObjects();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sensor"))
        {
            Destroy(gameObject);
        }
    }

    private void MoveObjects()
    {
        if (spawnManager.isGameActive)
        {
            transform.Translate(Vector3.back * spawnManager.currentSpeed * Time.deltaTime, Space.World);
        }

        else
        {
            transform.Translate(Vector3.zero);
        }
    }
}
