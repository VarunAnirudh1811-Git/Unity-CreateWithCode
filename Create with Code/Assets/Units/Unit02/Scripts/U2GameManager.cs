using System.Runtime.CompilerServices;
using UnityEngine;

public class U2GameManager : MonoBehaviour
{
    public int lives = 3;
    public int score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int value)
    {
        bool isAlive = AddLives(0);
        if (!isAlive)
        {
            return;
        }
        score += value;
        Debug.Log($"Score: {score}");
    }

    public bool AddLives(int value)
    {
        lives += value;

        if (lives <= 0)
        {
            Debug.Log("Game Over");
            lives = 0;
        }

        Debug.Log($"Lives: {lives}");

        return lives > 0;
    }
}
