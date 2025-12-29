using UnityEngine;

public class U5_Target : MonoBehaviour
{
    public ParticleSystem explosionParticle;
    public int pointValue;

    private float spawnRangeX = 4.5f;
    private float spawnPosY = -2.0f;
    private Vector2 projectileStrength = new Vector2(12, 15);
    private float projectileTorque = 1f;
    private Rigidbody targetRb;
    private U5_GameManager gameManager;    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindFirstObjectByType<U5_GameManager>();

        targetRb = GetComponent<Rigidbody>();

        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque() , ForceMode.Impulse);

        transform.position = RandomSpawmPos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        DestroyUsingMouse();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        
        if (!gameObject.CompareTag("Enemy"))
        {
            gameManager.GameOver();
        }
    }

    void DestroyUsingMouse()
    {
        if ((gameManager.isGameActive))
        {
            Destroy(gameObject);

            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);

            gameManager.UpdateScore(pointValue);
        }
        
    } 

    Vector3 RandomForce()
    {
        // Returns a random force between the given range
        float randomStrength = Random.Range(projectileStrength.x, projectileStrength.y);
        Vector3 randomForce = Vector3.up * randomStrength;
        return randomForce;
    }

    float RandomTorque()
    {
        // Returns a random torque between the given range
        float randomTorque = Random.Range(-projectileTorque, projectileTorque);
        return randomTorque;
    }

    Vector3 RandomSpawmPos()
    {
        // Returns a random position in the given bounds
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), spawnPosY);
        return spawnPos;
    }
}
