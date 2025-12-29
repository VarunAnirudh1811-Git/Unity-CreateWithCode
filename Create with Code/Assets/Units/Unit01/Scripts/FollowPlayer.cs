using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;  // Reference to the player object
    private Vector3 cameraOffset =  new Vector3(0, 6, -8);  // Offset position of the camera relative to the player
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + cameraOffset;
    }
}
