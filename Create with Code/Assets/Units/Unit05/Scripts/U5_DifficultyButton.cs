using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class U5_DifficultyButton : MonoBehaviour
{
    public int difficultyLevel;

    private Button button;
    private U5_GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<U5_GameManager>();

        button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficulty);
    }
        

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetDifficulty()
    {
        gameManager.StartGame(difficultyLevel);
    }
}
