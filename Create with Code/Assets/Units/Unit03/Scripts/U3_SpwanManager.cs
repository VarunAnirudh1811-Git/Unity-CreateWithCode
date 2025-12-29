using UnityEngine;

public class U3_SpwanManager : MonoBehaviour
{
    public GameObject obstaclePrefab; // Prefab for the obstacle
    public Vector3 spawnPos = new Vector3(25, 0, 0); // Position to spawn the obstacle
    public float startDelay = 2f; // Delay before the first spawn
    public float repeatRate = 2f; // Time interval between spawns
    public bool gameOver = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnObstacle()
    {
        if (!gameOver)
        {
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
        }        
    }
}
