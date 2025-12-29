using UnityEngine;

public class Player02Controller : MonoBehaviour
{
    public Camera mainCamera;
    public Camera hoodCamera;
    public KeyCode switchKey;
    public float speed = 10.0f;
    public float turnSpeed = 25.0f;
    private float horizontalInput;
    private float forwardInput;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("P2_Horizontal");
        forwardInput = Input.GetAxis("P2_Vertical");

        // Move the vehicle forward 
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);

        // Turn the vehicle right or left based on horizontal input 
        if (forwardInput != 0)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);
        }

        if (Input.GetKeyDown(switchKey))
        {
            mainCamera.enabled = !mainCamera.enabled;
            hoodCamera.enabled = !hoodCamera.enabled;
        }
    }
}
