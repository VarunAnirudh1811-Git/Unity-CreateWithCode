using System;
using UnityEngine;

public class U2SpawnManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    // Top Spawn Parameters
    [Header("Top Spawn Parameters")]
    public float spawnRangeX = 20.0f;
    public float spawnPosZ = 20.0f;
    // Side Spawn Parameters
    [Header("Side Spawn Parameters")]
    public float minSpawnZ = -1.0f;
    public float maxSpawnZ = 15.0f;
    public float spawnPosX = 15.0f;
    // Starting Parameters
    [Header("Spawn Delay and Interval")]
    public float startDelay = 2.0f;
    public float spawnInterval = 6.0f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval);
        InvokeRepeating("SpawnRandomAnimalRight", startDelay + spawnInterval/3, spawnInterval);
        InvokeRepeating("SpawnRandomAnimalLeft", startDelay + 2*spawnInterval/3, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnRandomAnimal()
    {
        int animalIndex = UnityEngine.Random.Range(0, animalPrefabs.Length);
        Vector3 spawnPos = new Vector3(UnityEngine.Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);

        Instantiate(animalPrefabs[animalIndex], spawnPos, animalPrefabs[animalIndex].transform.rotation);
    }

    private void SpawnRandomAnimalLeft()
    {
        int animalIndex = UnityEngine.Random.Range(0, animalPrefabs.Length);
        Vector3 spawnPos = new Vector3(-spawnPosX, 0, UnityEngine.Random.Range(minSpawnZ, maxSpawnZ));

        Instantiate(animalPrefabs[animalIndex], spawnPos, Quaternion.Euler(0, 90, 0));
    }

    private void SpawnRandomAnimalRight()
    {
        int animalIndex = UnityEngine.Random.Range(0, animalPrefabs.Length);
        Vector3 spawnPos = new Vector3(spawnPosX, 0, UnityEngine.Random.Range(minSpawnZ, maxSpawnZ));

        Instantiate(animalPrefabs[animalIndex], spawnPos, Quaternion.Euler(0, -90, 0));
    }
}
