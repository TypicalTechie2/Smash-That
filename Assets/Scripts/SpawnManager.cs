using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Prefabs for the objects to spawn
    [SerializeField] GameObject crystalBlock;
    [SerializeField] GameObject[] obstaclePrefabs;
    [SerializeField] Transform spawnArea;
    public bool isGameActive = true;
    private float maxSpeed = 65f;
    private float initialSpeed = 5f;
    public float currentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = initialSpeed;
        StartCoroutine(SpawnCrystalBlock());
        StartCoroutine(SpawnObstacles());
        StartCoroutine(IncreaseSpeed());
    }

    // Coroutine for spawning object1
    public IEnumerator SpawnCrystalBlock()
    {
        while (true)
        {
            yield return new WaitForSeconds(currentSpeed <= 25 ? 3f : 1.5f);
            if (!isGameActive) yield break;

            Vector3 spawnPosition = new Vector3(Random.Range(0, 2) == 0 ? -25f : 25f, -10f, spawnArea.position.z);
            Instantiate(crystalBlock, spawnPosition, Quaternion.identity);
        }
    }

    // Coroutine for spawning obstacles
    public IEnumerator SpawnObstacles()
    {
        while (true)
        {
            yield return new WaitForSeconds(currentSpeed <= 25f ? 5f : currentSpeed >= 45f ? 2f : 3f);
            if (!isGameActive) yield break;

            int randomIndex = Random.Range(0, obstaclePrefabs.Length);
            Vector3 spawnPosition = new Vector3(0f, 1f, spawnArea.position.z);
            Instantiate(obstaclePrefabs[randomIndex], spawnPosition, Quaternion.identity);
        }
    }

    public IEnumerator IncreaseSpeed()
    {
        while (isGameActive && currentSpeed < maxSpeed)
        {
            yield return new WaitForSeconds(10f);
            currentSpeed += 5f;
        }
    }
}
