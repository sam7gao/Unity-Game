using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSceneScript : MonoBehaviour
{
    public Transform player1SpawnPoint;
    public Transform player2SpawnPoint;
    public SelectionScreen selectionScreen; // Array of player prefabs

    void Start()
    {
        int player1CharacterIndex = PlayerPrefs.GetInt("Player1Character", 0);
        int player2CharacterIndex = PlayerPrefs.GetInt("Player2Character", 0);

        int selectedStage = PlayerPrefs.GetInt("SelectedStage", 0);

        GameObject player1 = Instantiate(selectionScreen.characterPrefabs[player1CharacterIndex].player1Prefab, player1SpawnPoint.position, Quaternion.identity);
        GameObject player2 = Instantiate(selectionScreen.characterPrefabs[player2CharacterIndex].player2Prefab, player2SpawnPoint.position, Quaternion.identity);
    }
}
