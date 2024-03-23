using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinSceneManager : MonoBehaviour
{
    public GameObject snowWhiteWinsScene;
    public GameObject cinderellaWinsScene;
    public float delayInSeconds = 3f; // Adjust this value as needed

    public GameObject continueToEnding;

    void Start()
    {
        // Disable both win scenes initially
        snowWhiteWinsScene.SetActive(false);
        cinderellaWinsScene.SetActive(false);
    }

    void Update()
    {
        // Check for win conditions and display corresponding win scene
        CheckSnowWhiteWinCondition();
        CheckCinderellaWinCondition();
    }

    void CheckSnowWhiteWinCondition()
    {
        if ((IsDefeated("cinderella") || IsDefeated("cinderellap2")) &&
            !(IsDefeated("snowWhite") || IsDefeated("snowWhitep1")))
        {
            StartCoroutine(ShowSnowWhiteWinsSceneWithDelay());
        }
    }

    void CheckCinderellaWinCondition()
    {
        if ((IsDefeated("snowWhite") || IsDefeated("snowWhitep1")) &&
            !(IsDefeated("cinderella") || IsDefeated("cinderellap2")))
        {
            StartCoroutine(ShowCinderellaWinsSceneWithDelay());
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

    IEnumerator ShowSnowWhiteWinsSceneWithDelay()
    {
        snowWhiteWinsScene.SetActive(true);
        yield return new WaitForSeconds(delayInSeconds);
        continueToEnding.GetComponent<ContinueToEnding>().LoadNextScene();
    }

    IEnumerator ShowCinderellaWinsSceneWithDelay()
    {
        cinderellaWinsScene.SetActive(true);
        yield return new WaitForSeconds(delayInSeconds);
        continueToEnding.GetComponent<ContinueToEnding>().LoadNextScene();
    }

}
