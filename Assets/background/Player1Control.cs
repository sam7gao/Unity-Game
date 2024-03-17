using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour
{
    [SerializeField] private GameObject[] characterPrefabs;
    [SerializeField] private Transform spawnPoint;

    private GameObject selectedCharacterPrefab;
    public float moveSpeed = 5f; // Adjust this value as needed
    public float jumpForce = 7f; // Adjust this value as needed
    private Rigidbody2D rb; // Rigidbody2D component reference

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component attached to the player GameObject
    }

    void Update()
    {
        // Character movement
        float horizontalInput = Input.GetAxis("Horizontal_P1");
        Vector2 movement = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        rb.velocity = movement;

        // Jump
        if (Input.GetButtonDown("Jump_P1"))
        {
            Jump();
        }

        // Character attacks
        if (Input.GetButtonDown("fire1"))
        {
            // Perform hit1
        }
        else if (Input.GetButtonDown("fire2"))
        {
            // Perform hit2
        }
        else if (Input.GetButtonDown("fire3"))
        {
            // Perform hit3
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    public void SelectCharacter(int characterIndex)
    {
        if (characterIndex >= 0 && characterIndex < characterPrefabs.Length)
        {
            selectedCharacterPrefab = characterPrefabs[characterIndex];
        }
    }

    public void SpawnSelectedCharacter()
    {
        if (selectedCharacterPrefab != null && spawnPoint != null)
        {
            Instantiate(selectedCharacterPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
