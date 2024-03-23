using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    private Slider slider;
    public Image fillImage;

    private Health player1Health; // Reference to player 1's health script

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Start()
    {
        // Get the index of the character selected by player 1
        int player1CharacterIndex = PlayerPrefs.GetInt("Player1Character", 0);
        // Find the GameObject with the Health script based on the character index
        GameObject player1 = GameObject.FindGameObjectWithTag("Player1");
        if (player1 != null)
        {
            // Get the Health script of player 1
            player1Health = player1.GetComponent<Health>();
        }
        else
        {
            Debug.LogWarning("Player 1 GameObject not found!");
        }
    }

    void Update()
    {
        if (player1Health == null)
        {
            Debug.LogWarning("Player 1 Health script not found!");
            return;
        }

        // Update the health bar based on player 1's health
        float fillValue = (float)player1Health.currentHealth / player1Health.maxHealth;
        slider.value = fillValue;

        // Hide fill image if health is zero
        if (slider.value <= slider.minValue)
        {
            fillImage.enabled = false;
        }
        else
        {
            fillImage.enabled = true;
        }
    }
}
