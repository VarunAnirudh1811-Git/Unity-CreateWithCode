using UnityEngine;

public class U3_BackgroundScroll : MonoBehaviour
{
    public float moveSpeed = 30;
    private float resetDistance;
    private Vector3 startPos;
    public U3_SpwanManager spwanManagerScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spwanManagerScript = GameObject.Find("SpawnManager").GetComponent<U3_SpwanManager>();
        startPos = transform.position;
        resetDistance = GetComponent<Collider>().bounds.size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        ScrollAndReset();
    }

    private void ScrollAndReset()
    {
        // Scroll the BG if !GameOver
        if (!spwanManagerScript.gameOver)
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        
        // Reset the BG
        if (transform.position.x < startPos.x - resetDistance)
        {
            transform.position = startPos;
        }
    }
}
