using UnityEngine;

public class U3_PlayerController : MonoBehaviour
{
    public float jumpForce = 10f;
    public float gravityMultiplier = 1.0f;    
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip crashSFX;
    public AudioClip jumpSFX;

    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudioSource;
    private U3_SpwanManager spwanManagerScript;
    private bool isOnGround = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spwanManagerScript = GameObject.Find("SpawnManager").GetComponent<U3_SpwanManager>();
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudioSource = GetComponent<AudioSource>();
        Physics.gravity *= gravityMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        playerJump();
    }

    private void playerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !spwanManagerScript.gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            // Play jump animation
            playerAnim.SetTrigger("Jump_trig");
            // Stop dirt
            dirtParticle.Stop();
            // Play jump VFX
            playerAudioSource.PlayOneShot(jumpSFX, 1.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            spwanManagerScript.gameOver = true;
            Debug.Log("Game Over");
            // Play death animation
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            // Trigger explosion
            explosionParticle.Play();
            // Stop dirt
            dirtParticle.Stop();
            // Play crash VFX
            playerAudioSource.PlayOneShot(crashSFX, 1.0f);
        }
    }
}
