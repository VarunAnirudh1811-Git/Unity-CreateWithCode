using UnityEngine;

public class U3_Obstacle : MonoBehaviour
{
    public float speed = 10f;
    public float objectRemoveDistance = -10f;
    public U3_SpwanManager spwanManagerScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spwanManagerScript = GameObject.Find("SpawnManager").GetComponent<U3_SpwanManager>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveObstacle();
        DestroyObstacle();
    }

    private void MoveObstacle()
    {
        if (spwanManagerScript != null && !spwanManagerScript.gameOver)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }        
    }

    private void DestroyObstacle()
    {
        if (transform.position.x < objectRemoveDistance)
        {
            Destroy(gameObject);
        }
    }
}
