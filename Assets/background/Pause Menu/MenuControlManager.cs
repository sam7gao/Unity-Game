using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneManager : MonoBehaviour
{
    public string nextSceneName; // Name of the next scene to load

    void Update()
    {
        // Check if the user presses the Enter key
        if (Input.GetKeyDown(KeyCode.Return))
        {
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        // Load the next scene
        SceneManager.LoadScene(nextSceneName);
        Time.timeScale = 1f;
    }

    void OnMouseDown()
    {
        // Check if the left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            LoadNextScene();
            Time.timeScale = 1f;
        }
    }
}
