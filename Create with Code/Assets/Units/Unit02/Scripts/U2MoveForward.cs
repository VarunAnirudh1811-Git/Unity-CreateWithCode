using UnityEngine;

public class U2MoveForward : MonoBehaviour
{    
    public float speed = 40.0f;
    public float topBound = 30.0f;
    public float lowerBound = -10.0f;
    public float sideBound = 20.0f;

    // Reference to the game manager
    private U2GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<U2GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("U2GameManager not found on GameManager object.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Move the object forward
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        // Destroy the object if it goes out of bounds
        if (transform.position.z > topBound)
        {            
            Destroy(gameObject);
        }
        else if (transform.position.z < lowerBound)
        {
            gameManager.AddLives(-1);
            Destroy(gameObject);
        }
        else if (Mathf.Abs(transform.position.x) > sideBound)
        {
            gameManager.AddLives(-1);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {        
        if (other.CompareTag("Player"))
        {
            gameManager.AddLives(-1);
        }
        else if (other.CompareTag("Enemy"))
        {
            other.GetComponent<U2AnimalHunger>().FeedAnimal(1);
            Destroy(gameObject);            
        }
    }
}
