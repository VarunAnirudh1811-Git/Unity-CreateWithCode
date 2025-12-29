using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class U2AnimalHunger : MonoBehaviour
{
    public Slider hungerSlider;
    public int amountToFeed = 2;

    private int currentAmountFed = 0;
    private U2GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hungerSlider.maxValue = amountToFeed;
        hungerSlider.value = 0;
        hungerSlider.fillRect.gameObject.SetActive(false);

        gameManager = GameObject.Find("GameManager").GetComponent<U2GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FeedAnimal(int amount)
    {
        currentAmountFed += amount;        
        hungerSlider.fillRect.gameObject.SetActive(true);
        hungerSlider.value = currentAmountFed;

        if (currentAmountFed >= amountToFeed)
        {
            gameManager.AddScore(amountToFeed * 5); //score multiplier
            Destroy(gameObject, 0.1f);
        }
    }
}
