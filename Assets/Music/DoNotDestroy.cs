using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoNotDestroy : MonoBehaviour
{
    private int sceneCount = 0; // Variable to keep track of the number of scenes

    private void Awake()
    {
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("GameMusic");
        if (musicObj.Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Update()
    {
        // Check if the current scene index is greater than or equal to 4
        if (SceneManager.GetActiveScene().buildIndex >= 4)
        {
            Destroy(this.gameObject); // Destroy the "GameMusic" GameObject
        }
    }

    private void OnEnable()
    {
        // Subscribe to the scene loaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe from the scene loaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Increment the scene count when a new scene is loaded
        sceneCount++;
    }
}
