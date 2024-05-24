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

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnObject1());
        StartCoroutine(SpawnObstacles());
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Coroutine for spawning object1
    IEnumerator SpawnObject1()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            if (!isGameActive) yield break;

            Vector3 spawnPosition = new Vector3(Random.Range(0, 2) == 0 ? -15f : 15f, -5.25f, spawnArea.position.z);
            Instantiate(crystalBlock, spawnPosition, Quaternion.identity);
        }
    }

    // Coroutine for spawning obstacles
    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            yield return new WaitForSeconds(7f);
            if (!isGameActive) yield break;

            int randomIndex = Random.Range(0, obstaclePrefabs.Length);
            Vector3 spawnPosition = new Vector3(0f, 1f, spawnArea.position.z);
            Instantiate(obstaclePrefabs[randomIndex], spawnPosition, Quaternion.identity);
        }
    }
}
