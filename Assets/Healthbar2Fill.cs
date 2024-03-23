using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar2 : MonoBehaviour
{
    private Slider slider;
    public Image fillImage;

    private Health player2Health; // Reference to player 2's health script

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Start()
    {
        // Get the index of the character selected by player 2
        int player2CharacterIndex = PlayerPrefs.GetInt("Player2Character", 0);
        // Find the GameObject with the Health script based on the character index
        GameObject player2 = GameObject.FindGameObjectWithTag("Player2");
        if (player2 != null)
        {
            // Get the Health script of player 2
            player2Health = player2.GetComponent<Health>();
        }
        else
        {
            Debug.LogWarning("Player 2 GameObject not found!");
        }
    }

    void Update()
    {
        if (player2Health == null)
        {
            Debug.LogWarning("Player 2 Health script not found!");
            return;
        }

        // Update the health bar based on player 2's health
        float fillValue = (float)player2Health.currentHealth / player2Health.maxHealth;
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