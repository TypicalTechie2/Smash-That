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
    private float maxSpeed = 100f;
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

    // Update is called once per frame
    void Update()
    {

    }

    // Coroutine for spawning object1
    IEnumerator SpawnCrystalBlock()
    {
        while (true)
        {
            yield return new WaitForSeconds(currentSpeed <= 25 ? 3.5f : 1.5f);
            if (!isGameActive) yield break;

            Vector3 spawnPosition = new Vector3(Random.Range(0, 2) == 0 ? -12f : 12f, -10f, spawnArea.position.z);
            Instantiate(crystalBlock, spawnPosition, Quaternion.identity);
        }
    }

    // Coroutine for spawning obstacles
    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            yield return new WaitForSeconds(currentSpeed <= 25f ? 6f : 3f);
            if (!isGameActive) yield break;

            int randomIndex = Random.Range(0, obstaclePrefabs.Length);
            Vector3 spawnPosition = new Vector3(0f, 1f, spawnArea.position.z);
            Instantiate(obstaclePrefabs[randomIndex], spawnPosition, Quaternion.identity);
        }
    }

    private IEnumerator IncreaseSpeed()
    {
        while (isGameActive && currentSpeed < maxSpeed)
        {
            yield return new WaitForSeconds(10f);
            currentSpeed += 5f;
        }
    }
}
