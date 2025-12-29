using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class U4_PlayerController : MonoBehaviour
{
    public float playerSpeed = 10.0f;    
    public GameObject powerUpIndicator;    
    public GameObject rocketPrefab;
    public float powerUpTimer = 7.0f;
    [Header("Push Back Settings")]
    public float pushBackStrength = 15.0f;
    [Header("Smash Settings")]
    public float hangTime = 0.2f;
    public float smashSpeed = 10f;
    public float smashForce = 15f;
    public float smashRadius = 5f;

    private bool hasPowerUp = false;
    private Rigidbody playerRb;
    private GameObject cameraFocalPoint;
    private GameObject tmpRocket;
    private U4_SpawnManager gameManager;
    private U4_PowerUp powerUp;
    public PowerUpType currentPowerUp = PowerUpType.None;
    private Coroutine powerUpCountdown;
    public bool smashing = false;
    private float floorY;

    void Start()
    {
        powerUp = FindAnyObjectByType<U4_PowerUp>();
        gameManager = GameObject.Find("SpawnManager").GetComponent<U4_SpawnManager>(); // Reference to SpawnManager
        cameraFocalPoint = GameObject.Find("CameraFocalPoint"); // Reference to CameraFocalPoint
        playerRb = GetComponent<Rigidbody>(); // Reference to Player Rigidbody
    }

    void Update()
    {
        MoveForward(); // Move the player forward based on input
        GameOverScreen(); // Check for game over condition

        if (currentPowerUp == PowerUpType.RocketLaunch && Input.GetKeyDown(KeyCode.F))
        {
            LaunchRockets();
        }

        if (currentPowerUp == PowerUpType.Smash && Input.GetKeyDown(KeyCode.F) && !smashing)
        {
            smashing=true;
            StartCoroutine(Smash());
        }

    }

    private void MoveForward()
    {
        float forwardInput = Input.GetAxis("P1_Vertical"); 
        playerRb.AddForce(cameraFocalPoint.transform.forward * playerSpeed * forwardInput);

        // Update power-up indicator position
        powerUpIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Player's powerUpTime is updated with the Power Up Type assigned to PowerUp
        if (other.CompareTag("PowerUp"))
        {
            powerUp = other.GetComponent<U4_PowerUp>();
            hasPowerUp = true;
            currentPowerUp = other.gameObject.GetComponent<U4_PowerUp>().powerUpType;
            powerUpIndicator.SetActive(true);
            Destroy(other.gameObject);
            if (powerUpCountdown != null)
            {
                StopCoroutine(powerUpCountdown);
            }
            StartCoroutine(PowerUpCountdownRoutine());
        }
    }
    
    // Co-Routines
    IEnumerator PowerUpCountdownRoutine()
    {
        // Coroutine to handle power-up duration
        yield return new WaitForSeconds(powerUpTimer);
        hasPowerUp = false;
        currentPowerUp = PowerUpType.None;
        powerUpIndicator.SetActive(false);
    }

    IEnumerator Smash()
    {
        floorY = transform.position.y; // Store Y pos

        float elapsedTime = 0f;

        while (elapsedTime < hangTime)
        {
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, smashSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        while (transform.position.y > floorY)
        {
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, -smashSpeed * 2);
            yield return null;
        }

        var enemies = FindObjectsByType<U4_EnemyAI>(FindObjectsSortMode.None);

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                Rigidbody enemyRb = enemies[i].GetComponent<Rigidbody>();
                if (enemyRb != null)
                {
                    enemyRb.AddExplosionForce(smashForce, transform.position, smashRadius, 0f, ForceMode.Impulse);
                }
            }
        }
        smashing = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Player collides with enemy while powered up
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp && currentPowerUp == PowerUpType.PushBack)
        {
            PushBackPowerUp(collision.gameObject);
        }
    }

    void LaunchRockets()
    {
        foreach (var enemy in FindObjectsByType<U4_EnemyAI>(FindObjectsSortMode.None))
        {
            tmpRocket = Instantiate(rocketPrefab, transform.position + Vector3.up, Quaternion.identity);
            tmpRocket.GetComponent<U4_RocketAI>().Fire(enemy.transform);
        }
    }

    private void PushBackPowerUp(GameObject enemyGameObject)
    {
        Rigidbody enemyRb = enemyGameObject.GetComponent<Rigidbody>();
        Vector3 awayFromPlayer = (enemyGameObject.transform.position - transform.position).normalized;

        enemyRb.AddForce(awayFromPlayer * pushBackStrength, ForceMode.Impulse);
    }

    private void GameOverScreen()
    {
        if (transform.position.y < -10)
        {
            gameManager.GameOver();
        }
    }
}
