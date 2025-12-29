using UnityEngine;

public class U2PlayerController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject spawnPosition;
    private float horizontalInput;
    private float verticalInput;
    public float speed = 10.0f;
    public float xRange = 10.0f;
    public float zMin = -2.0f;
    public float zMax = 10.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("P1_Horizontal");
        verticalInput = Input.GetAxis("P1_Vertical");
        // Move the player sideways 
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
        // Move the player forward and backward
        transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);

        // Restrict the player movement in xRange
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        // Restrict the player movement in zRange
        if (transform.position.z < zMin)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zMin);
        }
        if (transform.position.z > zMax)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zMax);
        }

        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectilePrefab, spawnPosition.transform.position, projectilePrefab.transform.rotation);
        }
    }
}
