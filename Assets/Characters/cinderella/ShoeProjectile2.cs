using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoeProjectile2 : MonoBehaviour
{
    public GameObject shoePrefab;
    public Transform spawnPoint;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ShootShoe();
        }
    }

    void ShootShoe()
    {
        GameObject shoe = Instantiate(shoePrefab, spawnPoint.position, Quaternion.identity);
        Rigidbody2D shoeRb = shoe.GetComponent<Rigidbody2D>();

        // Determine direction based on the sprite renderer of the character
        PlayerController2 playerController = FindObjectOfType<PlayerController2>();
        if (playerController != null)
        {
            SpriteRenderer characterSpriteRenderer = playerController.GetComponent<SpriteRenderer>();
            float direction = characterSpriteRenderer.flipX ? 1f : -1f;

            // Define the force vector with both X and Y components
            Vector2 forceDirection = new Vector2(direction, 0.5f).normalized; // Adjust the values as needed
            float forceMagnitude = 8f; // Adjust the magnitude of the force

            // Apply the force using the defined direction and magnitude
            shoeRb.AddForce(forceDirection * forceMagnitude, ForceMode2D.Impulse);

            // Adjust the rotation of the shoe projectile based on character's facing direction
            shoe.transform.localScale = new Vector3(direction, 1f, 1f);
        }
        else
        {
            Debug.LogError("PlayerController2 not found!");
        }
    }
}
