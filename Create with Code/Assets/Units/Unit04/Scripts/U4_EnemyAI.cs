using UnityEngine;

public class U4_EnemyAI : MonoBehaviour
{
    public float speed = 5.0f;
    public float destructHeight = -3.0f;
    public int pointValue = 10;

    private Rigidbody enemyRb;
    private GameObject player;
    private U4_SpawnManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("SpawnManager").GetComponent<U4_SpawnManager>();
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        FollowPlayer();
        DestroyObject();
    }

    private void FollowPlayer()
    {
        // Move towards the player if on ground
        if (player.transform.position.y > destructHeight)
        {
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;
            enemyRb.AddForce(lookDirection * speed);
        }
    }

    private void DestroyObject()
    {
        // Destroy enemy if it falls below destructHeight
        if (transform.position.y < destructHeight)
        {
            Destroy(gameObject);
            gameManager.AddScore(pointValue);
        }
    }
}
