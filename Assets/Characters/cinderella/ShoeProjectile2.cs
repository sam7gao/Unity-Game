using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoeProjectile2 : MonoBehaviour
{
    public GameObject shoePrefab;
    public Transform spawnPoint;
    public float cooldownPeriod = 1f;
    private bool canShoot = true;
    public LayerMask collisionLayer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && canShoot)
        {
            ShootShoe();
            StartCoroutine(CooldownCoroutine());
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
    
    IEnumerator CooldownCoroutine()
    {
        canShoot = false;
        yield return new WaitForSeconds(cooldownPeriod);
        canShoot = true;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with the specified collision layer
        if (((1 << collision.gameObject.layer) & collisionLayer) != 0)
        {
            var healthComponent = collision.gameObject.GetComponent<Health>();
            if (healthComponent != null)
            {
                healthComponent.TakeDamage(3);
            }
        
            // Destroy the projectile upon collision with any object
            Destroy(gameObject);
        }
    }
}
