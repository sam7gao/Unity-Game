using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionScreen : MonoBehaviour
{
    public Transform[] characterPositions;
    public Transform[] stagePositions;
    public PlayerCharacterPrefabs[] characterPrefabs;

    public GameObject player1Arrow;
    public GameObject player2Arrow;
    public GameObject stageArrow;

    private int currentPlayer1Index = 0;
    private int currentPlayer2Index = 1;
    private int currentStageIndex = 0;

    private bool player1SelectionComplete = false;
    private bool player2SelectionComplete = false;
    private bool stageSelectionComplete = false;
	
	
    void Update()
    {
        if (!player1SelectionComplete)
        {
            // Player 1 Controls
            if (Input.GetKeyDown(KeyCode.A))
            {
                MovePlayer1Arrow(-1);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                MovePlayer1Arrow(1);
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                player1SelectionComplete = true;
            	PlayerPrefs.SetInt("Player1Character", currentPlayer1Index);
            }
        }

        if (!player2SelectionComplete)
        {
            // Player 2 Controls
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MovePlayer2Arrow(-1);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MovePlayer2Arrow(1);
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                player2SelectionComplete = true;
                PlayerPrefs.SetInt("Player2Character", currentPlayer2Index);
            }

        }

        if (Input.GetKeyDown(KeyCode.G) && player1SelectionComplete) // Player 1 unconfirm
        {
            player1Arrow.transform.position = characterPositions[0].position;
            currentPlayer1Index = 0;
            player1SelectionComplete = false;
        }
            
        if (Input.GetKeyDown(KeyCode.L) && player2SelectionComplete) // Player 2 unconfirm
        {
            player2Arrow.transform.position = characterPositions[0].position;
            currentPlayer2Index = 0;
            player2SelectionComplete = false;
        }


        if (!stageSelectionComplete && player1SelectionComplete && player2SelectionComplete)
        {
            // Stage Selection Controls
            if (Input.GetKeyDown(KeyCode.A))
            {
                MoveStageArrow(-1);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                MoveStageArrow(1);
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                stageSelectionComplete = true;
                LoadSceneByStageIndex(currentStageIndex);
            }
        }
    }

    void MovePlayer1Arrow(int direction)
    {
        currentPlayer1Index = Mathf.Clamp(currentPlayer1Index + direction, 0, characterPositions.Length - 1);
        player1Arrow.transform.position = characterPositions[currentPlayer1Index].position;
    }

    void MovePlayer2Arrow(int direction)
    {
        currentPlayer2Index = Mathf.Clamp(currentPlayer2Index + direction, 0, characterPositions.Length - 1);
        player2Arrow.transform.position = characterPositions[currentPlayer2Index].position;
    }

    void MoveStageArrow(int direction)
    {
        currentStageIndex = Mathf.Clamp(currentStageIndex + direction, 0, stagePositions.Length - 1);
        stageArrow.transform.position = stagePositions[currentStageIndex].position;
    }

	void LoadSceneByStageIndex(int index)
	{
    	string sceneName;
    	switch (index)
    	{
        	case 0:
            	sceneName = "Dark Forest";
            	break;
        	case 1:
            	sceneName = "Highlands";
            	break;
        	case 2:
            	sceneName = "SampleScene";
            	break;
            case 3:
            	sceneName = "Evening Goth";
            	break;
            case 4:
            	sceneName = "Seaside";
            	break;
            
        	default:
            	// If the index is out of range, load a default scene or handle the error as needed
            	sceneName = "DefaultScene";
            	break;
    	}
    	SceneManager.LoadScene(sceneName);
	}

}
