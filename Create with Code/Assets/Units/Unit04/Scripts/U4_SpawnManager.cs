using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class U4_SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] powerUpPrefabs;
    public GameObject gameOverScreen;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI roundText;

    private int enemyCount;
    private int waveNumber = 1;
    private float spawnRange = 9.0f;
    private int score = 0; 
    private float waitTime = 2.0f;
    private bool isGameActive = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        roundText.text = "Round: " + waveNumber.ToString();
        roundText.gameObject.SetActive(true);
        StartCoroutine(WaitAndSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        SpawnMoreEnemies();
    }

    IEnumerator WaitAndSpawn()
    {
        isGameActive = false;
        yield return new WaitForSeconds(waitTime);

        roundText.gameObject.SetActive(false);

        SpawnObject(enemyPrefabs, waveNumber);
        SpawnObject(powerUpPrefabs, 1);
        isGameActive = true;
    }        

    void SpawnMoreEnemies()
    {
        enemyCount = FindObjectsByType<U4_EnemyAI>(FindObjectsSortMode.None).Length;

        if (enemyCount == 0 & isGameActive)
        {
            waveNumber++;
            roundText.text = "Round: " + waveNumber.ToString();
            roundText.gameObject.SetActive(true);            
            StartCoroutine(WaitAndSpawn());            
        }

    }
    private Vector3 RandomSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        return new Vector3(spawnPosX, 0, spawnPosZ);
    }

    private void SpawnObject(GameObject[] prefabArray ,int count)
    {
        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, prefabArray.Length);
            Instantiate(prefabArray[randomIndex], RandomSpawnPosition(), enemyPrefabs[0].transform.rotation);
        }            
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddScore(int points)
    {
        if (!isGameActive) return;

        score += points;
        scoreText.text = score.ToString("D4");
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        isGameActive = false;
    }
}
