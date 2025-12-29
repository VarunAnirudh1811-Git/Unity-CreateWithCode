using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C2_PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    public float delay = 1.0f;
    private float timer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        // On spacebar press, send dog
        timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && timer >= delay )
        {           
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
            timer = 0.0f;
        }
    }
}
