using UnityEngine;

public class U4_RocketAI : MonoBehaviour
{   
    public float rocketSpeed = 10f;
    public float rocketStrength = 15f;
    public float rocketAliveTimer = 5f;

    private Transform target;
    private bool homing = false;
    private U4_PowerUp powerUp;

    private void Start()
    {
        powerUp = FindAnyObjectByType<U4_PowerUp>();
    }

    // Update is called once per frame
    void Update()
    {
        RocketProjectileDirection();
    }

    public void Fire(Transform newTarget)
    {
        target = newTarget;
        homing = true;
        Destroy(gameObject, rocketAliveTimer);
    }

    private void RocketProjectileDirection()
    {
        if (homing)
        {
            if (target == null)
            {
                Destroy(gameObject);
                return;
            }
            Vector3 moveDirection = (target.transform.position - transform.position).normalized;
            transform.position += moveDirection * rocketSpeed * Time.deltaTime;
            transform.LookAt(target);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (target != null && collision.gameObject.CompareTag(target.tag))
        {
            Rigidbody targetRb = collision.gameObject.GetComponent<Rigidbody>();

            Vector3 away = collision.GetContact(0).normal;
            targetRb.AddForce(-away * rocketStrength, ForceMode.Impulse);
            Destroy(gameObject);
        }
    }
}
