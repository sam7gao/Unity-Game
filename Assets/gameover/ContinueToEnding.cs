using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueToEnding : MonoBehaviour
{
    public string nextSceneIfP1Wins;
    public string nextSceneIfP2Wins;

    public void LoadNextScene()
    {
        // Check if Cinderella or SnowWhiteP1 wins
        if ((IsDefeated("cinderella") || IsDefeated("snowWhitep1")) &&
            !(IsDefeated("snowWhite") || IsDefeated("cinderellap2")))
        {
            // Load the scene specified for Cinderella winning
            SceneManager.LoadScene(nextSceneIfP2Wins);
        }
        // Check if SnowWhite or SnowWhiteP1 wins
        else if ((IsDefeated("snowWhite") || IsDefeated("cinderellap2")) &&
                !(IsDefeated("cinderella") || IsDefeated("snowWhitep1")))
        {
            // Load the scene specified for SnowWhite winning
            SceneManager.LoadScene(nextSceneIfP1Wins);
        }
    }

    bool IsDefeated(string characterName)
    {
        GameObject character = GameObject.Find(characterName + "(Clone)");
        if (character != null)
        {
            Health health = character.GetComponent<Health>();
            if (health != null && health.currentHealth <= 0)
            {
                return true;
            }
        }
        return false;
    }
}

