using UnityEngine;

public class RotatePropeller : MonoBehaviour
{
    public GameObject propeller;
    public float rotationSpeed = 1000f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       propeller.transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
